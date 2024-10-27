// using System.Collections;
// using UnityEngine;

// public class BPEnd : AbstractBattleState {
//     public override void EnterState(){
//         OnEnterState?.Invoke(this);
//         Wait();
//     }

//     public override void ExitState(){
        
//     }

//     public override void Update(){
        
//     }

//     public void Wait(){
//         BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
//     }

//     private IEnumerator WaitRoutine(){
//         // Debug.Log("Wainting End phase");
//         yield return new WaitForSeconds(3);

//         if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             BattleManager.Instance.AIStateManager.ChangeState(BattleManager.Instance.AIStandBy);
//         }
        
//         BattleManager.Instance.TurnManager.EndTurn();
//         BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.StartPhase);
//     }
// }