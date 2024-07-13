using UnityEngine;

public class AI_BoardPlaceSel : AbstractState{
    public override void Enter(){
        Debug.Log("AI Board Place Selection State - Enter");
        AI.StartCoroutine(AI.Manager.ChangeStateRoutine(3f, AI.Action));
    }

    public override void Exit(){
        
    }
}