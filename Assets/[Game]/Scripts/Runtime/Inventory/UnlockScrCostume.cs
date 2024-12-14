using UnityEngine;
using TMPro;
using DG.Tweening;

public class UnlockScrCostume : MonoBehaviour
{
    public int requiredKillsToUnlock;
    [SerializeField] private EnemyKillCounterSO killCounterData;

    public TextMeshPro TxtAmtUnlock;
    public GameObject ItemToUnlock;
    private bool isUnlocked = false;
    private Vector3 originalPosition;

    private int lastKillCount;

    private void Start()
    {
        killCounterData.InitData(); 
        lastKillCount = killCounterData.totalEnemiesKilled; 
        UpdateUnlockText(); 
    }


    private void Update()
    {
        if (killCounterData.totalEnemiesKilled != lastKillCount)
        {
            lastKillCount = killCounterData.totalEnemiesKilled;
            UpdateUnlockText(); 
        }

        if (!isUnlocked && killCounterData.totalEnemiesKilled >= requiredKillsToUnlock)
        {
            UnlockItem();
        }
    }

    private void UnlockItem()
    {
        isUnlocked = true;
        ItemToUnlock.SetActive(true);
        ItemToUnlock.transform.localPosition = originalPosition + new Vector3(0, -5f, 0);
        ItemToUnlock.transform.DOLocalMove(originalPosition, 0.5f).SetEase(Ease.OutBounce);

        TxtAmtUnlock.text = "";
    }

    private void UpdateUnlockText()
    {
        TxtAmtUnlock.text = $"{killCounterData.totalEnemiesKilled} / {requiredKillsToUnlock}";
    }
}
