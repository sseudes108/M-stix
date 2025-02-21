using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIActor : MonoBehaviour {
        private AIA_CardSelector _cardSelector;
        private AIA_CardStatsSelector _cardStatsSelector;
        private AIA_BoardPlaceSelector _boardPlaceSelector;
        private AIManager _aiManager;
        private Card _resultCard;

        private void Awake() {
            _aiManager = GetComponent<AIManager>();
            CreateActions();
        }

        private void CreateActions(){
            _cardSelector = new(this);
            _cardStatsSelector = new(this);
            _boardPlaceSelector = new(this);
            _boardPlaceSelector.SetBoardPlaces(_aiManager.GetAIPlaces().Item1, _aiManager.GetAIPlaces().Item2);
        }

        //Card Selection
        public void StartCardSelection(){ _cardSelector.StartActionRoutine(); }
        public void CardSelectionFinished(){ _aiManager.CardSelectionFinished(); }
        public void SetSelectedAICards(List<Card> selectedList){ _aiManager.SetSelectedAICards(selectedList); }

        //Card Stat Selection
        public void StartCardStatsSelection(){ _cardStatsSelector.StartActionRoutine(); }
        public void EndAICardStatsSelection(){ _aiManager.EndAICardStatsSelection(); }
        
        //Board Place Selection
        public void StartBoardPlaceSelection(){ _boardPlaceSelector.StartActionRoutine(); }
        
        //Cards In Hand
        public List<Card> GetCardsInAIHand(){ return _aiManager.GetCardsInAIHand(); }

        //Result Card
        public void SetFusionedCard(Card resultCard){ _resultCard = resultCard; }
        public Card GetFusionedCard(){ return _resultCard; }


    }
}