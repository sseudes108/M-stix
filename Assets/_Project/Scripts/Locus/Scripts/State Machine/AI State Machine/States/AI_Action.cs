using UnityEngine;

public class AI_Action : AbstractState{
    public override void Enter(){
        Debug.Log("AI  State - Enter");
        AI.StartCoroutine(AI.Manager.ChangeStateRoutine(3f, AI.ActionTwo));
    }

    public override void Exit(){
        
    }
}