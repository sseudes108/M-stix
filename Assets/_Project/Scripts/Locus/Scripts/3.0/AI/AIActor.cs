using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIActor : MonoBehaviour {
        private AIA_CardSelector _cardSelector;
        private AIManager _aiManager;

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
    }
}