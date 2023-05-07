using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{

    [SerializeField] float intensityAmount = 5f;
    [SerializeField] float restoreAngle = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var flashlight = other.GetComponentInChildren<Flashlight>();
            flashlight.RestoreLightAngle(restoreAngle);
            flashlight.RestoreLightIntensitive(intensityAmount);
            Destroy(gameObject);
        }
    }
}
