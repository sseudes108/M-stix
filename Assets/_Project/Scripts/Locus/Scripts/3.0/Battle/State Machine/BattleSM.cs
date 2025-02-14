using System;
using UnityEngine;

namespace Mistix{
    public class BattleSM : StateMachine {
        [SerializeField] private BattleManager _battleManager;

        public AbstractState CurrentState { get; private set; }
        
        //States - Phases
        public BS_01_Start StartPhase { get; private set; }
        public BS_02_Draw DrawPhase { get; private set; }
        public BS_03_CardSelection CardSelectionPhase { get; private set; }
        public BS_04_Fusion FusionPhase { get; private set; }
        public BS_05_CardStatSel CardStatSelPhase { get; private set; }
        public BS_06_BoardPlaceSel BoardPlaceSelPhase { get; private set; }
        public BS_07_Action ActionPhase { get; private set; }
        

    #region Unity Methods
        private void Awake() { CreateStates(); }
        private void Start() { ChangeState(StartPhase); }

    #endregion

    #region States
        public void ChangeState(AbstractState newState){
            if(newState == CurrentState) { return;}

            CurrentState?.Exit();
            CurrentState = newState;
            
            CurrentState.Enter();

            UpdateDebugBattleState(CurrentState);
        }

        private void CreateStates(){
            StartPhase = new(this);
            DrawPhase = new(this);
            CardSelectionPhase = new(this);
            FusionPhase = new(this);
            CardStatSelPhase = new(this);
            BoardPlaceSelPhase = new(this);
            ActionPhase  = new(this);
        }

    #endregion

    #region Board
        public void LightOffAllPlaces(){ _battleManager.LighOffAllPlaces(); }
        public void LightUpAllPlaces() { _battleManager.LightUpAllPlaces(); }

        public void MoveToBoardPlaceSelection(){
            _battleManager.MoveToBoardPlaceSelection();
        }

        public void HighLightPossiblePlaces(){
            _battleManager.HighLightPossiblePlaces();
        }
    #endregion

    #region Turn
        public void UpdateTurn() { _battleManager.UpdateTurn(); }
        public bool IsFirstTurn() { return _battleManager.IsFirstTurn(); }
    #endregion

    #region UI
        public void ResetLifePoints() { _battleManager.ResetLifePoints(); }
        public void ResetDeckCount() { _battleManager.ResetDeckCount(); }
        private void UpdateDebugBattleState(AbstractState state) { _battleManager.UpdateDebugBattleState(state.ToString()); }

        public void MoveUICardOffScreen(){ _battleManager.MoveUICardOffScreen(); }

    #endregion

    #region Cards
        public void DrawCards() { _battleManager.DrawCards(); }
        public void AllowCardSelection() { _battleManager.AllowCardSelection(); }
        public void BlockCardSelection() { _battleManager.BlockCardSelection(); }
    #endregion

    #region Hand
        public bool IsHandFull() { return _battleManager.IsHandFull(); }
        public void CheckPositionsInHand() { _battleManager.CheckPositionsInHand(); }

        public bool IsCardSelectionEnded(){ return _battleManager.IsCardSelectionEnded(); }

        public void MoveHandOffScreen(){ _battleManager.MoveHandOffScreen(); }

        public void StartFusionRoutine(){ _battleManager.StartFusionRoutine(); }

    #endregion
    
    #region Fusion
        public Card GetFusionResultCard(){
            return _battleManager.GetFusionResultCard();
        }
        public bool IsFusionEnded(){ return _battleManager.IsFusionEnded(); }

    #endregion

    #region Card Stats
        public void ShowCardStatOptions(Card card){
            _battleManager.ShowCardStatOptions(card);
        }

        public bool IsAllStatsSelected(){ return _battleManager.IsAllStatsSelected(); }

        public bool BoardPlaceSelected(){ return _battleManager.IsBoardPlaceSelected(); }

        #endregion

    }
}