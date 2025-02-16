namespace Mistix{  
    public abstract class AbstractState {
        public AbstractState(BattleSM battleSM, AISM aiSM){ BattleSM = battleSM; AISM = aiSM; }
        public BattleSM BattleSM { get; private set; }
        public AISM AISM { get; private set; }
        public abstract void Enter();
        public abstract void Exit();
    }
}