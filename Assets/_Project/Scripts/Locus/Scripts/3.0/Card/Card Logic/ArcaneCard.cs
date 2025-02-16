using TMPro;
using UnityEngine;

namespace Mistix{
    public abstract class ArcaneCard : Card {
        protected EArcaneType ArcaneType;
        protected string Effect;
        [SerializeField] private TextMeshProUGUI _effect;

        public override void SetCardInfo(){
            var CardData = _data as ArcaneCardSO;
            base.SetCardInfo();
            ArcaneType = CardData.ArcaneType;

            Effect = CardData.Effect;
        }

        public override void SetCardText(){
            base.SetCardText();
            _effect.text = Effect;
        }
    }
}