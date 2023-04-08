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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log($"Hit {hit.transform.name}");
            var target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
