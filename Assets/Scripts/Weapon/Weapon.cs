using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Serializable]
    private class WeaponCharacteristics
    {
        public bool IsAutomatic = false;
        public float Range = 100f;
        public float Damage = 20f;
        public float TimeBetweenShots = 0.5f;
    }

    [SerializeField] Camera FPSCamera;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] WeaponCharacteristics characteristics = new WeaponCharacteristics();

    Animation animations;
    Dictionary<string, string> animationsNames;
    bool canShoot = true;
    bool continueShooting = false;

    void OnEnable()
    {
        canShoot = true;
        continueShooting = false;
    }

    void Start()
    {
        animationsNames = new Dictionary<string, string>();
        animations = GetComponent<Animation>();
        foreach (AnimationState clip in animations)
        {
            Debug.Log("Added animation " + clip.name + " as " + clip.name.Split('|')[1]);
            animationsNames.Add(clip.name.Split('|')[1], clip.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();

        if ((Input.GetButtonDown("Fire1") || (characteristics.IsAutomatic && continueShooting)) && canShoot)
        {
            StartCoroutine(Shoot());
        }
       
        if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void DisplayAmmo()
    {
        ammoText.SetText(ammoSlot.GetCurrentAmmo(ammoType).ToString("000"));
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        continueShooting = true;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            Debug.Log("Shooting");
            PlayAnimation("Fire");
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(characteristics.TimeBetweenShots);
        canShoot = true;
    }

    private void StopShooting()
    {
        Debug.Log("Stop shooting");
        continueShooting = false;

    }

    private void Reload()
    {
        PlayAnimation("Reload");
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out RaycastHit hit, characteristics.Range))
        {
            CreateHitImpact(hit);
            var target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(characteristics.Damage);
            }
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        if (hit.transform)
        {
            var impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.1f);
        }
    }

    private bool PlayAnimation(string shortName)
    {
        if (!animationsNames.ContainsKey(shortName)) return false;
        if (animations.isPlaying) animations.Stop();
        animations.Play(animationsNames[shortName]);
        return true;
    }
}
