using ProjectBase.Core;
using UnityEngine;

public class SellPoint : MonoBehaviour
{
    public float toplam = 0f;
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>(); // PlayerInventory nesnesine erişim
    }

    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroidComponent = other.GetComponent<Asteroid>();

        if (asteroidComponent != null)
        {
            Dotask dotask = FindObjectOfType<Dotask>();

            if (dotask != null)
            {
                dotask.InstantiateCustomerPrefab();
            }

        }
    }

    public void resetToplam()
    {
        toplam = 0f;
    }
}
