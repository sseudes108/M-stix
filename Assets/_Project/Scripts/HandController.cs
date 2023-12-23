using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HandController : MonoBehaviour{
    public List<Card> CardsInHand => _cardsInHand;
    private DeckManager _deckManager;
    [SerializeField] private Card _cardPrefab;
    //[SerializeField] private List<Transform> _handCardPositions;
    [SerializeField] private List<HandPlacementController> _handCardPositions;
    [SerializeField] private List<Card> _cardsInHand;
    [SerializeField] private List<CardSO> _pickedCards;
    [SerializeField] private List<CardSO> _cardsToAddToHand;
    [SerializeField] private List<HandPlacementController> _freeHandCardPositions;   

    private void Awake() {
        _deckManager = GetComponentInParent<DeckManager>();

        StartCoroutine(DrawCardsRoutine());
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            RemoveCardFromHand(_cardsInHand[2]);
        }

        if(Input.GetKeyDown(KeyCode.K)){
            StartCoroutine(DrawCardsRoutine());
            CheckFreePositionsInHand(_handCardPositions);
        }
    }
    private IEnumerator DrawCardsRoutine(){

        _pickedCards = PickCardsFromDeck();
        yield return new WaitForSeconds(2);

        _cardsToAddToHand = CardsToAddToHand(_pickedCards);
        yield return new WaitForSeconds(2);

        MoveCardsToPosition(_cardsToAddToHand);
    }

    private int CheckFreePositionsInHand(List<HandPlacementController> handCardPositions){
        _freeHandCardPositions.Clear();
        foreach(HandPlacementController cardPlace in handCardPositions){
            if(!cardPlace.ocuppied){
                _freeHandCardPositions.Add(cardPlace);
            }
        }
        return _freeHandCardPositions.Count;
    }

    private List<CardSO> PickCardsFromDeck(){
        List<CardSO> pickedCards = new();
        for(int i = 0; i < CheckFreePositionsInHand(_handCardPositions); i ++){
            int pickedCard = RandomValue(_deckManager.Deck);

            pickedCards.Add(_deckManager.Deck[pickedCard]);
            _deckManager.Deck.Remove(_deckManager.Deck[pickedCard]);
        }
        //Debug.Log("Picked Cards");
        return pickedCards;
    }
    private int RandomValue(List<CardSO> deck){
        return Random.Range(0, deck.Count);
    }

    private List<CardSO> CardsToAddToHand(List<CardSO> pickedCards){
        List<CardSO> cardsToAddToHand = new();
        int data = 0;
        foreach (CardSO card in pickedCards){
            cardsToAddToHand.Add(pickedCards[data]);
            data++;
        }
        //Debug.Log("Cards in hand");
        return cardsToAddToHand;
    }

    private void MoveCardsToPosition(List<CardSO> cardsToAddToHand){
        int pos = 0;
        foreach (CardSO card in cardsToAddToHand){
            _cardPrefab.SetCardData(card);

            if (_handCardPositions[pos].ocuppied){
                pos++;
            }

            Card newCard = Instantiate(_cardPrefab, _handCardPositions[pos].gameObject.transform.position, _handCardPositions[pos].gameObject.transform.rotation);
            newCard.transform.SetParent(_handCardPositions[pos].transform);
            _handCardPositions[pos].ocuppied = true;
            _cardsInHand.Add(newCard);
            pos++;
        }
        cardsToAddToHand.Clear();
        _pickedCards.Clear();
        //Debug.Log("Move to position");
    }

    public void MoveFusionedCardToPositionInHand(Card fusionedCard){
        CheckFreePositionsInHand(_handCardPositions);
        
        fusionedCard.transform.position = _freeHandCardPositions[0].gameObject.transform.position;
        fusionedCard.transform.SetParent(_freeHandCardPositions[0].gameObject.transform);

        _freeHandCardPositions[0].ocuppied = true;
        _cardsInHand.Add(fusionedCard);
    }

    public void RemoveCardFromHand(Card cardToRemove){
        HandPlacementController handPlacementController = cardToRemove.GetComponentInParent<HandPlacementController>();
        handPlacementController.ocuppied = false;
        _cardsInHand.Remove(cardToRemove);
        Destroy(cardToRemove.gameObject);
        Debug.Log("RemoveCardFromHand");
    }
}