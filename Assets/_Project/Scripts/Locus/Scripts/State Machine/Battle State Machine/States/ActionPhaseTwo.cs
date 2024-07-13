using UnityEngine;

public class ActionPhaseTwo : AbstractState{
    public override void Enter(){
        Debug.Log("ActionPhaseTwo - Enter()");
        Battle.StartCoroutine(Battle.BattleManager.ChangeStateRoutine(2f, Battle.EndPhase));
    }

    public override void Exit(){
        
    }
    
    public override string ToString(){
        return "Action Two";
    }
}