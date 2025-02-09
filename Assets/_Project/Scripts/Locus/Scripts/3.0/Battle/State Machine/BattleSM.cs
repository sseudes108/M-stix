using System;
using UnityEngine;

namespace Mistix{
    public class BattleSM : StateMachine {
        [SerializeField] private BattleManager BattleManager;

        public AbstractState CurrentState { get; private set; }
        
        //States - Phases
        public BS_01_StartPhase StartPhase { get; private set; }
        public BS_02_DrawPhase DrawPhase { get; private set; }
        public BS_03_CardSelection CardSelectionPhase { get; private set; }

        private void Awake() { CreateStates(); }

        private void Start() { ChangeState(StartPhase); }

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
        }

        public void LightOffAllPlaces(){ BattleManager.LighOffAllPlaces(); }
        public void LightUpAllPlaces() { BattleManager.LightUpAllPlaces(); }
        public void UpdateTurn() { BattleManager.UpdateTurn(); }
        public void ResetLifePoints() { BattleManager.ResetLifePoints(); }
        public void ResetDeckCount() { BattleManager.ResetDeckCount(); }

        private void UpdateDebugBattleState(AbstractState state){
            BattleManager.UpdateDebugBattleState(state.ToString());
        }

        public void CheckPositionsInHand() { BattleManager.CheckPositionsInHand(); }

        public void DrawCards() { BattleManager.DrawCards(); }

        public void AllowCardSelection(){
            BattleManager.AllowCardSelection();
        }
    }
}