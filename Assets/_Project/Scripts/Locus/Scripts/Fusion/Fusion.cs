using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FusionPositions))]
public class Fusion : MonoBehaviour {
    [SerializeField] protected FusionManagerSO _fusionManager;
    [SerializeField] protected BattleManagerSO _battleManager;
    [SerializeField] protected CardManagerSO _cardManager;
    [SerializeField] protected CameraManagerSO _cameraManager;
    [SerializeField] protected BoardManagerSO _boardManager;
    [SerializeField] protected AIManagerSO _aIManager;
    
    private bool _isPlayerTurn;
    private List<Card> _fusionLine;
    private Card _resultCard;
    private bool _isBoardFusion;

    private void OnEnable() {
        _fusionManager.OnFusionStart.AddListener(FusionManager_OnFusionStart);
        _fusionManager.OnFusionSucess.AddListener(FusionManager_OnFusionSucess);
        _fusionManager.OnFusionFailed.AddListener(FusionManager_OnFusionFailed);
        _boardManager.OnBoardFusion.AddListener(BoardManager_OnBoardFusion);
    }

    private void OnDisable() {
        _fusionManager.OnFusionStart.RemoveListener(FusionManager_OnFusionStart);
        _fusionManager.OnFusionSucess.RemoveListener(FusionManager_OnFusionSucess);
        _fusionManager.OnFusionFailed.RemoveListener(FusionManager_OnFusionFailed);
        _boardManager.OnBoardFusion.RemoveListener(BoardManager_OnBoardFusion);
    }

#region Events

    private void FusionManager_OnFusionStart(List<Card> selectedCards, bool isPlayerTurn){
        StartFusionRoutine(selectedCards, isPlayerTurn);
    }

    private void FusionManager_OnFusionSucess(Card card1, Card card2, Card resultCard){
        FusionSucess(card1, card2, resultCard);
    }

    private void FusionManager_OnFusionFailed(Card card1, Card card2){
        FusionFailed(card1, card2);
    }

    private void BoardManager_OnBoardFusion(){
        _isBoardFusion = true;
    }

#endregion

#region Fusion

    private void StartFusionRoutine(List<Card> selectedCards, bool isPlayerTurn){
        StartCoroutine(FusionRoutine(selectedCards, isPlayerTurn));
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards, bool isPlayerTurn){        
        _isPlayerTurn = isPlayerTurn;
        _fusionLine = selectedCards;
        _resultCard = null;

        ResetCards(_fusionLine);

        if(_fusionLine.Count > 1){
            do{
                _fusionManager.Positions.MoveCardsToFusionPosition(_fusionLine, _isPlayerTurn);
    
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
                        _fusionManager.StartMonsterFusionRoutine(card1 as MonsterCard, card2 as MonsterCard);
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
                yield return new WaitForSeconds(1f);

            }while(_fusionLine.Count > 1);

        }else if(_fusionLine.Count == 1){

            _resultCard = _fusionLine[0];

            yield return null;
            _fusionManager.Positions.MoveCardToResultPosition(_resultCard, _isPlayerTurn);
        }

        if(!_isPlayerTurn){
            _aIManager.SetFusionedCard(_resultCard);
        }
    
        yield return new WaitForSeconds(1f);

        //Board Fusion
        if(_isBoardFusion){
            _resultCard.SetCardAsFusioned();
            _resultCard.ResetCardStats();
            _isBoardFusion = false;
        }

        // Open UI Select options
        _fusionManager.SetResultCard(_resultCard);
        _fusionManager.FusionEnd(_resultCard);

        _fusionLine.Clear();
    }

    private void RemoveCardsFromFusionLine(Card card1, Card card2){
        _fusionLine.Remove(card1);
        _fusionLine.Remove(card2);
    }

    private void AddCardToFusionLine(Card cardToAdd){
        _fusionLine.Insert(0, cardToAdd);
    }

#endregion

#region Fusion Failed

    private void FusionFailed(Card card1, Card card2){
        StartCoroutine(FusionFailedRoutine(card1, card2));
    }

    private IEnumerator FusionFailedRoutine(Card card1, Card card2){
        yield return null;
        //Set Result of fusion Card
        _resultCard = null;
        _resultCard = card2;

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        _fusionManager.Positions.MoveCardsToMergePosition(materials, _isPlayerTurn);

        // Camera Shake
        if(_isPlayerTurn){
            _cameraManager.CamShake();
        }

        yield return new WaitForSeconds(0.05f);

        //Dissolve the first card
        card1.Visuals.Dissolve.DissolveCard(Color.red);

        yield return new WaitForSeconds(0.5f);

        //Destroy Card
        card1.Visuals.DisableRenderer();
        yield return null;
        card1.DestroyCard();

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(card2);
        }else{
            // OnFusionRoutineFinished?.Invoke(card2, _isPlayerTurn);
            // BattleManager.Instance.FusionPositions.MoveCardToResultPosition(card2);
        }
    }

#endregion

#region Fusion Sucess
    private void FusionSucess(Card card1, Card card2, Card resultCard){
        StartCoroutine(FusionSucessRoutine(card1, card2, resultCard));
    }

    private IEnumerator FusionSucessRoutine(Card card1, Card card2, Card resultCard){
        //Set Result of fusion Card
        _resultCard = null;
        _resultCard = resultCard;

        //Cards used in fusion
        var materials = new List<Card>() {card1, card2};

        //Move cards
        _fusionManager.Positions.MoveCardsToMergePosition(materials, _isPlayerTurn);

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
            resultCard.SetPlayerCard();
        }

        //Move fusioned card to position
        _fusionManager.Positions.MoveCardToResultPosition(resultCard, _isPlayerTurn);

        //Check if the line is 0
        if(_fusionLine.Count > 0){
            AddCardToFusionLine(resultCard);
        }
    }

#endregion

#region Custom Methods
    private void ResetCards(List<Card> selectedcards){
        foreach(var card in selectedcards){
            if(card.IsOnHand){
                card.SetCardOnHand(false);
            }
            card.SetHandPositionFree();
            card.Visuals.Border.ResetBorderColor();
        }
    }
#endregion

}