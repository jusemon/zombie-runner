﻿using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var ammo = FindObjectOfType<Ammo>();
            ammo.IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
