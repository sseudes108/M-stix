using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Fusion : MonoBehaviour {

    public Action OnFusionStart, OnFusionEnd;
    
    [SerializeField] private List<Card> _fusionLine;

    public void StartFusionRoutine(List<Card> selectedCards){
        StartCoroutine(FusionRoutine(selectedCards));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards){
        float waitTime = 2f;

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
                yield return new WaitForSeconds(waitTime);
            }

            //Type Equals (monster x monster / arcane x arcane)
            if(card1.GetCardType() == card2.GetCardType()){
                yield return new WaitForSeconds(waitTime);

                if(card1.GetCardType() == ECardType.Monster){
                    //FusionMonster
                    MonsterFusion(card1 as CardMonster, card2 as CardMonster);
                    RemoveCardsFromFusionLine(card1, card2);

                    //Time for the Monster fusion Coroutine finish
                    yield return new WaitForSeconds(waitTime);

                }else if(card1.GetCardType() == ECardType.Arcane){
                    //FusionArcane
                    ArcaneFusion(card1 as CardArcane, card2 as CardArcane);
                    RemoveCardsFromFusionLine(card1, card2);

                    //Time for the Arcane fusion Coroutine finish
                    yield return new WaitForSeconds(waitTime);
                }
            }
              
        }while(selectedCards.Count > 0);

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


    public void FusionFailed(Card card1, Card card2){
       StartCoroutine(FusionFailedRoutine(card1, card2));
    }
    private IEnumerator FusionFailedRoutine(Card card1, Card card2){
        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        BattleManager.Instance.FusionPositions.MergeCards(materials);

        //Dissolve the first card
        BattleManager.Instance.FusionVisuals.DissolveCard(card1, Color.red);
        yield return new WaitForSeconds(0.5f);

        //Momentaneo, apenas para testar a visualização da card no UI - 
        //Ativar o colider apenas quando a card for adicionada um board place
            card2.EnableCollider();
        //

        //Check if the line is 0
        if(BattleManager.Instance.Fusion.GetCardsInFusionLine() > 0){
            BattleManager.Instance.Fusion.AddCardToFusionLine(card2);
        }else{
            BattleManager.Instance.FusionPositions.FusionFailed(card2);
        }

        Debug.Log("Corrigir! Fusion Failed - Fusion - Destroy Card");
        card1.DisableModelVisual();
        card1.gameObject.SetActive(false);
    }

    //Monster Fusion Sucess
    public void FusionSucess(Card card1, Card card2, Card resultCard){
        StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
    }
    private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        BattleManager.Instance.FusionPositions.MergeCards(materials);

        yield return new WaitForSeconds(0.4f);
        //Dissolve cards used
        BattleManager.Instance.FusionVisuals.DissolveCard(materials, Color.green);

        //Set Card Owner
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            resultCard.SetPlayerCard();
        }

        resultCard.EnableCollider();

        //Move fusioned card to position
        resultCard.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosition);

        //Check if the line is 0
        if(GetCardsInFusionLine() > 0){
            AddCardToFusionLine(resultCard);
        }
    }

    //Equip Fusion Sucess
    public void FusionSucess(CardArcane card1, CardMonster card2){
        StartCoroutine(FusionSucessRoutine(card1, card2));
    }
    private IEnumerator FusionSucessRoutine(CardArcane arcane, CardMonster monster){
        //Cards used in fusion
        List<Card> materials = new(){arcane, monster};

        //Move cards
        BattleManager.Instance.FusionPositions.MergeCards(materials);

        yield return new WaitForSeconds(0.4f);
        //Dissolve arcane card used
        BattleManager.Instance.FusionVisuals.DissolveCard(arcane, Color.green);

        //Set Card Owner
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            monster.SetPlayerCard();
        }

        //Momentaneo, apenas para testar a visualização da card no UI - 
        //Ativar o colider apenas quando a card for adicionada um board place
            monster.EnableCollider();
        //

        //Move fusioned card to position
        monster.MoveCard(BattleManager.Instance.FusionPositions.ResultCardPosition);

        //Check if the line is 0
        if(GetCardsInFusionLine() > 0){
            AddCardToFusionLine(monster);
        }
    }
}