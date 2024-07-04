using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {
    public static Action<MonsterCard, MonsterCard> OnMonsterFusion;
    public static Action OnEquipFusion;
    public static Action OnArcaneFusion;
    public static Action<Card, Card> OnFusionFailed;
    public static Action<Card, bool> OnFusionFinished;

    private bool _isPlayerTurn;
    [SerializeField] private List<Card> _fusionLine;
    private Card _resultCard;

    private void OnEnable() {
        FusionPhase.OnStartFusion += FusionPhase_OnStartFusion;
        OnFusionFailed += Fusion_OnFusionFailed;
    }

    private void OnDisable() {
        FusionPhase.OnStartFusion -= FusionPhase_OnStartFusion;
        OnFusionFailed += Fusion_OnFusionFailed;
    }

    public void FusionPhase_OnStartFusion(List<Card> selectedCardList, bool isPLayerTurn){
        _isPlayerTurn = isPLayerTurn;
        StartCoroutine(FusionRoutine(selectedCardList));
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

                if(cardType1 == cardType2){
                    if(card1 is MonsterCard){
                        //Fusion Monster
                        yield return null;

                    }
                }

            }while(selectedCardList.Count > 0);

        }else{

        }
    }

    private void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);
    }

    //Fusion Failed
    public void Fusion_OnFusionFailed(Card card1, Card card2){
       StartCoroutine(FusionFailedRoutine(card1, card2));
    }

    private IEnumerator FusionFailedRoutine(Card card1, Card card2){
        //Set Result of fusion Card
        _resultCard = card2;
        _resultCard.SetFusionedCard();

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        OnMergeCards?.Invoke(materials);

        //Camera Shake
        // if(_isPlayerTurn){
        //     OnFusionFailed?.Invoke();
        // }

        yield return new WaitForSeconds(0.1f);

        //Dissolve the first card
        card1.Shader.DissolveCard(Color.red);

        yield return new WaitForSeconds(0.5f);

        //Destroy Card
        card1.DisableModelVisual();
        card1.DestroyCard();

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(card2);
        }else{
            OnFusionFinished?.Invoke(card2, _isPlayerTurn);
            // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
        }
    }

    public void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }

}