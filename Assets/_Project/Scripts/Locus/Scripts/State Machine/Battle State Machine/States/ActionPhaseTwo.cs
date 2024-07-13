using UnityEngine;

public class ActionPhaseTwo : AbstractState{
    public override void Enter(){
        Debug.Log("ActionPhaseTwo - Enter()");
        Battle.StartCoroutine(Battle.Helper.ChangeStateRoutine(2f, Battle.EndPhase, Battle));
    }

    public override void Exit(){
        
    }
    
    public override string ToString(){
        return "Action Two";
    }
}