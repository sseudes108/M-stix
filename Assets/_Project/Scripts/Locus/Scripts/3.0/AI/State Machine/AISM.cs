using UnityEngine;

namespace Mistix{
    public class AISM : StateMachine {
        [SerializeField] private BattleManager _battleManager;
        public AbstractState CurrentState { get; private set; }

        public AIS_01_CardSelection AI_CardSelection { get; private set; }
        public AIS_02_CardStatSel AI_CardStatSelection { get; private set; }
        public AIS_03_BoardPlaceSel AI_BoardPlaceSelection { get; private set; }

        private void Awake() { CreateStates(); }

        public void ChangeState(AbstractState newState){
            if(newState == CurrentState) { return;}

            CurrentState?.Exit();
            CurrentState = newState;
            
            CurrentState.Enter();

            UpdateDebugBattleState(CurrentState);
        }

        private void CreateStates(){
            AI_CardSelection = new(null, this);
            AI_CardStatSelection = new(null, this);
            AI_BoardPlaceSelection = new(null, this);
        }

        private void UpdateDebugBattleState(AbstractState aiPhase) { _battleManager.UpdateDebugAIState(aiPhase.ToString()); }

        public void EndCardSelection(){
            Debug.Log("AISM.cs EndCardSelection()");
            _battleManager.EndCardSelection();
        }
    }
}