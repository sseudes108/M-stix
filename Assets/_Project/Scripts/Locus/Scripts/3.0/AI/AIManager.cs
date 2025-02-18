using System;
using System.Collections.Generic;
using UnityEngine;
namespace Mistix{

    public class AIManager : MonoBehaviour{
        private AISM _aiSM;
        private AIActor _actor;
        [SerializeField] private BattleManager _battleManager;

        private void Awake() {
            _aiSM = GetComponent<AISM>();
            _actor = GetComponent<AIActor>();
        }

        public void ChangeAISMToCardSelectionPhase(){ _aiSM.ChangeState(_aiSM.AI_CardSelection); }
        public void ChangeAISMToCardStatSelPhase(){ _aiSM.ChangeState(_aiSM.AI_CardStatSelection); }

        public void StartCardSelection(){
            _actor.StartCardSelection();
        }

        public List<Card> GetCardsInAIHand(){
            return _battleManager.GetCardsInAIHand();
        }

        public void CardSelectionFinished(){
            _battleManager.EndCardSelection();
        }

        public void SetFusionedCard(Card resultCard){
            _actor.SetFusionedCard(resultCard);
        }

        public void SetSelectedAICards(List<Card> selectedList){
            _battleManager.SetSelectedAICards(selectedList);
        }
    }
}