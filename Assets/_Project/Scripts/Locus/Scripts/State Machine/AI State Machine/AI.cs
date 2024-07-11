using System.Collections;
using UnityEngine;

public class AI : StateManager {
    public AI_IdleState Idle;
    public AI_DrawPhase Draw;

    public AI(){
        Idle = new AI_IdleState();
        Draw = new AI_DrawPhase();
    }

    private void Start(){
        ChangeState(Idle);
    }

    public IEnumerator WaitRoutine(AbstractState nextPhase){
        yield return new WaitForSeconds(2f);
        yield return null;
        ChangeState(nextPhase);
    }
}