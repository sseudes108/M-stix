using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIActor : MonoBehaviour {
        private AIA_CardSelector _cardSelector;
        private AIA_CardStatsSelector _cardStatsSelector;
        private AIA_BoardPlaceSelector _boardPlaceSelector;

        private AIB_FieldChecker _fieldChecker;
        private AIB_HandChecker _handChecker;
        private AIB_Fusioner _fusioner;

        private AIManager _aiManager;
        private Card _resultCard;

        private bool _isBoardFusion = false;
        private Card _cardOnBoardToFusion;

        private void Awake() {
            _aiManager = GetComponent<AIManager>();

            _fusioner = GetComponent<AIB_Fusioner>();
            _handChecker = GetComponent<AIB_HandChecker>();
            _fieldChecker = GetComponent<AIB_FieldChecker>();

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

        //Hand Checker
        public int Lvl4OnHandCount(){ return _handChecker.Lvl4OnHand.Count; }
        public int Lvl3OnHandCount(){ return _handChecker.Lvl3OnHand.Count; }
        public int Lvl2OnHandCount(){ return _handChecker.Lvl2OnHand.Count; }
        public MonsterCard SelectLvl4Card(int index){ return _handChecker.Lvl4OnHand[index]; }
        public MonsterCard SelectLvl3Card(int index){ return _handChecker.Lvl3OnHand[index]; }
        public MonsterCard SelectLvl2Card(int index){ return _handChecker.Lvl2OnHand[index]; }
        public void OrganizeCardsOnHand(){ _handChecker.OrganizeCardsOnHand(GetCardsInAIHand()); }
        
        //Board Fusion
        public void ReEnterCardSelectionPhase(){ _aiManager.ChangeAISMToCardSelectionPhase(); }
        public Card GetCardOnBoardToFusion(){ return _cardOnBoardToFusion; }
        public bool IsBoardFusion(){ return _isBoardFusion; }
        public void SetBoardFusion(Card cardToFusion){
            _isBoardFusion = true;
            _cardOnBoardToFusion = cardToFusion;
        }

        public void ResetBoardFusion(){
            _isBoardFusion = false;
            if(_cardOnBoardToFusion != null){
                _cardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
                _cardOnBoardToFusion = null;
            }
        }
        
        public void CheckForBoardFusion(MonsterCard cardToPlace){ 
            _fusioner.CheckForBoardMonsterFusion(cardToPlace); 
        }

        public int Lvl7OnAIField(){ return _fieldChecker.Lvl7OnAIField.Count; }
        public int Lvl6OnAIField(){ return _fieldChecker.Lvl6OnAIField.Count; }
        public int Lvl5OnAIField(){ return _fieldChecker.Lvl5OnAIField.Count; }
        public int Lvl4OnAIField(){ return _fieldChecker.Lvl4OnAIField.Count; }
        public int Lvl3OnAIField(){ return _fieldChecker.Lvl3OnAIField.Count; }
        public int Lvl2OnAIField(){ return _fieldChecker.Lvl2OnAIField.Count; }
        public MonsterCard GetLvl7OnField() {return _fieldChecker.Lvl7OnAIField[0];}
        public MonsterCard GetLvl6OnField() {return _fieldChecker.Lvl6OnAIField[0];}
        public MonsterCard GetLvl5OnField() {return _fieldChecker.Lvl5OnAIField[0];}
        public MonsterCard GetLvl4OnField() {return _fieldChecker.Lvl4OnAIField[0];}
        public MonsterCard GetLvl3OnField() {return _fieldChecker.Lvl3OnAIField[0];}
        public MonsterCard GetLvl2OnField() {return _fieldChecker.Lvl2OnAIField[0];}
    }
}