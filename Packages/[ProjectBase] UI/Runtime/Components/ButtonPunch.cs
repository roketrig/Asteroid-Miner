using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    public class ButtonPunch : MonoBehaviour
    {
        private Button _button;
        private Button Button => _button == null ? _button = GetComponent<Button>() : _button;

        [SerializeField] private float _punchStrength = 0.1f;
        [SerializeField] private float _punchDuration = 0.15f;

        private string _punchTweenID;

        private void Awake() => _punchTweenID = GetInstanceID() + "PunchTween";

        private void OnEnable() => Button.onClick.AddListener(PunchTween);

        private void OnDisable() => Button.onClick.RemoveListener(PunchTween);

        private void PunchTween()
        {
            DOTween.Complete(_punchTweenID);
            transform.DOPunchScale(Vector3.one * _punchStrength, _punchDuration, 1).SetEase(Ease.Linear).SetId(_punchTweenID);
        }
    }
}
