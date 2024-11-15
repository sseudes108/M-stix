public class AttackSelectionPhase : AbstractState{
    public AttackSelectionPhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){}

    public override void Exit(){}
    
    public override string ToString(){
        return "Attack Selection";
    }
}