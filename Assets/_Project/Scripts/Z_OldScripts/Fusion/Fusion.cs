// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Fusion : MonoBehaviour {
//     public static Action<List<Card>> OnFusionRoutineStart; //Used to reset the color borders of the selected cards
//     public static Action<List<Card>> OnFusion; //Called if more than one card is selected
//     public static Action<CardMonster, CardMonster> OnMonsterFusion;
//     public static Action<CardArcane, CardArcane> OnArcaneFusion;
//     public static Action<Card, Card> OnEquipFusion;
//     public static Action<List<Card>> OnMergeCards;
//     public static Action OnFusionFailed;
//     public static Action<Card, bool> OnFusionFinished;

//     [SerializeField] private Card _resultCard;
//     [SerializeField] private List<Card> _fusionLine;

//     private bool _isPlayerTurn;

//     private IEnumerator _fusionRoutine;

//     #region Custom Methods Methods

//     public void SetCoroutine(IEnumerator routine) {
//         _fusionRoutine = routine;
//     }

//     public void StartFusionRoutine(List<Card> selectedCards){
//         _isPlayerTurn = BattleManager.Instance.TurnManager.IsPlayerTurn();
//         StartCoroutine(FusionRoutine(selectedCards));
//     }

//     private IEnumerator FusionRoutine(List<Card> selectedCards){
//         yield return null;
//         float waitTime = 2f;

//         //Reset Border card Colors
//         OnFusionRoutineStart?.Invoke(selectedCards);
//         yield return null;

//         _fusionLine = selectedCards;

//         if(selectedCards.Count > 1){
//             do{
//                 _resultCard = null;
                
//                 //Move cards to fusion line positions
//                 OnFusion?.Invoke(selectedCards);

//                 yield return new WaitForSeconds(waitTime/3);

//                 var card1 = _fusionLine[0];
//                 var card2 = _fusionLine[1];

//                 //Types Not Equals (arcane x monster / monster x arcane)
//                 if(card1.GetCardType() != card2.GetCardType()){
//                     //FusionEquip
//                     OnEquipFusion?.Invoke(card1, card2);
//                     yield return null;

//                     RemoveCardsFromFusionLine(card1, card2);

//                     //Time for the Equip fusion Coroutine finish
//                     yield return new WaitForSeconds(waitTime);
//                 }

//                 //Type Equals (monster x monster / arcane x arcane)
//                 if(card1.GetCardType() == card2.GetCardType()){
//                     yield return new WaitForSeconds(waitTime);

//                     if(card1.GetCardType() == ECardType.Monster){
//                         //FusionMonster
//                         OnMonsterFusion?.Invoke(card1 as CardMonster, card2 as CardMonster);
//                         yield return null;
                        
//                         RemoveCardsFromFusionLine(card1, card2);

//                         //Time for the Monster fusion Coroutine finish
//                         yield return new WaitForSeconds(waitTime);
//                     }else if(card1.GetCardType() == ECardType.Arcane){
//                         //FusionArcane
//                         OnArcaneFusion?.Invoke(card1 as CardArcane, card2 as CardArcane);
//                         yield return null;

//                         RemoveCardsFromFusionLine(card1, card2);

//                         //Time for the Arcane fusion Coroutine finish
//                         yield return new WaitForSeconds(waitTime);
//                     }
//                 }
//             }while(selectedCards.Count > 0);
//         }else if(selectedCards.Count == 1){
//             //Caso tenha apenas uma carta na lista o resultado será ela
//             _resultCard = selectedCards[0];
//             yield return null;
//             OnFusionFinished?.Invoke(_resultCard, _isPlayerTurn);
//             // _resultCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosistion());
//         }

//         AbstractBattleState.OnEndState?.Invoke(BattleManager.Instance.AfterFusionSelections);
//         yield return null;
//     }

//     //Fusion Line
//     public void RemoveCardsFromFusionLine(Card card1, Card card2){
//         _fusionLine.Remove(card1);
//         _fusionLine.Remove(card2);
//     }

