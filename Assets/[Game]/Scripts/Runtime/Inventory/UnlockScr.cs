using DG.Tweening;
using ProjectBase.Core;
using TMPro;
using UnityEngine;

namespace Assets._Game_.Scripts.Runtime.Inventory
{
    public class UnlockScr : MonoBehaviour
    {
        public float NumToUnlock;
        private double CoinAmount;
        public TextMeshPro TxtAmtUnlock;
        public GameObject ItemToUnlock;
        private bool isopen = false;
        private Vector3 originalPosition;

        private void Start()
        {
            TxtAmtUnlock.text = NumToUnlock.ToString();
            originalPosition = ItemToUnlock.transform.localPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isopen)
            {
                CoinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
                if (CoinAmount >= NumToUnlock)
                {
                    GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, NumToUnlock);

                    if (ItemToUnlock.activeSelf)
                    {

                        ItemToUnlock.SetActive(false);
                    }
                    else
                    {
                        ItemToUnlock.SetActive(true);
                        ItemToUnlock.transform.localPosition = originalPosition + new Vector3(0, -5f, 0);
                        ItemToUnlock.transform.DOLocalMove(originalPosition, 0.5f).SetEase(Ease.OutBounce);
                    }

                    isopen = true;
                    TxtAmtUnlock.text = "";
                }
            }
        }
    }
}
