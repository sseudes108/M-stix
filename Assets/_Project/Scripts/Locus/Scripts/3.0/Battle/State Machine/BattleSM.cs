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
        public BS_08_Effects EffectsPhase { get; private set; }
        public BS_09_Damage DamagePhase { get; private set; }
        public BS_10_Action2 Action2Phase { get; private set; }
        public BS_11_End EndPhase { get; private set; }
        

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
            StartPhase = new(this, null);
            DrawPhase = new(this, null);
            CardSelectionPhase = new(this, null);
            FusionPhase = new(this, null);
            CardStatSelPhase = new(this, null);
            BoardPlaceSelPhase = new(this, null);
            ActionPhase = new(this, null);
            EffectsPhase = new(this, null);
            DamagePhase = new(this, null);
            EndPhase = new(this, null);
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
        public bool BoardPlaceSelected(){ return _battleManager.IsBoardPlaceSelected(); }
        
    #endregion

    #region Turn
        public void UpdateTurn() { _battleManager.UpdateTurn(); }
        public bool IsFirstTurn() { return _battleManager.IsFirstTurn(); }
        public bool IsPlayerTurn(){ return _battleManager.IsPlayerTurn(); }
        public void EndTurn(){ _battleManager.EndTurn(); }
    #endregion

    #region UI
        public void ResetLifePoints() { _battleManager.ResetLifePoints(); }
        public void ResetDeckCount() { _battleManager.ResetDeckCount(); }
        private void UpdateDebugBattleState(AbstractState state) { _battleManager.UpdateDebugBattleState(state.ToString()); }

        public void MoveUICardOffScreen(){ _battleManager.MoveUICardOffScreen(); }
        public bool IsActionSelected(){ return _battleManager.IsActionSelected(); }
        public void ResetActionSelected(){ _battleManager.ResetActionSelected(); }
        public void ShowEndActionButton(){ _battleManager.ShowEndActionButton(); }

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
        public void ShowCardStatOptions(Card card){ _battleManager.ShowCardStatOptions(card); }
        public bool IsAllStatsSelected(){ return _battleManager.IsAllStatsSelected(); }        

    #endregion

    #region Card Stats


        public void ResetCardSelectionEnded(){
            _battleManager.ResetCardSelectionEnded();
        }

        public bool PlayerHasArcaneOnField(){  return _battleManager.PlayerHasArcaneOnField(); }

        public bool EnemyHasArcaneOnField(){  return _battleManager.EnemyHasArcaneOnField(); }

    #endregion

    #region AI
        public void ChangeAISMToCardSelectionPhase(){
            _battleManager.ChangeAISMToCardSelectionPhase();
        }

        public void ChangeAISMToCardStatSelPhase(){
            _battleManager.ChangeAISMToCardStatSelPhase();
        }
    #endregion

    }
}