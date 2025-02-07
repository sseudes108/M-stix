namespace Mistix{  
    public abstract class AbstractState {
        public AbstractState(StateMachine stateMachine, BattleManager battleManager){
            StateMachine = stateMachine;
            BattleManager = battleManager;
        }
        
        public StateMachine StateMachine { get; private set; }
        public BattleManager BattleManager { get; private set; }

        public abstract void Enter();
        public abstract void Exit();
    }
}