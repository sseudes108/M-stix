using UnityEngine;

namespace Mistix{
    [CreateAssetMenu(fileName = "ArcaneCardSO", menuName = "Mistix/ArcaneCardSO", order = 0)]
    public class ArcaneCardSO : ScriptableObject {
        public Sprite Illustration;
        public string Name, Effect;
    }
}