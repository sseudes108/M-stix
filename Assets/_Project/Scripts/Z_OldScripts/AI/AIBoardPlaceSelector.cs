// using System.Collections;
// using UnityEngine;

// public class AIBoardPlaceSelector : MonoBehaviour {

//     private void OnEnable() {
//         BPBoardPlaceSelection.OnBoardPlaceSelectionStart += BattlePhaseBoardPlaceSelection_OnBoardPlaceSelection;
//     }

//     private void OnDisable() {
//         BPBoardPlaceSelection.OnBoardPlaceSelectionStart += BattlePhaseBoardPlaceSelection_OnBoardPlaceSelection;
//     }

//     private void BattlePhaseBoardPlaceSelection_OnBoardPlaceSelection(Card card){
//         StartBoardPlaceSelection(card);
//     }

//     public void StartBoardPlaceSelection(Card resultCard){
//         StartCoroutine(BoardPlaceSelectionRoutine(resultCard));
//     }

//     private IEnumerator BoardPlaceSelectionRoutine(Card resultCard){
//         var monsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
//         var arcanePlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlaces;
//         BoardCardPlace boarderPlace;

//         if(resultCard is CardMonster){
//             boarderPlace = monsterPlaces[BattleManager.Instance.AIStateManager.CurrentArchetype.SelectMonsterPlaceOnBoard(monsterPlaces, resultCard)].GetComponent<BoardCardMonsterPlace>();
//         }else{
//             //Random Placement if its not board fusion
//             boarderPlace = arcanePlaces[Random.Range(0,5)].GetComponent<BoardCardArcanePlace>();
//         }
//         //

//         yield return new WaitForSeconds(1f);

//         if(boarderPlace.IsFree()){
//             boarderPlace.SetCardInPlace(resultCard);
//         }else{
//             boarderPlace.BoardFusion(resultCard);
//         }

//         //Atualiza os montros em campo no debug
//         BattleManager.Instance.AIStateManager.CardSelector.AnalyzeMonstersOnField();
//         BattleManager.Instance.AIStateManager.CardSelector.OrganizeCardsFromHand();
//     }
// }