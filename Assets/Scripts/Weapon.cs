using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponCharacteristics
{
    public bool IsAutomatic = false;
    public float Range = 100f;
    public float Damage = 20f;
    public float TimeBetweenShots = 0.5f;
}

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] WeaponCharacteristics characteristics = new WeaponCharacteristics();

    Animation animations;
    Dictionary<string, string> animationsNames;
    bool canShoot = true;
    bool continueShooting = false;

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
        if ((Input.GetButtonDown("Fire1") || (characteristics.IsAutomatic && continueShooting)) && canShoot)
        {
            StartCoroutine(Shoot());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }


    private IEnumerator Shoot()
    {
        canShoot = false;
        continueShooting = true;
        if (ammoSlot.CurrentAmmo > 0)
        {
            if (!PlayAnimation("Fire")) PlayAnimation("FireWBullet");
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo();
        }
        yield return new WaitForSeconds(characteristics.TimeBetweenShots);
        canShoot = true;
    }

    private void StopShooting()
    {
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
            Debug.Log($"Hit {hit.transform.name}");
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
