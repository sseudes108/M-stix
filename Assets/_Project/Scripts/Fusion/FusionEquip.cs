using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionEquip : Fusion {

    public void EquipFusion(Card card1, Card card2){
        StartCoroutine(StartEquipFusionRoutine(card1, card2));
    }

    public IEnumerator StartEquipFusionRoutine(Card card1, Card card2){
        if(card1.GetCardType() == ECardType.Monster){
            var arcane = card2 as CardArcane;
            var arcaneType = arcane.GetArcaneType();

            if(arcaneType != EArcaneType.Equip){
                //Not an equip card
                Debug.Log("The arcane card is not an equip card");
                BattleManager.Instance.Fusion.FusionFailed(arcane, card2);

            }else{
                var monster = card1 as CardMonster;
                var animas = monster.GetAnimas();
                var animaLink = arcane.GetAnimaLink();

                if(animas.Contains(animaLink)){
                    //Sucess
                    (int atkMonster, int defMonster, int lvlMonster) = monster.GetMonsterStats();
                    (int atkMod, int defMod, int lvlMod) = arcane.GetModifiers();

                    int newAtk = atkMonster + atkMod;
                    int newDef = defMonster + defMod;
                    int newLvl = lvlMonster + lvlMod;

                    //Move cards (aproximar as duas)
                    yield return new WaitForSeconds(0.3f);
                    //dissolve arcane card

                    monster.ChangeMonsterStats(newAtk, newDef, newLvl);

                    //Move monster to result position
                    //wait (2f);
                    //Check if the fusion is over

                }else{
                    //Equip card not compatible with the monster
                    Debug.Log("The arcane card is not compatible with the monster");
                    BattleManager.Instance.Fusion.FusionFailed(arcane, monster);
                }
            }
        }else if(card1.GetCardType() == ECardType.Arcane){
            var arcane = card1 as CardArcane;
            var arcaneType = arcane.GetArcaneType();

            if(arcaneType != EArcaneType.Equip){
                //Not an equip card
                Debug.Log("The arcane card is not an equip card");
                BattleManager.Instance.Fusion.FusionFailed(arcane, card2);
            }else{
                var monster = card2 as CardMonster;
                var animas = monster.GetAnimas();
                var animaLink = arcane.GetAnimaLink();

                if(animas.Contains(animaLink)){
                    //Sucess
                    //Implement
                }else{
                    //Equip card not compatible with the monster
                    Debug.Log("The arcane card is not an equip card");
                    BattleManager.Instance.Fusion.FusionFailed(arcane, monster);
                }
            }
        }
    }
}