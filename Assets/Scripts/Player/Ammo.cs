using System;
using UnityEngine;

public enum AmmoType
{
    Bullets,
    Shells,
    Rockets
}

public class Ammo : MonoBehaviour
{
    [Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    [SerializeField] AmmoSlot[] ammoSlots;

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        var ammoSlot = GetAmmoSlot(ammoType);
        return ammoSlot.ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        var ammoSlot = GetAmmoSlot(ammoType);
        ammoSlot.ammoAmount--;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (var slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
