using UnityEngine;

public class AI_End : AbstractState{
    public override void Enter(){
        Debug.Log("AI End - Enter");
        AI.StartCoroutine(AI.Manager.ChangeStateRoutine(3f, AI.ActionTwo));
    }

    public override void Exit(){
        
    }
}