//     public void AddCardToFusionLine(Card cardToAdd){
//         _fusionLine.Insert(0, cardToAdd);
//     }

//     public int GetCardsInFusionLine() => _fusionLine.Count;

//     //Fusion Result
//     public Card GetResultCard() => _resultCard;

//     //Fusion process
//     //Fusion Failed
//     public void FusionFailed(Card card1, Card card2){
//        StartCoroutine(FusionFailedRoutine(card1, card2));
//     }

//     private IEnumerator FusionFailedRoutine(Card card1, Card card2){
//         //Set Result of fusion Card
//         _resultCard = card2;
//         _resultCard.SetFusionedCard();

//         //Cards used in fusion
//         var materials = new List<Card>() {card1, card2};

//         //Move cards
//         OnMergeCards?.Invoke(materials);

//         //Camera Shake
//         if(_isPlayerTurn){
//             OnFusionFailed?.Invoke();
//         }

//         yield return new WaitForSeconds(0.1f);

//         //Dissolve the first card
//         card1.Shader.DissolveCard(Color.red);

//         yield return new WaitForSeconds(0.5f);

//         //Destroy Card
//         card1.DisableModelVisual();
//         card1.DestroyCard();

//         //Check if the line is 0
//         if(GetCardsInFusionLine() > 0){
//             AddCardToFusionLine(card2);
//         }else{
//             OnFusionFinished?.Invoke(card2, _isPlayerTurn);
//             // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
//         }
//     }

//     //Monster Fusion Sucess
//     public void FusionSucess(Card card1, Card card2, Card resultCard){
//         StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
//     }

//     private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
//         //Set Result of fusion Card
//         _resultCard = resultCard;

//         //Cards used in fusion
//         var materials = new List<Card>() {card1, card2};

//         //Move cards
//         OnMergeCards?.Invoke(materials);

//         //Dissolve cards used
//         yield return new WaitForSeconds(0.3f);
//         card1.Shader.DissolveCard(Color.green);
//         card2.Shader.DissolveCard(Color.green);

//         //Destroy Cards
//         yield return new WaitForSeconds(0.7f);
//         card1.DestroyCard();
//         card2.DestroyCard();

//         //Set Card Owner
//         if(_isPlayerTurn){
//             resultCard.SetPlayerCard();
//         }

//         //Move fusioned card to position
//         OnFusionFinished?.Invoke(resultCard, _isPlayerTurn);
//         // resultCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosistion());

//         //Check if the line is 0
//         if(GetCardsInFusionLine() > 0){
//             AddCardToFusionLine(resultCard);
//         }
//     }

//     //Equip Fusion Sucess
//     public void FusionSucess(CardArcane card1, CardMonster card2){
//         StartCoroutine(FusionSucessRoutine(card1, card2));
//     }

//     private IEnumerator FusionSucessRoutine(CardArcane arcane, CardMonster monster){
//         //Set Result of fusion Card
//         _resultCard = monster;

//         //Cards used in fusion
//         List<Card> materials = new(){arcane, monster};

//         //Move cards
//         OnMergeCards?.Invoke(materials);
        

//         //Dissolve arcane card used
//         yield return new WaitForSeconds(0.3f);
//         arcane.Shader.DissolveCard(Color.green);
//         monster.Shader.DissolveCard(Color.green);

//         //Destroy Cards
//         yield return new WaitForSeconds(0.7f);
//         arcane.DestroyCard();

//         //Set Card Owner
//         if(_isPlayerTurn){
//             monster.SetPlayerCard();
//         }

//         //Move fusioned card to position
//         OnFusionFinished?.Invoke(monster, _isPlayerTurn);
//         // monster.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosistion());

//         //Check if the line is 0
//         if(GetCardsInFusionLine() > 0){
//             AddCardToFusionLine(monster);
//         }
//     }

// #endregion

// }