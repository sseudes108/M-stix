// using System.Collections;
// using UnityEngine;

// public class AIAttackSelector : AIAction {
//     // public AIAttackSelector(AIActorSO actor, AICardOrganizer organizer, AIManagerSO manager) { 
//     //     _actor = actor;
//     //     _cardOrganizer = organizer;
//     //     _manager = manager;
//     // }

//     public IEnumerator SelectAttackRoutine(){
//         Debug.Log("SelectAttackRoutine()");
//         yield return null;

//         // if(_actor.FieldChecker.AIMonstersThatCanAttack.Count > 0){
//         //     _actor.FieldChecker.AIMonstersThatCanAttack[0].SetCanAttack(false);
//         // }else{

//         // }

//         // if(_actor.MonstersOnAIFieldThatCanAttack.Count > 0){
//         //     _actor.MonstersOnAIFieldThatCanAttack[0].SetCanAttack(false);
//         //     _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.ActionSelect);
//         // }else{
            
//         // }

//         if(_cardOrganizer.AIMonstersOnFieldThatCanAttack.Count > 0){
//             _cardOrganizer.AIMonstersOnFieldThatCanAttack[0].SetCanAttack(false);
//             _manager.AI.ChangeState(_manager.AI.ActionSelect);
//         }else{
            
//         }

//     }
// }