using System.Collections.Generic;
using Mistix;
using UnityEngine;

public class FusionCardsPlacement : MonoBehaviour{
    [SerializeField] private Transform _playerHand, _enemyHand;
    [SerializeField] private Transform _resultCardPosition, _card1InLinePosition, _card2InLinePosition;

    private Vector3 _playerHandStartPosition, _enemyHandStartPosition;

    private void OnEnable() {
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
        // Debug.Log("Fusion_OnFusionStarted Invoked");

        if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
            Vector3 targetPosition = new(0.72f,-1f,-3f);
            _playerHand.GetComponent<Hand>().MoveHand(targetPosition);

        }else{
            Vector3 targetPosition = new(0.72f,-1f,-3f);
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
        var cardIndex = 0;

        foreach(var card in selectedCards){
            card.GetComponent<Collider>().enabled = false;

            if(cardIndex == 0){
                card.transform.SetParent(_card1InLinePosition.transform);
                card.MoveCard(_card1InLinePosition.position, _card1InLinePosition.rotation);

            }else if(cardIndex == 1){
                card.transform.SetParent(_card2InLinePosition.transform);
                card.MoveCard(_card2InLinePosition.position, _card2InLinePosition.rotation);

            }else{
                card.transform.SetParent(_card2InLinePosition.transform);
                
                var offsetPosition = 0.3f * cardIndex;
                Vector3 finalPosition = new(_card2InLinePosition.position.x + offsetPosition, _card2InLinePosition.position.y, _card2InLinePosition.position.z);

                card.MoveCard(finalPosition, _card2InLinePosition.rotation);
            }

            cardIndex++;
        }
    }

    public void MoveResultCardToPosition(Card resultCard){
        resultCard.MoveCard(_resultCardPosition.position, _resultCardPosition.rotation);

        resultCard.transform.SetParent(_resultCardPosition);
    }
}