using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIActor : MonoBehaviour {
        private AIA_CardSelector _cardSelector;
        private AIA_CardStatsSelector _cardStatsSelector;
        private AIA_BoardPlaceSelector _boardPlaceSelector;

        private AIB_FieldChecker _fieldChecker;
        private AIB_HandChecker _handChecker;

        private AIManager _aiManager;
        private Card _resultCard;

        private bool _isBoardFusion = false;

        private void Awake() {
            _aiManager = GetComponent<AIManager>();
            _fieldChecker = GetComponent<AIB_FieldChecker>();
            _handChecker = GetComponent<AIB_HandChecker>();
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

        //Field Checker
        public void OrganizeAIMonsterCardsOnField(){
            _fieldChecker.OrganizeAIMonsterCardsOnField(_aiManager.GetMonstersInField());
        }
        public bool IsBoardFusion(){return _isBoardFusion;}
        public void SetBoardFusion(bool isBoardFusion){_isBoardFusion = isBoardFusion;}

        //Hand Checker
        public int Lvl4OnHandCount(){ return _handChecker.Lvl4OnHand.Count; }
        public int Lvl3OnHandCount(){ return _handChecker.Lvl3OnHand.Count; }
        public int Lvl2OnHandCount(){ return _handChecker.Lvl2OnHand.Count; }
        public MonsterCard SelectLvl4Card(int index){ return _handChecker.Lvl4OnHand[index]; }
        public MonsterCard SelectLvl3Card(int index){ return _handChecker.Lvl3OnHand[index]; }
        public MonsterCard SelectLvl2Card(int index){ return _handChecker.Lvl2OnHand[index]; }

        internal void OrganizeCardsOnHand(){ _handChecker.OrganizeCardsOnHand(GetCardsInAIHand()); }
    }
}