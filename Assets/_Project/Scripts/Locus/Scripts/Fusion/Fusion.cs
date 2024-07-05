using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {

    public static Action OnFusionEnd;

    public bool _isPlayerTurn;
    public List<Card> _fusionLine;
    public List<Card> _selectedCardList;
    public Card ResultCard;

    public void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
        StartCoroutine(FusionRoutine(selectedCards, isPlayerTurn));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
        _isPlayerTurn = isPlayerTurn;
        _fusionLine = selectedCards;
        GameManager.Instance.Fusion.Positions.MoveCardsToFusionPosition(_fusionLine, _isPlayerTurn);

        if(_fusionLine.Count > 1){
            do{
                ResultCard = null;
                GameManager.Instance.Fusion.Positions.MoveCardsToFusionPosition(_fusionLine, _isPlayerTurn);

                yield return new WaitForSeconds(1f);

                var card1 = _fusionLine[0];
                var card2 = _fusionLine[1];
                var cardType1 = card1.GetType();
                var cardType2 = card2.GetType();

                //Types Not Equals (arcane x monster / monster x arcane)
                if(cardType1 != cardType2){
                    //Equip Fusion
                    RemoveCardsFromFusionLine(card1, card2);
                    //Time for the Equip fusion Coroutine finish
                    yield return new WaitForSeconds(2f);
                    yield break;
                }

                //Equal types
                if(cardType1 == cardType2){
                    //Monster x Monster
                    yield return new WaitForSeconds(2f);
                    if(card1 is MonsterCard){
                        //Fusion Monster
                        GameManager.Instance.Fusion.Monster.StartFusionRoutine(card1 as MonsterCard, card2 as MonsterCard);
                        yield return null;
                        RemoveCardsFromFusionLine(card1, card2);
                        //Time for the Monster fusion Coroutine finish
                        yield return new WaitForSeconds(2f);
                    }else{
                        //Arcane Fusion
                        yield return null;
                        RemoveCardsFromFusionLine(card1, card2);
                        yield return new WaitForSeconds(2f);
                    }
                }
            }while(_fusionLine.Count > 0);
        }else if(_fusionLine.Count == 1){
            ResultCard = null;
            ResultCard = _fusionLine[0];
            yield return null;
            GameManager.Instance.Fusion.Positions.MoveCardToResultPosition(ResultCard, _isPlayerTurn);
        }
        yield return new WaitForSeconds(1f);
        // Open UI Select options
        OnFusionEnd?.Invoke();
    }

    private void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);
    }
    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }
    
    //Fusion Failed
    public void FusionFailed(Card card1, Card card2){
        StartCoroutine(FusionFailedRoutine(card1, card2));
    }

    private IEnumerator FusionFailedRoutine(Card card1, Card card2){
        yield return null;
        //Set Result of fusion Card
        ResultCard = null;
        ResultCard = card2;

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        GameManager.Instance.Fusion.Positions.MoveCardsToMergePosition(materials, _isPlayerTurn);

        //Camera Shake
        if(_isPlayerTurn){
            GameManager.Instance.Camera.Shake();
        }

        yield return new WaitForSeconds(0.05f);

        //Dissolve the first card
        card1.Visuals.Dissolve.DissolveCard(Color.red);

        yield return new WaitForSeconds(0.5f);

        //Destroy Card
        card1.Visuals.DisableRenderer();
        yield return null;
        // card1.DestroyCard();
        card1.DestroyCard();

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(card2);
        }else{
            // OnFusionRoutineFinished?.Invoke(card2, _isPlayerTurn);
            // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
        }
    }

    public void FusionSucess(Card card1, Card card2, Card resultCard){
        StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
    }

    private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
        //Set Result of fusion Card
        ResultCard = null;
        ResultCard = resultCard;

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        GameManager.Instance.Fusion.Positions.MoveCardsToMergePosition(materials, _isPlayerTurn);

        //Dissolve cards used
        yield return new WaitForSeconds(0.3f);
        foreach(var card in materials){
            card.Visuals.Dissolve.DissolveCard(Color.green);
        }

        //Destroy Cards
        yield return new WaitForSeconds(0.7f);
        foreach(var card in materials){
            card.DestroyCard();
        }

        //Set Card Owner
        if(_isPlayerTurn){
            resultCard.IsPlayeCard();
        }

        //Move fusioned card to position
        GameManager.Instance.Fusion.Positions.MoveCardToResultPosition(resultCard, _isPlayerTurn);

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(resultCard);
        }
    }
}