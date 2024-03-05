using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour{
    [SerializeField] protected List<Transform> _handPositions;
    protected List<Transform> _freeHandPositions = new();
    protected Hand _hand;
    public Deck deck;

    //Move
    private Movement _movement;

    //
    private void OnEnable() {
        BattleManager.Instance.Fusion.OnFusionStart += Fusion_OnFusionStart;
    }

    private void OnDisable() {
        BattleManager.Instance.Fusion.OnFusionStart -= Fusion_OnFusionStart;
    }

    private void Awake() {
        GetHand();
        GetDeck();
        _movement = GetComponentInChildren<Movement>();
    }

    private void Start() {
        DrawCard();
    }
    protected virtual void GetHand(){}
    protected virtual void GetDeck(){}

    private void CheckFreePositionsInHand(){
        _freeHandPositions.Clear();
        foreach(var position in _handPositions){
            var handPosition = position.GetComponent<HandPosition>();
            if(handPosition.IsFree){
                _freeHandPositions.Add(position);
            }
        }
    }
    
    public virtual void DrawCard(){
        StartCoroutine(DrawCardRoutine());
    }

    private IEnumerator DrawCardRoutine(){
        CheckFreePositionsInHand();
        int cardsToDraw;

        do{
            cardsToDraw = _freeHandPositions.Count;

            //Card data
            var randomIndex = Random.Range(0, deck.DeckInUse.Count);
            var cardData = deck.DeckInUse[randomIndex];

            //Spawn Position
            deck.transform.GetPositionAndRotation(out Vector3 spawnPosition, out Quaternion spawnRotation);

            //Instance
            var drewCard = Instantiate(BattleManager.Instance.CardCreator.CreateCard(cardData), spawnPosition, spawnRotation);
            drewCard.name = drewCard.GetCardName();

            //Remove card from deck
            deck.RemoveCardFromDeck(cardData);

            //Check card Owner
            if(_hand is HandPlayer){
                drewCard.SetPlayerCard();
            }

            //Move to hand position
            drewCard.MoveCard(_freeHandPositions[0].transform);

            //Ocupy place in hand
            _freeHandPositions[0].GetComponent<HandPosition>().SetHandPlaceOccupied();
            drewCard.SetCardOnHand();

            //Refresh positions
            CheckFreePositionsInHand();

            //Wait
            yield return new WaitForSeconds(0.5f);
            
        }while(cardsToDraw > 1);
    }

    protected virtual void MoveHand(Vector3 targetPosition){
        if(_hand is HandPlayer){
            _movement.SetTargetPosition(targetPosition, 5); 
        }
    }
    private void Fusion_OnFusionStart(){
        Debug.Log("Fusion Start Signal");
        MoveHand(BattleManager.Instance.FusionPositions.HandOffCameraPosition.position);
    }
}