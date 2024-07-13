using UnityEngine;

public class AI_ActionTwo : AbstractState{
    public override void Enter(){
        Debug.Log("AI Action Two - Enter");
        AI.StartCoroutine(AI.Manager.ChangeStateRoutine(3f, AI.BoardPlaceSelection));
    }

    public override void Exit(){
        
    }
}