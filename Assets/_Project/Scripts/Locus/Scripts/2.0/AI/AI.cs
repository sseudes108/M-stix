// public class AI : StateMachine{
//     public AIActor Actor { get; private set; }
//     public AbstractState CurrentState { get; private set; }

//     public AISelectCardState CardSelect { get; private set; }
//     public AICardStatSelState CardStatSelect { get; private set; }
//     public AIBoardPlaceSelState BoardPlaceSelect { get; private set; }
//     public AIActionSelState ActionSelect { get; private set; }

//     private void Awake() { Actor = GetComponent<AIActor>(); }

//     public AI(){
//         CardSelect ??= new(this);
//         CardStatSelect ??=  new(this);
//         BoardPlaceSelect ??= new(this);
//         ActionSelect ??= new(this);
//     }

//     public void ChangeState(AbstractState newState){
//         CurrentState?.Exit();
//         CurrentState = newState;
//         CurrentState?.Enter();
        
//         TesterUI.Instance.UpdateAIStateText(CurrentState.ToString());
//     }
// }