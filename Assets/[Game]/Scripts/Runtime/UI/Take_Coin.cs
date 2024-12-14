using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Coin : MonoBehaviour
{
    [SerializeField] private FloatingImageAnimator floatingImagePrefab;
    [SerializeField] private Transform targetPosition;
    [SerializeField] public static int coinValue = 200;
    [SerializeField] private TextManager textManager;
    private int totalCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Vector3 coinPosition = other.transform.position;
            Destroy(other.gameObject);
            Vector3 worldPosition = Camera.main.WorldToScreenPoint(coinPosition);

            FloatingImageAnimator floatingImage = Instantiate(floatingImagePrefab, worldPosition, Quaternion.identity);
            floatingImage.StartImageAnimation(targetPosition);
            totalCoin += coinValue;
            textManager.ShowText("+ " + totalCoin.ToString());
        }
    }
}