using UnityEngine;

namespace Mistix{
    
    [CreateAssetMenu(fileName = "ArcaneCardSO", menuName = "Mistix/Card/Arcane", order = 0)]
    public class ArcaneCardSO : CardSO {
        public EArcaneType ArcaneType;
        public string Effect;
    }
}
