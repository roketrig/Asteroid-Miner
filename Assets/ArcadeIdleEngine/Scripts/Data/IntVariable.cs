using UnityEngine;

namespace ArcadeBridge.ArcadeIdleEngine.Data
{
    [CreateAssetMenu(menuName = nameof(ArcadeIdleEngine) + "/" + nameof(Data) + "/" + nameof(IntVariable))]
    public class IntVariable : Saveable<int>
    {
        protected override void OnEnable()
        {
            RuntimeValue = (int)GetDefaultValue;
        }
        
        public override void RestoreState(object obj)
        {
            RuntimeValue = (int)obj;
        }
    }
}