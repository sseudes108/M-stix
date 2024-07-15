using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField] protected BattleManagerSO BattleManager;
    [SerializeField] protected HandManagerSO HandManager;
    [SerializeField] protected CardManagerSO CardManager;

    [SerializeField] private Transform[] _handPositions;
    [SerializeField] List<Transform> _freePositionsInHand;
    [SerializeField] protected List<Card> _cardsInHand;

    [SerializeField] private Deck _deck;

    public virtual void OnEnable() {
        BattleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
    }

    public virtual void OnDisable() {
        BattleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
    }

    private void BattleManager_OnStartPhase() {
        CheckPositionsInHand(); 
    }

    private void CheckPositionsInHand(){
        _cardsInHand.Clear();
        _freePositionsInHand.Clear();

        foreach(var position in _handPositions){
            var handPosition = position.GetComponent<HandPosition>();
            if(handPosition.IsFree){
                _freePositionsInHand.Add(position);
            }else{
                _cardsInHand.Add(handPosition.CardInPosition);
            }
        }
    }

    public void Draw() { StartCoroutine(DrawCardsRoutine()); }

    private IEnumerator DrawCardsRoutine(){
        foreach(var position in _freePositionsInHand){
            //Random card data
            var randomIndex = Random.Range(0, _deck.DeckInUse.Count);
            var cardData = _deck.DeckInUse[randomIndex];

            //Instantiate
            var drewCard = Instantiate(CardManager.Creator.CreateCard(cardData), _deck.transform.position, _deck.transform.rotation);

            //Remove from Deck
            _deck.RemoveCardFromDeck(cardData);

            //Card Owner
            if(this is PlayerHand){
                drewCard.SetPlayerCard();
            }

            //Move to position
            drewCard.MoveCard(position);

            //Occupy place position
            _cardsInHand.Add(drewCard);
            drewCard.SetCardOnHand(true);
            position.GetComponent<HandPosition>().OccupyPlace(drewCard);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
        CardsDrew();
    }

    public virtual void CardsDrew(){ //The Enemy has some aditional logic before call the event
        HandManager.CardsDrew();
    }
}