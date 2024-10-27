// using System;
// using System.Collections.Generic;

// public class BPFusion : AbstractBattleState {

//     public static Action OnFusionPhaseStart;
//     public static Action<List<Card>> OnFusionStart;

//     public override void EnterState() {
//         OnEnterState?.Invoke(this);

//         if(_isPlayerTurn){
//             OnFusionPhaseStart?.Invoke();
//         }

//         var selectedlist = BattleManager.Instance.FusionManager.GetFusionList();
//         foreach(var card in selectedlist){
//             card.SetCardOnHand(false);
//         }

//         // OnFusionStart?.Invoke(selectedlist);
//         BattleManager.Instance.Fusion.StartFusionRoutine(selectedlist);
//     }

//     public override void ExitState() {}

//     public override void Update() {}

//     public override string ToString() {
//         return "Fusion";
//     }
// }