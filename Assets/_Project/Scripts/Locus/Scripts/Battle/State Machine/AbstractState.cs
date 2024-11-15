public abstract class AbstractState {
    public AbstractState(StateMachine stateMachine){
        StateMachine = stateMachine;
    }

    public StateMachine StateMachine{get; private set;}

    public abstract void Enter();
    public abstract void Exit();

    public virtual void SubscribeEvents(){}
    public virtual void UnsubscribeEvents(){}
}