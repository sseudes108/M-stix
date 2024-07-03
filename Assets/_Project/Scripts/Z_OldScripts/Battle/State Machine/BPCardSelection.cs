// using System;

// public class BPCardSelection : AbstractBattleState {
//     public static Action OnCardSelectionStart;
//     public static Action OnCardSelectionEnd;
//     public override void EnterState(){
//         OnEnterState?.Invoke(this);
        
//         if(_isPlayerTurn){
//             OnCardSelectionStart?.Invoke();
//         }else{
//             OnAIStateChange?.Invoke(BattleManager.Instance.AICardSelection);
//         }
//     }

//     public override void ExitState(){}

//     public override void Update(){}

//     public void EndSelection(){
//         OnCardSelectionEnd?.Invoke();
//         OnEndState?.Invoke(BattleManager.Instance.FusionPhase);
//     }

//     public override string ToString(){
//         return "Card Sel. Phase";
//     }
// }