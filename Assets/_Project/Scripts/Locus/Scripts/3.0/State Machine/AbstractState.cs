namespace Mistix{  
    public abstract class AbstractState {
        public AbstractState(BattleSM battleSM){
            BattleSM = battleSM;
        }
        
        public BattleSM BattleSM { get; private set; }

        public abstract void Enter();
        public abstract void Exit();
    }
}