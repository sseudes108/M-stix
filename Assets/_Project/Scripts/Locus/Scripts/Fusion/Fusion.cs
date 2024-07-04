using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FusionPositions), typeof(MonsterFusion))]
public class Fusion : MonoBehaviour {
    public static Action<MonsterCard, MonsterCard> OnMonsterFusion;
    public static Action OnEquipFusion;
    public static Action<ArcaneCard, ArcaneCard> OnArcaneFusion;
    public static Action<Card, Card> OnFusionFailed;
    public static Action<Card, Card, Card> OnFusionSucess;
    public static Action<Card, bool> OnFusionRoutineFinished;
    public static Action<Card, bool> OnFusionEnd;
    public static Action<List<Card>, bool> OnMergeCards;

    private bool _isPlayerTurn;
    [SerializeField] private List<Card> _fusionLine;
    private Card _resultCard;

    private void OnEnable() {
        FusionPhase.OnStartFusion += FusionPhase_OnStartFusion;
        OnFusionFailed += Fusion_OnFusionFailed;
        OnFusionSucess += Fusion_OnFusionSucess;
    }

    private void OnDisable() {
        FusionPhase.OnStartFusion -= FusionPhase_OnStartFusion;
        OnFusionFailed += Fusion_OnFusionFailed;
        OnFusionSucess += Fusion_OnFusionSucess;
    }

    public void FusionPhase_OnStartFusion(List<Card> selectedCardList, bool isPLayerTurn){
        _isPlayerTurn = isPLayerTurn;
        StartCoroutine(FusionRoutine(selectedCardList));
    }

    private void Fusion_OnFusionSucess(Card card1, Card card2, Card resultCard){
        if(this == null) { return; }
        StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
    }

    public void Fusion_OnFusionFailed(Card card1, Card card2){
        Debug.Log("Fusion_OnFusionFailed Called");
        StartCoroutine(FusionFailedRoutine(card1, card2));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCardList){
        yield return null;
        _fusionLine = selectedCardList;

        if(selectedCardList.Count > 1){
            do{
                _resultCard = null;
                yield return new WaitForSeconds(1f);

                var card1 = selectedCardList[0];
                var card2 = selectedCardList[1];
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
                        OnMonsterFusion?.Invoke(card1 as MonsterCard, card2 as MonsterCard);
                        yield return null;
                        RemoveCardsFromFusionLine(card1, card2);
                        //Time for the Monster fusion Coroutine finish
                        yield return new WaitForSeconds(2f);
                    }else{
                        //Arcane Fusion
                        OnArcaneFusion?.Invoke(card1 as ArcaneCard, card2 as ArcaneCard);
                        yield return null;

                        RemoveCardsFromFusionLine(card1, card2);
                        yield return new WaitForSeconds(2f);
                    }
                }
            }while(selectedCardList.Count > 1);
        }else if(selectedCardList.Count == 1){
            _resultCard = selectedCardList[0];
            yield return null;
            OnFusionRoutineFinished?.Invoke(_resultCard, _isPlayerTurn);
        }
        yield return new WaitForSeconds(2f);
        OnFusionEnd?.Invoke(_resultCard, _isPlayerTurn);
        yield return null;
    }

    private void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);
    }
    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }
    
    //Fusion Failed
    private IEnumerator FusionFailedRoutine(Card card1, Card card2){
        yield return null;
        //Set Result of fusion Card
        _resultCard = card2;
        // _resultCard.SetFusionedCard();

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        OnMergeCards?.Invoke(materials, _isPlayerTurn);

        //Camera Shake
        // if(_isPlayerTurn){
        //     OnFusionFailed?.Invoke();
        // }

        yield return new WaitForSeconds(0.1f);

        //Dissolve the first card
        // card1.Shader.DissolveCard(Color.red);

        yield return new WaitForSeconds(0.5f);

        //Destroy Card
        // card1.DisableModelVisual();
        yield return null;
        // card1.DestroyCard();

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(card2);
        }else{
            OnFusionRoutineFinished?.Invoke(card2, _isPlayerTurn);
            // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
        }
    }

    private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
        //Set Result of fusion Card
        _resultCard = resultCard;

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        OnMergeCards?.Invoke(materials, _isPlayerTurn);

        //Dissolve cards used
        yield return new WaitForSeconds(0.3f);
        foreach(var card in materials){
            card.CardVisual.Dissolve.DissolveCard(Color.green);
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
        OnFusionRoutineFinished?.Invoke(resultCard, _isPlayerTurn);

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(resultCard);
        }
    }

}