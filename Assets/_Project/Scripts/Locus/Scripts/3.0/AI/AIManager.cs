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

        //Card Selection
        public void ChangeAISMToCardSelectionPhase(){ _aiSM.ChangeState(_aiSM.AI_CardSelection); }
        public void StartCardSelection(){ _actor.StartCardSelection(); }
        public void CardSelectionFinished(){ _battleManager.EndCardSelection(); }

        //Card Stat Selection
        public void ChangeAISMToCardStatSelPhase(){ _aiSM.ChangeState(_aiSM.AI_CardStatSelection); }
        public void StartCardStatsSelection(){ _actor.StartCardStatsSelection(); }
        public void EndAICardStatsSelection(){ _battleManager.EndAICardStatsSelection(); }
        
        //Board Place Selection
        public void ChangeAISMToBoardPlaceSelPhase(){ _aiSM.ChangeState(_aiSM.AI_BoardPlaceSelection); }
        public void StartBoardPlaceSelection(){ _actor.StartBoardPlaceSelection(); }
        public (List<BoardPlace>, List<BoardPlace>) GetAIPlaces(){ return _battleManager.GetAIPlaces(); }

        //Cards In Hand and Field
        public List<Card> GetCardsInAIHand(){ return _battleManager.GetCardsInAIHand(); }
        public List<MonsterCard> GetMonstersInField(){ return _battleManager.GetMonstersInAIField(); }

        //Result Card
        public void SetFusionedCard(Card resultCard){ _actor.SetFusionedCard(resultCard); }
        public void SetSelectedAICards(List<Card> selectedList){ _battleManager.SetSelectedAICards(selectedList); }

        //Board Fusion
        public void ChangeBSMToCardSelectionPhase(){
            _battleManager.ChangeBSMToCardSelectionPhase();
        }
    }
}