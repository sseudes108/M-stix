// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// public class AIAgroMonstersFocused : AIArchetype {

//     public override void SelectCard(){
//         if(CardsList.MonstersOnAIField.Count > 2){
//             BattleManager.Instance.AILib.CheckBoardForLowLevelFusion(CardsList);
//         }else{
//             BattleManager.Instance.AILib.StrongestMonsterFusion(CardsList);
//         }
//     }

//     public override int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card CardToPutOnBoard){
//         CardMonster monsterOnBoardToFusion = null;
//         CardMonster monsterToPutOnBoard = CardToPutOnBoard as CardMonster;
//         List<BoardCardMonsterPlace> monstersOnBoard = new();

//         BattleManager.Instance.AIStateManager.CardSelector.AnalyzeMonstersOnField();

//         if(CardsList.MonstersOnAIField.Count > 2){
//             BattleManager.Instance.AILib.CheckBoardFusion(CardsList.MonstersOnAIField, monsterToPutOnBoard);
//         }

//         int positionInBoard;
//         if (BoardFusion){
//             foreach(var boardPlace in monsterBoardPlaces){
//                 var place = boardPlace.GetComponentInChildren<BoardCardMonsterPlace>();
//                 var monster = place.GetCardInThisPlace() as CardMonster;
//                 if(monster != null && monster.GetLevel() == BoardFusionLvl) {
//                     monsterOnBoardToFusion = monster;
//                 }
//                 monstersOnBoard.Add(place);
//             }
//             positionInBoard = monstersOnBoard.IndexOf(monsterOnBoardToFusion.GetComponentInParent<BoardCardMonsterPlace>());
//             BattleManager.Instance.AILib.BoardFusionSetUp(false, 0);
//         }else{
//             //Corrigir - Infinite loop if there's no free place in board
//             do{
//                 positionInBoard = Random.Range(0, monsterBoardPlaces.Count);
//             }while(monsterBoardPlaces[positionInBoard].GetComponentInChildren<BoardCardMonsterPlace>().GetCardInThisPlace() != null);
//         }
//         return positionInBoard;
//     }

//     public override int SelectMonsterMode(CardMonster monster){
//         //0 = atk 1 = def
//         int atk = monster.GetAttack();

//         if(CardsList.PlayerMonstersFaceUp.Count > 0){
//             CardsList.PlayerMonstersFaceUp.Sort((x,y) => y.GetAttack().CompareTo(x.GetAttack()));
//         }

//         foreach(var card in CardsList.PlayerMonstersFaceUp){
//             if(atk >= card.GetAttack()){
//                 return 0;
//             }
//             return 1;
//         }
//         return 0;
//     }

//     public override int SelectCardFace(Card monster){
//         return 1;
//     }

//     public override IEnumerator CheckAttackRoutine(){
//         CheckMonstersOnField(out List<CardMonster> monstersThatCanAttack, out List<CardMonster> targetsInAttack, out List<CardMonster> targetsInDefense);

//         while(monstersThatCanAttack.Count > 0){
//             BoardCardMonsterPlace boardPlaceAttacking = ChooseAttackerAndTarget(monstersThatCanAttack, targetsInAttack, targetsInDefense);

//             if (targetsInAttack.Count > 0){
//                 foreach (var targetMonster in targetsInAttack){
//                     if (monstersThatCanAttack[0].GetAttack() > targetMonster.GetAttack()){
//                         targetsInAttack.Remove(targetMonster);
//                         BattleManager.Instance.ActionsManager.ActionAttack.StartMonsterBattle(boardPlaceAttacking, monstersThatCanAttack[0]);
//                         break;
//                     }
//                 }
//             }else if (targetsInDefense.Count > 0){
//                 foreach (var targetMonster in targetsInDefense){
//                     if (monstersThatCanAttack[0].GetAttack() > targetMonster.GetDefense()){
//                         targetsInDefense.Remove(targetMonster);
//                         BattleManager.Instance.ActionsManager.ActionAttack.StartMonsterBattle(boardPlaceAttacking, monstersThatCanAttack[0]);
//                         break;
//                     }
//                 }
//             }else{
//                 BattleManager.Instance.ActionsManager.ActionAttack.DirectAttack();
//             }
//             monstersThatCanAttack.Remove(monstersThatCanAttack[0]);
//             yield return new WaitForSeconds(5f);
//         }

//         BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.EndPhase);
//     }

//     private static BoardCardMonsterPlace ChooseAttackerAndTarget(List<CardMonster> monstersThatCanAttack, List<CardMonster> targetsInAttack, List<CardMonster> targetsInDefense){
//         monstersThatCanAttack.Sort((x, y) => y.GetAttack().CompareTo(x.GetAttack()));
//         targetsInAttack.Sort((x, y) => y.GetAttack().CompareTo(x.GetAttack()));
//         targetsInDefense.Sort((x, y) => y.GetDefense().CompareTo(x.GetDefense()));

//         BoardCardMonsterPlace boardPlaceAttacked = null;

//         //Nada em atk mais de 0 em def
//         if(targetsInAttack.Count == 0 && targetsInDefense.Count > 0){
//             boardPlaceAttacked = targetsInDefense[0].GetComponentInParent<BoardCardMonsterPlace>();

//             //Mais de um em atk
//         }else if(targetsInAttack.Count > 0){
//             boardPlaceAttacked = targetsInAttack[0].GetComponentInParent<BoardCardMonsterPlace>();

//             //Direct attack
//         }else if(targetsInAttack.Count == 0 && targetsInDefense.Count == 0){
//             var randPlace = Random.Range(0,BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements.Count);
//             boardPlaceAttacked = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements[randPlace];
//         }
        
//         // var boardPlaceAttacked = targetsInAttack[0].GetComponentInParent<BoardCardMonsterPlace>();
//         var boardPlaceAttacking = monstersThatCanAttack[0].GetComponentInParent<BoardCardMonsterPlace>();

//         boardPlaceAttacked.TriggerAttackMonsterEvent();
//         return boardPlaceAttacking;
//     }

//     private static void CheckMonstersOnField(out List<CardMonster> monstersThatCanAttack, out List<CardMonster> targetsInAttack, out List<CardMonster> targetsInDefense){

//         var cardsOnField = BattleManager.Instance.AILib.GetCardsOnField();

//         monstersThatCanAttack = new();
//         targetsInAttack = new();
//         targetsInDefense = new();

//         foreach (var monster in cardsOnField.AIFaceUpMonsters){
//             var boardPlace = monster.GetComponentInParent<BoardCardMonsterPlace>();
//             if(boardPlace != null){
//                 if (boardPlace.CanAttack()){
//                     monstersThatCanAttack.Add(monster);
//                 }
//             }
//         }

//         foreach (var monster in cardsOnField.PlayerFaceUpMonsters){
//             if (monster.IsInAttackMode()){
//                 targetsInAttack.Add(monster);
//             }else{
//                 targetsInDefense.Add(monster);
//             }
//         }
//     }
// }