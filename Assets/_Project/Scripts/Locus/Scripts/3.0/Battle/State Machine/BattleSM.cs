using System;
using UnityEngine;

namespace Mistix{
    public class BattleSM : StateMachine {
        [SerializeField] private BattleManager BattleManager;

        public AbstractState CurrentState { get; private set; }
        
        //States - Phases
        public BS_01_StartPhase StartPhase { get; private set; }
        public BS_02_DrawPhase DrawPhase { get; private set; }
        public BS_03_CardSelectionPhase CardSelectionPhase { get; private set; }
        public BS_04_FusionPhase FusionPhase { get; private set; }

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
        }
    #endregion

    #region Board
        public void LightOffAllPlaces(){ BattleManager.LighOffAllPlaces(); }
        public void LightUpAllPlaces() { BattleManager.LightUpAllPlaces(); }
    #endregion

    #region Turn
        public void UpdateTurn() { BattleManager.UpdateTurn(); }
        public bool IsFirstTurn() { return BattleManager.IsFirstTurn(); }
    #endregion

    #region UI
        public void ResetLifePoints() { BattleManager.ResetLifePoints(); }
        public void ResetDeckCount() { BattleManager.ResetDeckCount(); }
        private void UpdateDebugBattleState(AbstractState state) { BattleManager.UpdateDebugBattleState(state.ToString()); }

        public void MoveUICardOffScreen(){ BattleManager.MoveUICardOffScreen(); }

    #endregion

    #region Cards
        public void DrawCards() { BattleManager.DrawCards(); }
        public void AllowCardSelection() { BattleManager.AllowCardSelection(); }
        public void BlockCardSelection() { BattleManager.BlockCardSelection(); }
    #endregion

    #region Hand
        public bool IsHandFull() { return BattleManager.IsHandFull(); }
        public void CheckPositionsInHand() { BattleManager.CheckPositionsInHand(); }

        public bool IsCardSelectionEnded(){ return BattleManager.IsCardSelectionEnded(); }

        public void MoveHandOffScreen(){ BattleManager.MoveHandOffScreen(); }

        public void StartFusionRoutine(){ BattleManager.StartFusionRoutine(); }

        #endregion

    }
}