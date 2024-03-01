using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour{
    [SerializeField] protected List<Transform> _handPositions;
    [SerializeField] protected List<Transform> _freeHandPositions;
    [SerializeField] protected Hand _hand;
    [SerializeField] protected Deck _deck;

    private void Awake() {
        GetHand();
        GetDeck();
    }

    private void Start() {
        DrawCard();
    }

    private void CheckFreePositionsInHand(){
        _freeHandPositions.Clear();
        foreach(var position in _handPositions){
            var handPosition = position.GetComponent<HandPosition>();
            if(handPosition.IsFree){
                _freeHandPositions.Add(position);
            }
        }
    }

    protected virtual void GetHand(){}
    protected virtual void GetDeck(){}

    public virtual void DrawCard(){
        StartCoroutine(DrawCardRoutine());
    }

    private IEnumerator DrawCardRoutine(){
        CheckFreePositionsInHand();
        int cardsToDraw;

        do{
            cardsToDraw = _freeHandPositions.Count;

            //Card data
            var randomIndex = Random.Range(0, _deck.DeckInUse.Count);
            var randomCardData = _deck.DeckInUse[randomIndex];

            //Spawn Position
            _deck.transform.GetPositionAndRotation(out Vector3 spawnPosition, out Quaternion spawnRotation);

            //Instance
            var drewCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(randomCardData), spawnPosition, spawnRotation);

            //Set Parent
            drewCard.transform.SetParent(_freeHandPositions[0].transform);
            //Move to hand position
            drewCard.MoveCard(_freeHandPositions[0].transform.position, _freeHandPositions[0].transform.rotation);

            //Ocupy place
            _freeHandPositions[0].GetComponent<HandPosition>().SetHandPlaceOccupied();

            //Refresh positions
            CheckFreePositionsInHand();

            //Wait
            yield return new WaitForSeconds(0.5f);
            
        }while(cardsToDraw > 1);
    }
}