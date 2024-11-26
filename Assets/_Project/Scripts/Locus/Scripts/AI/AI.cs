public class AI : StateMachine{
    public AIActor Actor { get; private set; }
    //States
    public AbstractState CurrentState { get; private set; }

    public AISelectCardState CardSelect { get; private set; }
    public AICardStatSelState CardStatSelect { get; private set; }
    public AIBoardPlaceSelState BoardPlaceSelect { get; private set; }
    public AIActionSelState ActionSelect { get; private set; }

    private void Awake() { Actor = GetComponent<AIActor>(); }

    public AI(){
        CardSelect ??= new(this);
        CardStatSelect ??=  new(this);
        BoardPlaceSelect ??= new(this);
        ActionSelect ??= new(this);
    }

    public void ChangeState(AbstractState newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
        
        TesterUI.Instance.UpdateAIStateText(CurrentState.ToString());
    }
}





// // using UnityEngine;

// // public class AI : StateMachine {
// //     [field:SerializeField] public AIManagerSO Manager { get; private set; }
// //     [field:SerializeField] public AIActorSO Actor { get; private set; }
// //     [field:SerializeField] public AICardOrganizer CardOrganizer { get; private set; }

// //     public AbstractState CurrentState { get; private set; }

// //     //States
// //     public AISelectCardState CardSelect { get; private set; }
// //     public AICardStatSelState CardStatSelect { get; private set; }
// //     public AIBoardPlaceSelState BoardPlaceSelect { get; private set; }
// //     public AIActionSelState ActionSelect { get; private set; }

// //     public AI(){
// //         CardSelect ??= new(this);
// //         CardStatSelect ??=  new(this);
// //         BoardPlaceSelect ??= new(this);
// //         ActionSelect ??= new(this);
// //     }

// //     private void Start(){
// //         Manager.SetAI(this);
// //         Actor.SetAI(this);
// //         // Manager.SetCardOrganizer(_AICardOrganizer);
// //         // Actor.SetAIMAnager(Manager);
// //         Actor.Fusioner.ResetBoardFusion();
// //     }

// //     public void ChangeState(AbstractState newState){
// //         CurrentState?.Exit();
// //         CurrentState = newState;
// //         CurrentState?.Enter();
        
// //         TesterUI.Instance.UpdateAIStateText(CurrentState.ToString());
// //     }

// //     // public void SetAIOnBoardLists(List<Card> aICardsOnField, List<Card> playerCardsOnField){
// //     //     Actor.SetAICardLists(aICardsOnField, playerCardsOnField);
// //     // }
// // }