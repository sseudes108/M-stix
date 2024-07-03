using UnityEngine;

public class StateManager : MonoBehaviour {
    public AbstractState CurrentState {get; private set;}
    public int CurrentTurn {get; private set;}
    
    private void Update() {
        CurrentState.LogicUpdate();
    }

    public virtual void ChangeState(AbstractState newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.SetController(this);
        CurrentState.SetTurnOwner(GameManager.Instance.TurnManager.GetCurrentTurn());
        CurrentState.Enter();
    }
}