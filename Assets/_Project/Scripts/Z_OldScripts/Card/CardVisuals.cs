// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CardVisuals : MonoBehaviour {

//     private void OnEnable() {
//         Fusion.OnFusionRoutineStart += Fusion_OnFusionRoutineStart;
//         // Fusion.OnMergeCards += Fusion_OnMergeCards;
//     }

//     private void OnDisable() {
//         Fusion.OnFusionRoutineStart -= Fusion_OnFusionRoutineStart;
//         // Fusion.OnMergeCards -= Fusion_OnMergeCards;
//     }

//     private void Fusion_OnFusionRoutineStart(List<Card> cards){
//         ResetBorderColors(cards);
//     }

//     // private void Fusion_OnMergeCards(List<Card> cards, Color color){
//     //     StartCoroutine(DissolveCardRoutine(cards, color));
//     // }

//     // private IEnumerator DissolveCardRoutine(List<Card> cards, Color color){
//     //     yield return new WaitForSeconds(0.3f);
//     //     DissolveCard(cards,color);
//     // }

//     public void ResetBorderColors(List<Card> cards){
//         foreach(var card in cards){
//             if(card != null){
//                 card.Shader.ResetBoarderColor();
//             }
//         }
//     }
    
//     public void DissolveCard(List<Card> cards, Color color){
//         foreach(var card in cards){
//             card.Shader.DissolveCard(color);
//         }
//     }
    
//     public void DissolveCard(Card card, Color color){
//         card.Shader.DissolveCard(color);
//     }

//     public void SolidifyCard(Card card, Color color){
//         card.Shader.SolidifyCard(color);
//     }

//     public void MakeCardInvisible(Card card){
//         card.Shader.MakeCardCardInvisible();
//     }

    
// }