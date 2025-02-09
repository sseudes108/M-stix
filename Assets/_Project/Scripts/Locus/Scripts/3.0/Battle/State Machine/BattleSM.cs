using UnityEngine;

namespace Mistix{
    public class BattleSM : StateMachine {
        [SerializeField] private BattleManager BattleManager;

        public AbstractState CurrentState { get; private set; }
        
        //States - Phases
        public BS_01_StartPhase StartPhase { get; private set; }
        public BS_02_DrawPhase DrawPhase { get; private set; }
        public BS_03_CardSelectionPhase CardSelectionPhase { get; private set; }

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
    #endregion

    #region Cards
        public void DrawCards() { BattleManager.DrawCards(); }
        public void AllowCardSelection() { BattleManager.AllowCardSelection(); }
    #endregion

    #region Hand
        public bool IsHandFull() { return BattleManager.IsHandFull(); }
        public void CheckPositionsInHand() { BattleManager.CheckPositionsInHand(); }
    #endregion
        
    }
}