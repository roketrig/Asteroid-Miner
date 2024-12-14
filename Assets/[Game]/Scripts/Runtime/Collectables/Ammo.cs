using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, ICollectable
{
    public BulletSO bulletData;

    public void GetCollected()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.CollectAmmo(this);
        }
    }

    public bool IsAmmo()
    {
        return true;
    }
}