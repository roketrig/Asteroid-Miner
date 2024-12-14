using ProjectBase.Core;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_TextMeshPro;
    private int level = 1;
    private int level_currency = 250;
    private Hook hookScript;

    private void Start()
    {
        hookScript = FindObjectOfType<Hook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            double coinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];

            if (coinAmount >= level_currency)
            {
                level++;
                UpdateLevelText();
                level_currency += 20;
                GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, level_currency);

                if (level > 50)
                {
                    level = 50;
                }

                if (level <= 50)
                {
                    float newHookSpeed = hookScript.hookSpeed * 1.08f;
                    float newHookRange = hookScript.hookRange * 1.08f;

                    UpdateHook(newHookSpeed, newHookRange);
                }
                Debug.Log("Yeni Fiyat: " + level_currency);
            }
        }
    }

    private void UpdateLevelText()
    {
        m_TextMeshPro.text = "Level " + level.ToString();
    }

    private void UpdateHook(float newHookSpeed, float newHookRange)
    {
        if (hookScript != null)
        {
            hookScript.hookSpeed = newHookSpeed;
            hookScript.hookRange = newHookRange;
            Debug.Log("Hook Parameters Updated - Speed: " + newHookSpeed + ", Range: " + newHookRange);
        }
    }
}
