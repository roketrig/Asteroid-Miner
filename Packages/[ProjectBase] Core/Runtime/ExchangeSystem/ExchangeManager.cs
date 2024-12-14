using System.Collections.Generic;
using UnityEngine.Events;
using ProjectBase.Utilities;

namespace ProjectBase.Core
{
    public class ExchangeManager : Singleton<ExchangeManager>
    {

        private bool isInitialized = false;
        private Dictionary<ExchangeType, double> data = new Dictionary<ExchangeType, double>();

        public static UnityEvent OnExchange = new UnityEvent();

        public void Init()
        {
            Load();
            isInitialized = true;
        }

        public double GetData(ExchangeType exchangeType)
        {

            if (!isInitialized)
            {
                Init();
            }

            if (data.ContainsKey(exchangeType))
            {
                return data[exchangeType];
            }

            return 0;
        }

        // returns if result clamped to 0
        public bool DoExchange(ExchangeType exchangeType, int diff)
        {
            if (data.ContainsKey(exchangeType))
            {
                data[exchangeType] += diff;
            }
            else
            {
                data[exchangeType] = diff;
            }

            if (data[exchangeType] < 0)
            {
                data[exchangeType] = 0;
                if (OnExchange != null)
                {
                    OnExchange.Invoke();
                }
                return false;
            }

            OnExchange.Invoke();
            Save();

            return true;
        }

        private void Save()
        {
            var playerData = GameManager.Instance.PlayerData;
            playerData.CurrencyData = data;
        }

        private void Load()
        {
            data = GameManager.Instance.PlayerData.CurrencyData;
        }
    }
}
