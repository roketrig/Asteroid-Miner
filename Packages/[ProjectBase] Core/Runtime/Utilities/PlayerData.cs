using System.Collections.Generic;
using Sirenix.OdinInspector;
using ProjectBase.Core;

namespace ProjectBase.Utilities
{
    [System.Serializable]
    public class PlayerData
    {
        public PlayerData()
        {
            CurrencyData = new Dictionary<ExchangeType, double>();
            CurrencyData[ExchangeType.Coin] = 0;
        }

        private Dictionary<ExchangeType, double> currencyData = new Dictionary<ExchangeType, double>();
        [BoxGroup("Currency Data")]
        [ShowInInspector]
        [OnValueChanged("NotifyChange")]
        public Dictionary<ExchangeType, double> CurrencyData
        {
            get
            {
                return currencyData;
            }
            set
            {
                currencyData = value;
            }
        }

        private void NotifyChange()
        {
            EventManager.OnPlayerDataChange.Invoke();
        }

        public void IncreaseCurrencyData(ExchangeType type, float value)
        {
            CurrencyData[type] += value;
            NotifyChange();
        }

        public void DecreaseCurrencyData(ExchangeType type, float value)
        {
            CurrencyData[type] -= value;
            if (CurrencyData[type] < 0)
                CurrencyData[type] = 0;
            NotifyChange();
        }

        public void UpdateCurrencyData(ExchangeType type, float value)
        {
            CurrencyData[type] = value;
            NotifyChange();
        }
    }
}