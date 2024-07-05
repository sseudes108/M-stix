using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {
    // public static Action<MonsterCard, MonsterCard> OnMonsterFusion;
    public static Action OnEquipFusion;
    // public static Action<ArcaneCard, ArcaneCard> OnArcaneFusion;
    // public static Action<Card, Card> OnFusionFailed;
    // public static Action<Card, Card, Card> OnFusionSucess;
    // public static Action<Card, bool> OnFusionRoutineFinished;
    public static Action OnFusionEnd;
    // public static Action<List<Card>, bool> OnMergeCards;

    public bool _isPlayerTurn;
    public List<Card> _fusionLine;
    public List<Card> _selectedCardList;
    public Card ResultCard;

    // private void OnEnable() {
    //     FusionPhase.OnStartFusion += FusionPhase_OnStartFusion;
    //     // OnFusionFailed += Fusion_OnFusionFailed;
    // }

    // private void OnDisable() {
    //     FusionPhase.OnStartFusion -= FusionPhase_OnStartFusion;
    //     // OnFusionFailed += Fusion_OnFusionFailed;
    // }

    // public void FusionPhase_OnStartFusion(List<Card> selectedCardList, bool isPLayerTurn){
    //     // Debug.Log("Fusion - FusionPhase_OnStartFusion");
    //     _isPlayerTurn = isPLayerTurn;
    // }

    // public void Fusion_OnFusionFailed(Card card1, Card card2){
    //     StartCoroutine(FusionFailedRoutine(card1, card2));
    // }

    public void SetFusionSettings(List<Card> selectedCardList, bool isPLayerTurn){
        Debug.Log("Fusion - SetFusionSettings");
        _fusionLine.Clear();
        _selectedCardList.Clear();
        _isPlayerTurn = isPLayerTurn;
        _selectedCardList = selectedCardList;
    }

    public void StartFusionRoutine(){
        StartCoroutine(FusionRoutine(_selectedCardList));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCardList){
        Debug.Log("Fusion - FusionRoutine Started");

        foreach(var card in selectedCardList){
            card.Visuals.Border.ResetBorderColor();
        }
        yield return null;
        
        _fusionLine = selectedCardList;

        if(selectedCardList.Count > 1){
            do{
                ResultCard = null;
                GameManager.Instance.Fusion.Positions.MoveCardsToFusionPosition(selectedCardList, _isPlayerTurn);

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
        }else if(_selectedCardList.Count == 1){
            ResultCard = null;
            ResultCard = _selectedCardList[0];
            yield return null;
            GameManager.Instance.Fusion.Positions.MoveCardToResultPosition(ResultCard, _isPlayerTurn);
        }
        yield return new WaitForSeconds(1f);
        // Open UI Select options
        Debug.Log("Fusion - OnFusionEnd Invoked");
        OnFusionEnd?.Invoke();
        // GameManager.Instance.UI.CardStats.FusionEnded(ResultCard);
        // if(ResultCard != null){
        //     OnFusionEnd?.Invoke(ResultCard);
        // }
        // yield return null
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
        ResultCard = null;
        ResultCard = card2;
        // _resultCard.SetFusionedCard();

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        // OnMergeCards?.Invoke(materials, _isPlayerTurn);

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
            // OnFusionRoutineFinished?.Invoke(card2, _isPlayerTurn);
            // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
        }
    }

    public void FusionSucess(Card card1, Card card2, Card resultCard){
        StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
    }

    private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
        // Debug.Log("FusionSucessRoutine");
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

        Debug.Log("Fusion - FusionSucessRoutine End");
    }
}