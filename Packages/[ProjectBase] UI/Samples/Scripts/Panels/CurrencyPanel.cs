using DG.Tweening;
using ProjectBase.Core;
using ProjectBase.Utilities;
using TMPro;
using UnityEngine;

namespace ProjectBase.UI
{
    public class CurrencyPanel : BasePanel
    {
        [SerializeField] private TextMeshProUGUI _coinTextMesh;

        private const float COIN_TWEEN_DURATION = 0.25f;

        private string _coinTweenID;
        private double _currentAmount;

        private void Start()
        {
            SetDefaultValues();
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
            LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);

            GameManager.Instance.OnStageFail.AddListener(HidePanel);
            EventManager.OnPlayerDataChange.AddListener(UpdateTextMesh);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
            LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);

            GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
            EventManager.OnPlayerDataChange.RemoveListener(UpdateTextMesh);
        }

        private void UpdateTextMesh()
        {
            double targetAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];

            DOTween.Kill(_coinTweenID);
            DOTween.To(() => _currentAmount, x => _currentAmount = x, targetAmount, COIN_TWEEN_DURATION).SetEase(Ease.Linear).SetId(_coinTweenID).OnUpdate(() =>
            {
                SetText(_currentAmount);
            });
        }

        private void SetDefaultValues()
        {
            _coinTweenID = GetInstanceID() + "CoinTweenID";
            _currentAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
            SetText(_currentAmount);
        }

        private void SetText(double balance)
        {
            _coinTextMesh.SetText(ProjectBaseUtilities.ScoreShow(balance));
        }
    }
}
