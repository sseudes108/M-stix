// using System;
// using System.Collections;
// using UnityEngine;

// public class BPStart : AbstractBattleState{
//     public static Action OnBattleStart;
//     public override void EnterState(){
//         OnEnterState?.Invoke(this);

//         if(_currentTurn == 1){
//             StartBattle();
//         }else{
//             Wait();
//         }
//     }

//     public override void Update(){}
    
//     public override void ExitState(){}

//     public void StartBattle(){
//         BattleManager.Instance.StartCoroutine(InitializationRoutine());
//     }

//     private IEnumerator InitializationRoutine(){
//         yield return new WaitForSeconds(1f);
//         OnBattleStart?.Invoke();
        
//         yield return new WaitForSeconds(1.5f);
//         OnEndState?.Invoke(BattleManager.Instance.DrawPhase);
//     }

//     public void Wait(){
//         BattleManager.Instance.StartCoroutine(WaitRoutine());
//     }

//     private IEnumerator WaitRoutine(){
//         if(_isPlayerTurn){//Only called after the first turn. Move the hand back to view
//             BattleManager.Instance.PlayerHand.MoveHandToDefaulPosition();
//         }

//         yield return new WaitForSeconds(1.5f);
//         OnEndState?.Invoke(BattleManager.Instance.DrawPhase);
//     }

//     public override string ToString(){
//         return "Start Phase";
//     }
// }
