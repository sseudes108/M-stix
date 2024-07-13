using UnityEngine;

public class EndPhase : AbstractState{
    public override void Enter(){
        Debug.Log("Enter End Phase");

        Battle.StartCoroutine(Battle.BattleManager.ChangeStateRoutine(3f, Battle.StartPhase));
    }

    public override void Exit(){
        Battle.TurnManager.EndTurn();
    }

    public override string ToString(){
        return "End Phase";
    }
}