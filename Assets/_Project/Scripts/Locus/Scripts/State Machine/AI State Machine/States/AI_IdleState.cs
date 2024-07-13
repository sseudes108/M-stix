using UnityEngine;

public class AI_IdleState : AbstractState{
    public override void Enter(){
        Debug.Log("AI_IdleState");
        // AI.StartCoroutine(AI.WaitRoutine(AI.Draw));
    }

    public override void Exit(){

    }
}