// using System.Diagnostics;
// using UnityEngine;

// public class HandPlayer : Hand {

//     private Transform _offCameraHandPosition;
//     public Vector3 DefaulHandPosition {get; private set;}

//     private void OnEnable() {
//         BPDraw.PlayerDraw += BPDraw_OnPlayerDraw;
//         BPFusion.OnFusionPhaseStart += BPFusion_OnFusionStart;
//     }

//     private void OnDisable() {
//         BPDraw.PlayerDraw -= BPDraw_OnPlayerDraw;
//         BPFusion.OnFusionPhaseStart -= BPFusion_OnFusionStart;
//     }

//     private void Start() {
//         DefaulHandPosition = transform.position;
//     }

//     private void BPDraw_OnPlayerDraw(){
//         DrawCards();
//     }

//     private void BPFusion_OnFusionStart(){
//         MoveHand(_offCameraHandPosition.position);
//     }

//     public void MoveHandToDefaulPosition(){
//         MoveHand(DefaulHandPosition);
//     }

//     protected override void SetHand(){
//         _hand = GetComponent<HandPlayer>();
//     }
//     protected override void SetDeck(){
//         _deck = GetComponentInChildren<Deck>();
//     }

//     protected override void EndDrawPhase(){
//         //Solve the stack overflow excepction (Two hands trying to end the draw phase at the sime time)
//         if(BattleManager.Instance.TurnManager.GetTurn() == 0 || BattleManager.Instance.TurnManager.IsPlayerTurn()){
//             BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.CardSelectionPhase);
//         }
//     }
// }