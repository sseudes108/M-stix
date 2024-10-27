// using System.Collections;
// using UnityEngine;

// public class CardEffect : MonoBehaviour {

//     public void ActiveCardEffect(CardArcane arcaneCard){
//         var effectType = arcaneCard.GetArcaneType();
//         switch(effectType){
//             case EArcaneType.Field:
//                 StartCoroutine(FieldEffect(arcaneCard));
//             break;
//             case EArcaneType.DamageToPlayer:
//                 StartCoroutine(DamageOrHealToPlayerEffect(arcaneCard));
//             break;
//         }
//     }

//     private IEnumerator FieldEffect(CardArcane arcaneCard){
//         var animalink = arcaneCard.GetAnimaLink();
//         (int atkMod, int defMod, int lvlMod) = arcaneCard.GetModifiers();

//         var monstersOnField = BattleManager.Instance.BoardPlaceManager.GetAllMonstersOnTheField();

//         if(monstersOnField != null){
//             foreach(var monster in monstersOnField){
//                 if(monster.GetAnima() == animalink){
//                     (int atkMonster, int defMonster, int lvlMonster) = monster.GetMonsterStats();

//                     int newAtk = atkMonster + atkMod;
//                     int newDef = defMonster + defMod;
//                     int newLvl = lvlMonster + lvlMod;

//                     monster.ChangeMonsterStats(newAtk, newDef, newLvl);
//                 }
//             }
//         }

//         DissolveEffectCard(arcaneCard, EArcaneType.Field);

//         yield return new WaitForSeconds(1.1f);
//         BattleManager.Instance.BoardManager.ChangeBattleFieldBackground(arcaneCard.Ilustration);
//     }

//     private IEnumerator DamageOrHealToPlayerEffect(CardArcane arcaneCard){
//         DissolveEffectCard(arcaneCard, EArcaneType.DamageToPlayer);
//         yield return new WaitForSeconds(1.1f);

//         var amount = arcaneCard.GetHealOrDamageAmount();

//         if(arcaneCard.IsPlayerCard()){
//             if(arcaneCard.IsDamageCard()){
//                 BattleManager.Instance.HealthManager.DamageEnemy(amount);
//             }else{
//                 BattleManager.Instance.HealthManager.HealPlayer(amount);
//             }
//         }else{
//             if(arcaneCard.IsDamageCard()){
//                 BattleManager.Instance.HealthManager.DamagePlayer(amount);
//             }else{
//                 BattleManager.Instance.HealthManager.HealEnemy(amount);
//             }
//         }
//     }

//     private void DissolveEffectCard(CardArcane card, EArcaneType arcaneType){
//         StartCoroutine(DissolveEffectCardRoutine(card, arcaneType));
//     }

//     private IEnumerator DissolveEffectCardRoutine(CardArcane card, EArcaneType arcaneType){
//         BattleManager.Instance.BoardPlaceManager.RemoveCardFromBoard(card);

//         card.MoveCard(BattleManager.Instance.CardManager.CardEffectPosition);
        
//         yield return new WaitForSeconds(0.9f);
//         card.Shader.DissolveCard(Color.green);

//         yield return new WaitForSeconds(0.9f);

//         if(arcaneType == EArcaneType.Field){
//             BattleManager.Instance.BatteFieldManager.SetActivatedField(card);
//             yield break;
//         }

//         card.DestroyCard();
//     }
// }