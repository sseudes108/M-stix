using Mistix.Enums;
using UnityEngine;

namespace Mistix{
    [CreateAssetMenu(fileName = "ArcaneCardSO", menuName = "Mistix/ArcaneCardSO", order = 0)]
    public class ArcaneCardSO : ScriptableObject {
        public Texture2D Ilustration;
        public string Name;
        public string Effect;
        public EArcaneType ArcaneType;
    }
}