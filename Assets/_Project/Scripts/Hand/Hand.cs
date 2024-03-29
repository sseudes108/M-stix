using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour{
    [SerializeField] protected List<Transform> _handPositions;
    [SerializeField] protected List<Transform> _freeHandPositions;
    protected Hand _hand;
    protected Deck _deck;

    //Move
    private Movement _movement;
    //

    private void Awake() {
        SetHand();
        SetDeck();
        _movement = GetComponentInChildren<Movement>();
    }

    protected virtual void SetHand(){}
    protected virtual void SetDeck(){}
    public Deck GetDeck(){return _deck;}

    private void CheckFreePositionsInHand(){
        _freeHandPositions.Clear();
        foreach(var position in _handPositions){
            var handPosition = position.GetComponent<HandPosition>();
            if(handPosition.IsFree){
                _freeHandPositions.Add(position);
            }
        }
    }

    public virtual void DrawCards(){
        StartCoroutine(DrawCardsRoutine());
    }

    private IEnumerator DrawCardsRoutine(){
        CheckFreePositionsInHand();
        int cardsToDraw;

        if(_freeHandPositions.Count > 0){
            do{
                cardsToDraw = _freeHandPositions.Count;

                //Card data
                var randomIndex = Random.Range(0, _deck.DeckInUse.Count);
                var cardData = _deck.DeckInUse[randomIndex];

                //Spawn Position
                _deck.transform.GetPositionAndRotation(out Vector3 spawnPosition, out Quaternion spawnRotation);

                //Instance
                var drewCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(cardData), spawnPosition, spawnRotation);
                drewCard.name = drewCard.GetCardName();

                //Remove card from deck
                _deck.RemoveCardFromDeck(cardData, this);

                //Check card Owner
                if(_hand is HandPlayer){
                    drewCard.SetPlayerCard();
                }

                //Move to hand position
                drewCard.MoveCard(_freeHandPositions[0].transform);

                //Ocupy place in hand
                _freeHandPositions[0].GetComponent<HandPosition>().SetHandPlaceOccupied();
                drewCard.SetCardOnHand(true);

                //Refresh positions
                CheckFreePositionsInHand();

                //Wait
                yield return new WaitForSeconds(0.5f);
                
            }while(cardsToDraw > 1);
        }
        
        yield return new WaitForSeconds(0.5f);
        EndDrawPhase();
    }

    public virtual void MoveHand(Vector3 targetPosition){
        if(_hand is HandPlayer){
            _movement.SetTargetPosition(targetPosition, 5); 
        }
    }
    protected virtual void EndDrawPhase(){}
}