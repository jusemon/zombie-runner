using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimunAngle = 40f;

    Light flashlight;

    private void Start()
    {
        flashlight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightInstensity();
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        flashlight.spotAngle = restoreAngle;
    }

    public void RestoreLightIntensitive(float intensityAmmount)
    {
        flashlight.intensity += intensityAmmount;
    }

    private void DecreaseLightInstensity()
    {
        if (flashlight.intensity <= 0)
        {
            flashlight.intensity = 0;
            return;
        }

        flashlight.intensity -= lightDecay * Time.deltaTime;
    }

    private void DecreaseLightAngle()
    {
        if (flashlight.spotAngle <= minimunAngle)
        {
            flashlight.spotAngle = minimunAngle;
            return;
        }

        flashlight.spotAngle -= angleDecay * Time.deltaTime;
    }
}
