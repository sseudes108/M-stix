using TMPro;
using UnityEngine;
using Mistix.Enums;

namespace Mistix{
    public class ArcaneCard: Card{
        [SerializeField] private ArcaneCardSO _arcaneCardData; 
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshProUGUI _name, _effect;

        private void Start() {
            SetUpArcaneCard();
        }

        public override void SetUpCardData(ScriptableObject CardData){
            _arcaneCardData = CardData as ArcaneCardSO;
        }

        public void SetUpArcaneCard(){
            _spriteRenderer.sprite  = _arcaneCardData.Illustration;
            _name.text = _arcaneCardData.Name;
            _effect.text = _arcaneCardData.Effect;
        }

        public override string GetCardInfo(){
            return $"{ECardType.Arcane}{_arcaneCardData.Name}";
        }
    }
}