using System.Collections;
public class AICardStatSelState : AbstractState{
    public AICardStatSelState(StateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        StateMachine.AI.StartCoroutine(AIRoutine(StateMachine.Battle.FusionManager.ResultCard));
    }

    public override void Exit(){}

    public IEnumerator AIRoutine(Card resultCard){
        yield return StateMachine.Battle.StartCoroutine(StateMachine.AI.Actor.CardStatSelector.SelectCardStats(resultCard));
        yield return null;
    }
}