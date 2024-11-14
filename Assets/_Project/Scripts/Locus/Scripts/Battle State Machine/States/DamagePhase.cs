public class DamagePhase : AbstractState{
    public DamagePhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){}

    public override void Exit(){}
    
    public override string ToString(){
        return "Damage Phase";
    }
}