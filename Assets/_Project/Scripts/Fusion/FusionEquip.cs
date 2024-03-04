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
                #region Fusion Failed
                    Debug.Log("The arcane card is not an equip card");
                    //Remove Cards From line
                    BattleManager.Instance.Fusion.RemoveCardsFromFusionLine(card1, card2);

                    //Move the second card position
                    BattleManager.Instance.FusionPositions.MoveCardToFirstPositionInlinePos(card2);
                    yield return new WaitForSeconds(0.3f);

                    //Dissolve the first card
                    BattleManager.Instance.FusionVisuals.DissolveCard(card1, Color.red);
                    yield return new WaitForSeconds(0.6f);

                    //Check if the line is 0
                    if(GetCardsInFusionLine() > 0){
                        AddCardToFusionLine(card2);
                    }else{
                        BattleManager.Instance.FusionPositions.FusionFailed(card2);
                    }
                    yield return new WaitForSeconds(3);
                #endregion

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
                    //wait (0.3f);
                    //dissolve arcane card

                    monster.ChangeMonsterStats(newAtk, newDef, newLvl);

                    //Move monster to result position
                    //wait (2f);
                    //Check if the fusion is over

                }else{
                    //Equip card not compatible with the monster
                    #region Fusion Failed
                    Debug.Log("The arcane card is not an equip card");
                    //Remove Cards From line
                    BattleManager.Instance.Fusion.RemoveCardsFromFusionLine(card1, card2);

                    //Move the second card position
                    BattleManager.Instance.FusionPositions.MoveCardToFirstPositionInlinePos(card2);
                    yield return new WaitForSeconds(0.3f);

                    //Dissolve the first card
                    BattleManager.Instance.FusionVisuals.DissolveCard(card1, Color.red);
                    yield return new WaitForSeconds(0.6f);

                    //Check if the line is 0
                    if(GetCardsInFusionLine() > 0){
                        AddCardToFusionLine(card2);
                    }else{
                        BattleManager.Instance.FusionPositions.FusionFailed(card2);
                    }
                    yield return new WaitForSeconds(3);
                    #endregion
                }
            }
        }else if(card1.GetCardType() == ECardType.Arcane){
            var arcane = card1 as CardArcane;
            var arcaneType = arcane.GetArcaneType();

            if(arcaneType != EArcaneType.Equip){
                //Not an equip card
                #region Fusion Failed
                    Debug.Log("The arcane card is not an equip card");
                    //Remove Cards From line
                    BattleManager.Instance.Fusion.RemoveCardsFromFusionLine(card1, card2);

                    //Move the second card position
                    BattleManager.Instance.FusionPositions.MoveCardToFirstPositionInlinePos(card2);
                    yield return new WaitForSeconds(0.3f);

                    //Dissolve the first card
                    BattleManager.Instance.FusionVisuals.DissolveCard(card1, Color.red);
                    yield return new WaitForSeconds(0.6f);

                    //Check if the line is 0
                    if(GetCardsInFusionLine() > 0){
                        AddCardToFusionLine(card2);
                    }else{
                        BattleManager.Instance.FusionPositions.FusionFailed(card2);
                    }
                    yield return new WaitForSeconds(3);
                #endregion

            }else{
                var monster = card2 as CardMonster;
                var animas = monster.GetAnimas();
                var animaLink = arcane.GetAnimaLink();

                if(animas.Contains(animaLink)){
                    //Sucess
                    //Implement
                }else{
                    //Equip card not compatible with the monster
                    #region Fusion Failed
                    Debug.Log("The arcane card is not an equip card");
                    //Remove Cards From line
                    BattleManager.Instance.Fusion.RemoveCardsFromFusionLine(card1, card2);

                    //Move the second card position
                    BattleManager.Instance.FusionPositions.MoveCardToFirstPositionInlinePos(card2);
                    yield return new WaitForSeconds(0.3f);

                    //Dissolve the first card
                    BattleManager.Instance.FusionVisuals.DissolveCard(card1, Color.red);
                    yield return new WaitForSeconds(0.6f);

                    //Check if the line is 0
                    if(GetCardsInFusionLine() > 0){
                        AddCardToFusionLine(card2);
                    }else{
                        BattleManager.Instance.FusionPositions.FusionFailed(card2);
                    }
                    yield return new WaitForSeconds(3);
                    #endregion
                }
            }
        }
    }
}