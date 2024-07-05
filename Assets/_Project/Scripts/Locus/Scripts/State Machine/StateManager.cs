using UnityEngine;

public class StateManager : MonoBehaviour {
    public AbstractState CurrentState {get; private set;}
    public int CurrentTurn {get; private set;}

    public virtual void ChangeState(AbstractState newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.SetController(this);
        CurrentState.SetTurnOwner();
        CurrentState.SetResultCard();
        CurrentState.Enter();
    }
}