using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {

    public Action OnFusionStart, OnFusionEnd;

    int _cardsInFusionLine;
    [SerializeField] List<Card> _fusionLine;

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

        //Move cards to fusion line
        // BattleManager.Instance.FusionPositions.MoveCardsToPosition(selectedCards);

        _fusionLine.Clear();
        _fusionLine = selectedCards;

        do{
            Debug.Log("Do");
            //Move cards to fusion line
            BattleManager.Instance.FusionPositions.MoveCardsToPosition(selectedCards);

            var card1 = _fusionLine[0];
            var card2 = _fusionLine[1];

            //Precisa ser arrumado! da forma que está não é póssivel usar cartas de equipe na linha de fusão.
            if(card1.GetCardType() != card2.GetCardType()){
                yield return new WaitForSeconds(0.5f);
                BattleManager.Instance.FusionVisuals.DissolveCard(card1);
                Debug.Log("Fusion Failed. Diferent card types");
                // StopAllCoroutines();
            }

            if(card1.GetCardType() == card2.GetCardType()){
                yield return new WaitForSeconds(waitTime);

                if(card1.GetCardType() == ECardType.Monster){
                    //FusionMonster
                    MonsterFusion(card1 as CardMonster, card2 as CardMonster);
                    RemoveCardsFromFusionLine(card1, card2);

                    yield return new WaitForSeconds(5);


                }else if(card1.GetCardType() == ECardType.Arcane){
                    //FusionArcane
                    ArcaneFusion(card1 as CardArcane, card2 as CardArcane);
                    RemoveCardsFromFusionLine(card1, card2);

                    yield return new WaitForSeconds(5);
                }
            }    
        }while(selectedCards.Count > 0);

        // var card1 = selectedCards[0];
        // var card2 = selectedCards[1];

        // //Precisa ser arrumado! da forma que está não é póssivel usar cartas de equipe na linha de fusão.
        // if(card1.GetCardType() != card2.GetCardType()){
        //     yield return new WaitForSeconds(0.5f);
        //     BattleManager.Instance.FusionVisuals.DissolveCard(card1);
        //     Debug.Log("Fusion Failed. Diferent card types");
        //     // StopAllCoroutines();
        // }

        // if(card1.GetCardType() == card2.GetCardType()){
        //     yield return new WaitForSeconds(waitTime);

        //     if(card1.GetCardType() == ECardType.Monster){
        //         //FusionMonster
        //         MonsterFusion(card1 as CardMonster, card2 as CardMonster);
        //         yield return new WaitForSeconds(5);

        //         RemoveCardsFromFusionLine(card1, card2, selectedCards);

        //     }else if(card1.GetCardType() == ECardType.Arcane){
        //         //FusionArcane
        //         ArcaneFusion(card1 as CardArcane, card2 as CardArcane);
        //         yield return new WaitForSeconds(5);

        //         RemoveCardsFromFusionLine(card1, card2, selectedCards);
        //     }
        // }

        // if(selectedCards.Count > 0){
        //     //Restart Fusion();
        //     Debug.Log(selectedCards.Count);
        //     Debug.Log("restart fusion. Do while?");
        // }

        yield return new WaitForSeconds(waitTime);
        OnFusionEnd?.Invoke();
        Debug.Log("Fusion Ended");
    }

    private  void DisableCardColliders(List<Card> selectedCards){
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

    private void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);

        _cardsInFusionLine = _fusionLine.Count;
    }

    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }

    public int CardsInFusionLine() => _cardsInFusionLine;
}