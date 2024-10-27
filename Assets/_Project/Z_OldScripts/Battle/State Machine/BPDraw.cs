// using System;

// public class BPDraw : AbstractBattleState {
//     public static Action PlayerDraw;
//     public static Action EnemyDraw;

//     public override void EnterState(){
//         OnEnterState?.Invoke(this);

//         //First Turn
//         if(_currentTurn == 1){
//             PlayerDraw?.Invoke();
//             EnemyDraw?.Invoke();

//         }else if(_isPlayerTurn){
//             PlayerDraw?.Invoke();
//         }else{
//             EnemyDraw?.Invoke();
//         }
//     }

//     public override void ExitState(){}

//     public override void Update(){}

//     public override string ToString(){
//         return "Draw Phase";
//     }
// }