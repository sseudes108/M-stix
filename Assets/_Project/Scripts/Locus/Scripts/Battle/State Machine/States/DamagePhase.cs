public class DamagePhase : AbstractState{
    public DamagePhase(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        StateMachine.Battle.BattleLogic.SetBattleCards(StateMachine.BattleManager.AttackerMonster, StateMachine.BattleManager.DeffenderMonster);
        StateMachine.Battle.BattleLogic.StartBattleRoutine();
    }

    public override void Exit(){}
    
    public override string ToString(){
        return "Damage Phase";
    }
}