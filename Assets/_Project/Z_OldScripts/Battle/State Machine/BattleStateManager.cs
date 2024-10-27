// using UnityEngine;

// public class BattleStateManager : MonoBehaviour {

//     public AbstractBattleState CurrentState {get; private set;}
//     public BPStart StartPhase {get; private set;}
//     public BPDraw DrawPhase {get; private set;}
//     public BPCardSelection CardSelectionPhase {get; private set;}
//     public BPFusion FusionPhase {get; private set;}
//     public BPAfterFusionSelections AfterFusionSelections {get; private set;}
//     public BPBoardPlaceSelection BoardPlaceSelectionPhase {get; private set;}
//     public BPAction ActionPhase {get; private set;}
//     public BPAttack AttackPhase {get; private set;}
//     public BPDamage DamagePhase {get; private set;}
//     public BPActionTwo ActionTwoPhase {get; private set;}
//     public BPEnd EndPhase {get; private set;}


//     private void OnEnable() {
//         AbstractBattleState.OnEndState += AbstractBattleState_OnEndState;
//     }

//     private void OnDisable() {
//         AbstractBattleState.OnEndState -= AbstractBattleState_OnEndState;
//     }

//     private void Awake() { SetStates(); }
//     private void Start() { ChangeState(StartPhase);}
//     private void Update(){ CurrentState.Update(); }

//     public void AbstractBattleState_OnEndState(AbstractBattleState newState){
//         ChangeState(newState);
//     }

//     public void ChangeState(AbstractBattleState newState){
//         CurrentState?.ExitState();
//         CurrentState = newState;
//         CurrentState.SetTurn();
//         CurrentState.EnterState();
//     }

//     private void SetStates(){
//         StartPhase = new BPStart();
//         DrawPhase = new BPDraw();
//         CardSelectionPhase = new BPCardSelection();
//         FusionPhase = new BPFusion();
//         AfterFusionSelections = new BPAfterFusionSelections();
//         BoardPlaceSelectionPhase = new BPBoardPlaceSelection();
//         ActionPhase = new BPAction();
//         AttackPhase = new BPAttack();
//         DamagePhase = new BPDamage();
//         ActionTwoPhase = new BPActionTwo();
//         EndPhase = new BPEnd();
//     }
// }