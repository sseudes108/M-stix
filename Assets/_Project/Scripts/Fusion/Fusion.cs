using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {

    public Action OnFusionStart, OnFusionEnd;

    private int _cardsInFusionLine;
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
            yield return new WaitForSeconds(3f);

            var card1 = _fusionLine[0];
            var card2 = _fusionLine[1];

            //Precisa ser arrumado! da forma que está não é póssivel usar cartas de equipe na linha de fusão.
            if(card1.GetCardType() != card2.GetCardType()){
                Debug.Log("Fusion Failed - Types are not equals");
                //Remove Cards From line
                BattleManager.Instance.Fusion.RemoveCardsFromFusionLine(card1, card2);

                //Move the second card position
                BattleManager.Instance.FusionPositions.MoveCardToFirstPositionInlinePos(card2);
                yield return new WaitForSeconds(0.3f);

                //Dissolve the first card
                BattleManager.Instance.FusionVisuals.DissolveCard(card1);
                yield return new WaitForSeconds(0.6f);

                //Check if the line is 0
                if(GetCardsInFusionLine() > 0){
                    AddCardToFusionLine(card2);
                }else{
                    BattleManager.Instance.FusionPositions.FusionFailed(card2);
                }
                yield return new WaitForSeconds(3);
            }

            if(card1.GetCardType() == card2.GetCardType()){
                yield return new WaitForSeconds(waitTime);

                if(card1.GetCardType() == ECardType.Monster){
                    //FusionMonster
                    MonsterFusion(card1 as CardMonster, card2 as CardMonster);
                    RemoveCardsFromFusionLine(card1, card2);

                    yield return new WaitForSeconds(3);

                }else if(card1.GetCardType() == ECardType.Arcane){
                    //FusionArcane
                    ArcaneFusion(card1 as CardArcane, card2 as CardArcane);
                    RemoveCardsFromFusionLine(card1, card2);

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

    public void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);

        _cardsInFusionLine = _fusionLine.Count;
    }

    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }

    public int GetCardsInFusionLine() => _cardsInFusionLine;
}