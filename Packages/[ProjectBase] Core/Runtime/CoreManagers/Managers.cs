using ProjectBase.Utilities;
using UnityEngine;

namespace ProjectBase.Core
{
    public class Managers : Singleton<Managers>
    {
        [Header("CONFIG")]
        [SerializeField] private int _targetFrameRate = 120;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}
