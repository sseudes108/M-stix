// using System.Collections;
// using UnityEngine;

// public class FusionEquip : Fusion {
//     public void EquipFusion(Card card1, Card card2){
//         bool isEquipCard = true;

//         if(card1.GetCardType() == ECardType.Monster){
//             var arcane = card2 as CardArcane;
//             var monster = card1 as CardMonster;
//             var arcaneType = arcane.GetArcaneType();

//             if(arcaneType != EArcaneType.Equip){
//                 //Failed - Not an equip card
//                 BattleManager.Instance.Fusion.FusionFailed(monster, arcane);

//                 //Block the rest of the routine
//                 isEquipCard = false;
//             }

//             if(isEquipCard){
//                 //Sucess
//                 ModifyMonsterStats(arcane, monster);
//             }

//         }else if(card1.GetCardType() == ECardType.Arcane){
//             var arcane = card1 as CardArcane;
//             var monster = card2 as CardMonster;
//             var arcaneType = arcane.GetArcaneType();

//             if(arcaneType != EArcaneType.Equip){
//                 //Failed - Not an equip card
//                 BattleManager.Instance.Fusion.FusionFailed(arcane, monster);

//                 //Block the rest of the routine
//                 isEquipCard = false;
//             }

//             if(isEquipCard){
//                 //Sucess
//                 ModifyMonsterStats(arcane, monster);
//             }
//         }
//     }

//     private void ModifyMonsterStats(CardArcane arcane, CardMonster monster){
//         StartCoroutine(ModifyMonsterStatsRoutine(arcane, monster));
//     }

//     private IEnumerator ModifyMonsterStatsRoutine(CardArcane arcane, CardMonster monster){
//         var animas = monster.GetAnimas();
//         var animaLink = arcane.GetAnimaLink();

//         if (animas.Contains(animaLink)){
//             //Sucess
//             (int atkMonster, int defMonster, int lvlMonster) = monster.GetMonsterStats();
//             (int atkMod, int defMod, int lvlMod) = arcane.GetModifiers();

//             int newAtk = atkMonster + atkMod;
//             int newDef = defMonster + defMod;
//             int newLvl = lvlMonster + lvlMod;

//             yield return new WaitForSeconds(0.3f);

//             monster.ChangeMonsterStats(newAtk, newDef, newLvl);

//             //Fusion 
//             BattleManager.Instance.Fusion.FusionSucess(arcane, monster);

//         }else{
//             //Equip card not compatible with the monster
//             BattleManager.Instance.Fusion.FusionFailed(arcane, monster);
//         }
//     }
// }