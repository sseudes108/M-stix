using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIActor : MonoBehaviour {
        private AIA_CardSelector _cardSelector;
        private AIA_CardStatsSelector _cardStatsSelector;
        private AIManager _aiManager;
        private Card _resultCard;

        private void Awake() {
            _aiManager = GetComponent<AIManager>();
            CreateActions();
        }

        private void CreateActions(){
            _cardSelector = new(this);
            _cardStatsSelector = new(this);
        }

        public void StartCardSelection(){ _cardSelector.StartActionRoutine(); }
        public void StartCardStatsSelection(){ _cardStatsSelector.StartActionRoutine(); }

        public List<Card> GetCardsInAIHand(){ return _aiManager.GetCardsInAIHand(); }

        public void CardSelectionFinished(){ _aiManager.CardSelectionFinished(); }
        public void EndAICardStatsSelection(){ _aiManager.EndAICardStatsSelection(); }

        public void SetFusionedCard(Card resultCard){ _resultCard = resultCard; }
        public Card GetFusionedCard(){ return _resultCard; }

        public void SetSelectedAICards(List<Card> selectedList){ _aiManager.SetSelectedAICards(selectedList); }

    }
}