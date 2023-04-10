using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 100;
    [SerializeField] float damage = 20;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;

    Animation animations;
    Dictionary<string, string> animationsNames;

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
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }


    private void Shoot()
    {
        if (ammoSlot.CurrentAmmo == 0 || animations.isPlaying) return;
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo();
    }

    private void Reload()
    {
        PlayAnimation("Reload");
    }

    private void PlayMuzzleFlash()
    {
        var played = PlayAnimation("Fire") || PlayAnimation("FireWBullet");
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log($"Hit {hit.transform.name}");
            CreateHitImpact(hit);
            var target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
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
        animations.Play(animationsNames[shortName]);
        return true;
    }
}
