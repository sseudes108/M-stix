using UnityEngine;

public class AI : StateMachine {
    [field:SerializeField] public AIManagerSO Manager {get; private set;}
    [field:SerializeField] public AIActorSO Actor {get; private set;}

    public AbstractState CurrentState;

    public AISelectCardState CardSelect {get; private set;}
    public AICardStatSelState CardStatSelect {get; private set;}
    public AIBoardPlaceSelState BoardPlaceSelect {get; private set;}

    public AI(){
        CardSelect = new(this);
        CardStatSelect = new(this);
        BoardPlaceSelect = new(this);
    }

    private void Start(){
        Manager.SetAI(this);
    }

    public void ChangeState(AbstractState newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
        
        TesterUI.Instance.UpdateAIStateText(CurrentState.ToString());
    }
}