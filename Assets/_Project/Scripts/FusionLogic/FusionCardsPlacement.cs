using System.Collections.Generic;
using Mistix;
using UnityEngine;

public class FusionCardsPlacement : MonoBehaviour{
    [SerializeField] private Transform _playerHand, _enemyHand;
    
    [Header("Player")]
    [SerializeField] private Transform _playerResultCardPosition;    
    [SerializeField] private Transform _playerCard1InLinePosition, _playerCard2InLinePosition;

    [Header("Enemy")]
    [SerializeField] private Transform _enemyResultCardPosition; 
    [SerializeField] private Transform _enemyCard1InLinePosition, _enemyCard2InLinePosition;
    private Transform _parent;

    private Vector3 _playerHandStartPosition, _enemyHandStartPosition;

    private void OnEnable() {
        BattleManager.Instance.TurnSystem.OnTurnEnd += TurnSystem_OnTurnEnd;
        BattleManager.Instance.Fusion.OnFusionStarted += Fusion_OnFusionStarted;
        BattleManager.Instance.Fusion.OnFusionEnded += Fusion_OnFusionEnded;
    }

    private void OnDisable() {
        BattleManager.Instance.Fusion.OnFusionStarted -= Fusion_OnFusionStarted;
        BattleManager.Instance.Fusion.OnFusionEnded -= Fusion_OnFusionEnded;
    }

    private void Start() {
        //Defalt hand Positions
        _playerHandStartPosition = _playerHand.position;
        _enemyHandStartPosition = _enemyHand.position;
    }


    //Move The position of hand to off the screen when fusion starts
    private void Fusion_OnFusionStarted(){
        Debug.Log("Fusion_OnFusionStarted Invoked");

        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            Vector3 targetPosition = new(0.72f,-1f,-3f);
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = new(-4.2f, -0.9f, 12f);
            _enemyHand.GetComponent<Hand>().MoveHand(targetPosition);
        }
    }
    
    private void Fusion_OnFusionEnded(){
        // Debug.Log("Fusion_OnFusionEnded Invoked");

    }

    //Move The position of hand to default in the draw phase
    private void DrawPhaseStarts(){
        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            Vector3 targetPosition = _playerHandStartPosition;
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = _enemyHandStartPosition;
            _enemyHand.GetComponent<Hand>().MoveHand(targetPosition);
        }
    }

    //Move and organize the cards in fusion line position
    public void MoveSelectedCardsToPosition(List<Card> selectedCards){

        var card1TargetPosition = new Vector3();
        var card2TargetPosition = new Vector3();
        var card1TargetRotation = new Quaternion();
        var card2TargetRotation = new Quaternion();

        var cardIndex = 0;

        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            card1TargetPosition = _playerCard1InLinePosition.position;
            card2TargetPosition = _playerCard2InLinePosition.position;
            
            card1TargetRotation = _playerCard1InLinePosition.rotation;
            card2TargetRotation = _playerCard2InLinePosition.rotation;
            _parent = _playerCard1InLinePosition;
        }else{
            card1TargetPosition = _enemyCard1InLinePosition.position;
            card2TargetPosition = _enemyCard2InLinePosition.position;
            
            card1TargetRotation = _enemyCard1InLinePosition.rotation;
            card2TargetRotation = _enemyCard2InLinePosition.rotation;
            _parent = _enemyCard1InLinePosition;
        }

        foreach(var card in selectedCards){
            card.GetComponent<Collider>().enabled = false;
            if(cardIndex == 0){
                card.transform.SetParent(_parent);
                card.MoveCard(card1TargetPosition, card1TargetRotation);

            }else if(cardIndex == 1){
                card.transform.SetParent(_parent);
                card.MoveCard(card2TargetPosition, card2TargetRotation);

            }else{
                card.transform.SetParent(_parent);
                
                var offsetPosition = 0.3f * cardIndex;
                Vector3 finalPosition = new(card2TargetPosition.x + offsetPosition, card2TargetPosition.y, card2TargetPosition.z);

                card.MoveCard(finalPosition, card2TargetRotation);
            }
            cardIndex++;
        }
    }

    public void MoveResultCardToPosition(Card resultCard){
        var resultCardPosition = new Vector3();
        var resultCardRotation = new Quaternion();

        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            resultCardPosition = _playerResultCardPosition.position;
            resultCardRotation = _playerResultCardPosition.rotation;
            _parent = _playerResultCardPosition;
        }else{
            resultCardPosition = _enemyResultCardPosition.position;
            resultCardRotation = _enemyResultCardPosition.rotation;
            _parent = _enemyResultCardPosition;
        }

        resultCard.MoveCard(resultCardPosition, resultCardRotation);

        resultCard.transform.SetParent(_parent);
    }

    private void TurnSystem_OnTurnEnd(){
        Debug.Log("TurnSystem_OnTurnEnd");
        DrawPhaseStarts();
    }
}