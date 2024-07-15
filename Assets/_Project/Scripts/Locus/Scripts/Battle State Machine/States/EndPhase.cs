public class EndPhase : AbstractState{
    public override void Enter(){
        Battle.StartCoroutine(Battle.BattleManager.ChangeStateRoutine(3f, Battle, Battle.StartPhase));
    }

    public override void Exit(){
        Battle.TurnManager.EndTurn();
    }

    public override string ToString(){
        return "End Phase";
    }
}