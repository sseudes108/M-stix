using System;
using UnityEngine;

namespace Mistix{
    public class CardManager : MonoBehaviour {
        [SerializeField] private CardCreator _creator;
        [SerializeField] private CardSelector _selector;

        [SerializeField] private BattleManager _battleManager;

        private void Awake() {
            _creator = GetComponent<CardCreator>();
            _selector = GetComponent<CardSelector>();
        }

        public Card DrawCard(ScriptableObject cardData){
            return _creator.CreateCard(cardData);
        }

        public void SelectCard(Card selectedCard){
            _selector.AddToSelectedList(selectedCard);
        }

        public void DeselectCard(Card deselectedCard){
            _selector.RemoveFromSelectedList(deselectedCard);
        }

        public void ShowEndSelectionButton(){
            _battleManager.ShowEndSelectionButton();
        }

        public void HideEndSelectionButton(){
            _battleManager.HideEndSelectionButton();
        }

        public void UpdateCardUilustration(Texture2D illustration){
            _battleManager.UpdateCardUilustration(illustration);
        }
    }
}