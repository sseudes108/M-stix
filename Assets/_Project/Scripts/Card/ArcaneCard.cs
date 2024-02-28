using TMPro;
using UnityEngine;
using Mistix.Enums;

namespace Mistix{
    public class ArcaneCard: Card{
        [SerializeField] private ArcaneCardSO _arcaneCardData;
        // [SerializeField] private SpriteRenderer _spriteRenderer;
        // [SerializeField] private TextMeshProUGUI _name, _effect;
        private EArcaneType _arcaneType;

        private void Awake() {
            SetUpArcaneCard();
        }

        private void Start() {
        }

        public override void SetUpCardData(ScriptableObject CardData){
            _arcaneCardData = CardData as ArcaneCardSO;
        }

        public void SetUpArcaneCard(){
            // _spriteRenderer.sprite  = _arcaneCardData.Illustration;
            // _name.text = _arcaneCardData.Name;
            // _effect.text = _arcaneCardData.Effect;
            _arcaneType = _arcaneCardData.ArcaneType;
            _ilustration = _arcaneCardData.Ilustration;
        }

        public override string GetCardInfo(){
            return $"{ECardType.Arcane}{_arcaneCardData.Name}";
        }

        public string GetCardName(){
            return _arcaneCardData.Name;
        }

        public string GetCardEffectText(){
            return _arcaneCardData.Effect;
        }

        public EArcaneType GetArcaneType() => _arcaneType;

        public override Texture2D GetCardIlustration(){
            return _ilustration;
        }
        private void OnMouseDown(){
            
        }
    }
}