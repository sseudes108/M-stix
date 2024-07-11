using UnityEngine;

public class AI_DrawPhase : AbstractState{
    public override void Enter(){
        Debug.Log("AI_DrawPhase");
        // AI.StartCoroutine(AI.WaitRoutine(AI.Idle));
    }

    public override void Exit(){
        
    }
}