using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {

    public Action OnFusionStart, OnFusionEnd;
    
    [SerializeField] private List<Card> _fusionLine;

    public void StartFusionRoutine(List<Card> selectedCards){
        StartCoroutine(FusionRoutine(selectedCards));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards){
        float waitTime = 1f;

        //Move hand off camera
        OnFusionStart?.Invoke();

        //Disable Card Colliders
        DisableCardColliders(selectedCards);

        //Reset Border card Colors
        BattleManager.Instance.FusionVisuals.ResetBorderColors(selectedCards);

        _fusionLine.Clear();
        _fusionLine = selectedCards;

        do{
            //Move cards to fusion line positions
            BattleManager.Instance.FusionPositions.MoveCardToPosition(selectedCards);
            yield return new WaitForSeconds(1f);

            var card1 = _fusionLine[0];
            var card2 = _fusionLine[1];

            //Types Not Equals (arcane x monster / monster x arcane)
            if(card1.GetCardType() != card2.GetCardType()){
                //FusionEquip
                EquipeFusion(card1, card2);                
                RemoveCardsFromFusionLine(card1, card2);

                //Time for the Equip fusion Coroutine finish
                yield return new WaitForSeconds(3);
            }

            //Type Equals (monster x monster / arcane x arcane)
            if(card1.GetCardType() == card2.GetCardType()){
                yield return new WaitForSeconds(waitTime);

                if(card1.GetCardType() == ECardType.Monster){
                    //FusionMonster
                    MonsterFusion(card1 as CardMonster, card2 as CardMonster);
                    RemoveCardsFromFusionLine(card1, card2);


                    //Time for the Monster fusion Coroutine finish
                    yield return new WaitForSeconds(3);

                }else if(card1.GetCardType() == ECardType.Arcane){
                    //FusionArcane
                    ArcaneFusion(card1 as CardArcane, card2 as CardArcane);
                    RemoveCardsFromFusionLine(card1, card2);

                    //Time for the Arcane fusion Coroutine finish
                    yield return new WaitForSeconds(3);
                }
            }
              
        }while(selectedCards.Count > 0);

        yield return new WaitForSeconds(0.5f);
        OnFusionEnd?.Invoke();
        Debug.Log("Fusion Ended");
    }

    private void DisableCardColliders(List<Card> selectedCards){
        foreach(var card in selectedCards){
            card.DisableCollider();
        }
    }

    private void MonsterFusion(CardMonster monster1, CardMonster monster2){
        BattleManager.Instance.FusionMonster.MonsterFusion(monster1, monster2);
    }
    private void ArcaneFusion(CardArcane arcane1, CardArcane arcane2){
        BattleManager.Instance.FusionArcane.ArcaneFusion(arcane1, arcane2);
    }
    private void EquipeFusion(Card card1, Card card2){
        BattleManager.Instance.FusionEquip.EquipFusion(card1, card2);
    }

    public void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);
    }

    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }

    public int GetCardsInFusionLine() => _fusionLine.Count;
}