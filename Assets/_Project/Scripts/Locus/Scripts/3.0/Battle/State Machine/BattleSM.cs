using UnityEngine;

namespace Mistix{    
    public class BattleSM : StateMachine {
        [SerializeField] private BattleManager BattleManager;

        public AbstractState CurrentState { get; private set; }
        
        //States - Phases
        public BS_01_StartPhase StartPhase { get; private set; }

        private void Awake() {
            CreateStates();
        }

        private void Start() {
            ChangeState(StartPhase);
        }

        public void ChangeState(AbstractState newState){
            if(newState == CurrentState) { return;}

            CurrentState?.Exit();
            CurrentState = newState;
            
            CurrentState.Enter();
        }

        private void CreateStates(){
            StartPhase = new(this, BattleManager);
        }
    }
}