using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIActor : MonoBehaviour {
        private AIA_CardSelector _cardSelector;
        private AIManager _aiManager;
        private Card _resultCard;

        private void Awake() {
            _aiManager = GetComponent<AIManager>();
            CreateActions();
        }

        private void CreateActions(){
            _cardSelector = new(this);
        }

        public void StartCardSelection(){ _cardSelector.StartCardSelectionRoutine(); }

        public List<Card> GetCardsInAIHand(){
            return _aiManager.GetCardsInAIHand();
        }

        public void CardSelectionFinished(){
            _aiManager.CardSelectionFinished();
        }

        public void SetFusionedCard(Card resultCard){
            _resultCard = resultCard;
        }

        public void SetSelectedAICards(List<Card> selectedList){
            _aiManager.SetSelectedAICards(selectedList);
        }
    }
}