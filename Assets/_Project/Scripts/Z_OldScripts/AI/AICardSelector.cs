// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AICardSelector : MonoBehaviour {

//     //AI Hand
//     private List<CardMonster> _lvl1MonstersList;
//     private List<CardMonster> _lvl2MonstersList;
//     private List<CardMonster> _lvl3MonstersList;
//     private List<CardMonster> _lvl4MonstersList;

//     private List<CardArcane> _trapsList;
//     private List<CardArcane> _fieldsList;
//     private List<CardArcane> _equipsList;

//     //monsters On Field
//     private List<CardMonster> _AIMonstersOnField;
//     private List<int> _OnFieldLevels;
//     private List<Card> _cardsInHand;
//     private AICardsList CardsList;

//     //AI
//     public List<CardArcane> _aiArcanesFaceDownOnField;
//     public List<CardArcane> _aiArcanesFaceUpOnField;
//     private List<CardMonster> _aiMonstersFaceUp;
//     private List<CardMonster> _aiMonstersFaceDown;

//     private List<CardMonster> _lvl5MonstersList;
//     private List<CardMonster> _lvl6MonstersList;
//     private List<CardMonster> _lvl7MonstersList;

//     //Player
//     public List<CardArcane> _playerArcanesFaceDownOnField;
//     public List<CardArcane> _playerArcanesFaceUpOnField;
//     private List<CardMonster> _playerMonstersFaceUp;
//     private List<CardMonster> _playerMonstersFaceDown;

//     public void StartCardSelection(){
//         StartCoroutine(SelectCardsInEnemyHand());
//     }

//     private IEnumerator SelectCardsInEnemyHand(){
//         OrganizeCardsFromHand();
//         AnalyzeMonstersOnField();

//         BattleManager.Instance.AIStateManager.CurrentArchetype.SelectCard();

//         yield return new WaitForSeconds(2f);
//         BattleManager.Instance.BattleStateManager.CardSelectionPhase.EndSelection();
//     }

//     public void AnalyzeMonstersOnField(){
//         var (playerMonsterPlaces, aiMonsterPlaces) = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlacesAI();
//         var (playerArcanePlaces, aiArcanePlaces) = BattleManager.Instance.BoardPlaceManager.GetOcuppiedArcanePlacesAI();   

//         //Ai monsters on field
//         _AIMonstersOnField = new();
//         _OnFieldLevels = new();
//         _aiMonstersFaceUp = new();
//         _aiMonstersFaceDown = new();

//         _lvl5MonstersList = new();
//         _lvl6MonstersList = new();
//         _lvl7MonstersList = new();

//         foreach(var card in aiMonsterPlaces){
//             var monster = card.GetCardInThisPlace() as CardMonster;
//             var monsterLvl = monster.GetLevel();
//             _OnFieldLevels.Add(monsterLvl);

//             //Face
//             if(!monster.IsFaceDown()){
//                 _aiMonstersFaceUp.Add(monster);
//             }else{
//                 _aiMonstersFaceDown.Add(monster);
//             }

//             //Organize Levels
//             switch(monsterLvl){
//                 case 1:
//                     _lvl1MonstersList.Add(monster);
//                 break;

//                 case 2:
//                     _lvl2MonstersList.Add(monster);
//                 break;

//                 case 3:
//                     _lvl3MonstersList.Add(monster);
//                 break;

//                 case 4:
//                     _lvl4MonstersList.Add(monster);
//                 break;

//                 case 5:
//                     _lvl5MonstersList.Add(monster);
//                 break;

//                 case 6:
//                     _lvl6MonstersList.Add(monster);
//                 break;

//                 case 7:
//                     _lvl7MonstersList.Add(monster);
//                 break;
//             }

//             _AIMonstersOnField.Add(monster);
//         }

//         //Player monsters on field
//         _playerMonstersFaceUp = new();
//         _playerMonstersFaceDown = new();
//         foreach(var place in playerMonsterPlaces){
//             var monster = place.GetCardInThisPlace() as CardMonster;
//             if(monster != null){
//                 if(!monster.IsFaceDown()){
//                     _playerMonstersFaceUp.Add(monster);
//                 }else{
//                     _playerMonstersFaceDown.Add(monster);
//                 }
//             }
//         }

        
//         foreach(var card in playerArcanePlaces){
//             var arcane = card.GetCardInThisPlace() as CardArcane;
//             if(arcane != null){
//                 if(arcane.IsFaceDown()){
//                     _playerArcanesFaceDownOnField.Add(arcane);
//                 }else{
//                     _playerArcanesFaceUpOnField.Add(arcane);
//                 }
//             }
//         }    
        
        
//         SetMonstersList();
//     }

//     public void OrganizeCardsFromHand(){
//         _cardsInHand = BattleManager.Instance.EnemyHand.GetCardsInHand();

//         _lvl1MonstersList = new();
//         _lvl2MonstersList = new();
//         _lvl3MonstersList = new();
//         _lvl4MonstersList = new();
        
//         _trapsList = new();
//         _fieldsList = new();
//         _equipsList = new();

//         foreach (var card in _cardsInHand){
//             if(card != null){
//                 if (card is CardMonster){
//                     var monster = card as CardMonster;
//                     switch (monster.GetLevel()){
//                         case 1:
//                             _lvl1MonstersList.Add(monster);
//                             break;
//                         case 2:
//                             _lvl2MonstersList.Add(monster);
//                             break;
//                         case 3:
//                             _lvl3MonstersList.Add(monster);
//                             break;
//                         case 4:
//                             _lvl4MonstersList.Add(monster);
//                             break;
//                     }
//                 }else{
//                     var arcane = card as CardArcane;
//                     switch (arcane.GetArcaneType()){
//                         case EArcaneType.Field:
//                             _fieldsList.Add(arcane);
//                             break;
//                         case EArcaneType.Equip:
//                             _equipsList.Add(arcane);
//                             break;
//                         case EArcaneType.Trap:
//                             _trapsList.Add(arcane);
//                             break;
//                     }
//                 }
//             }
//         }

//         SetMonstersList();
//     }

//     private void SetMonstersList(){
//         CardsList = new(){
//         MonstersOnAIField = _AIMonstersOnField,

//         Lvl1MonstersList = _lvl1MonstersList,
//         Lvl2MonstersList = _lvl2MonstersList,
//         Lvl3MonstersList = _lvl3MonstersList,
//         Lvl4MonstersList = _lvl4MonstersList,
//         Lvl5MonstersList = _lvl5MonstersList,
//         Lvl6MonstersList = _lvl6MonstersList,
//         Lvl7MonstersList = _lvl7MonstersList,

//         AIMonstersFaceUp = _aiMonstersFaceUp,
//         AIMonstersFaceDown = _aiMonstersFaceDown,
//         AIArcanesFaceUp = _aiArcanesFaceUpOnField,
//         AIArcanesFaceDown = _aiArcanesFaceDownOnField,

//         PlayerMonstersFaceUp = _playerMonstersFaceUp,
//         PlayerMonstersFaceDown = _playerMonstersFaceDown,
//         PlayerArcanesFaceDown = _playerArcanesFaceDownOnField,
//         PlayerArcanesFaceUp = _playerArcanesFaceUpOnField,

//         OnFieldLevels = _OnFieldLevels

//         };

//         BattleManager.Instance.AIStateManager.CurrentArchetype.SetCardList(CardsList);

//         //DEBUG//
//         UpdateDebugLists(CardsList);
//         //DEBUG//
//     }

//     private void UpdateDebugLists(AICardsList CardsList){
//         Testing.Instance.UpdateLists(CardsList);
//     }
// }