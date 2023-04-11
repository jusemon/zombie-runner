using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    [SerializeField] int currentWeapon = 0;
    Dictionary<string, KeyCode> keyCodes = new Dictionary<string, KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        SetKeyCodes();
        SetWeaponActive();

    }

    private void SetKeyCodes()
    {
        var maxNumber = transform.childCount > 9 ? 9 : transform.childCount;
        for (int i = 0; i < maxNumber; i++)
        {
            keyCodes.Add($"Alpha{i + 1}", (KeyCode)Enum.Parse(typeof(KeyCode), $"Alpha{i + 1}"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        int prevoiusWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();
        if (prevoiusWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon == 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        var maxNumber = transform.childCount > 9 ? 9 : transform.childCount;
        for (int i = 0; i < maxNumber; i++)
        {
            if (Input.GetKeyDown(keyCodes[$"Alpha{i + 1}"]))
            {
                currentWeapon = i;
            }
        }
    }

    private void SetWeaponActive()
    {
        foreach (var (weapon, weaponIndex) in transform.WithIndex<Transform>())
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
