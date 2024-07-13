using System.Collections;
using UnityEngine;

public class AI : StateManager {
    public AIManagerSO Manager;
    public AIActorSO Actor;
    
    public AI_IdleState Idle;
    public AI_BoardPlaceSel BoardPlaceSelection;
    public AI_Action Action;
    public AI_ActionTwo ActionTwo;

    public AbstractState AICurrentState;

    public AI(){
        Idle = new AI_IdleState();
        BoardPlaceSelection = new AI_BoardPlaceSel();
        Action = new AI_Action();
        ActionTwo = new AI_ActionTwo();
    }

    private void Start(){
        Manager.SetAI(this);
        ChangeState(Idle);
    }

    public IEnumerator WaitRoutine(AbstractState nextPhase){
        yield return new WaitForSeconds(2f);
        yield return null;
        ChangeState(nextPhase);
    }

    public void ChangeState(AbstractState newState){
        AICurrentState?.Exit();
        AICurrentState = newState;

        if(AICurrentState.AI == null){
            AICurrentState.SetController(this);
        }

        AICurrentState.Enter();
    }
}