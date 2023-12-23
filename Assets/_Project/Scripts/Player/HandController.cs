using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour{

    [SerializeField] private List<Transform> _handCardPositions;
    [SerializeField] private Card _cardPrefab;

    [SerializeField] private List<CardSO> _pickedCards;
    [SerializeField] private List<CardSO> _cardsInHand;

    [SerializeField] private PlayerDeckManager _deckManager;


    private void Awake() {
        //_deckManager = GetComponent<PlayerDeckManager>();

        StartCoroutine(AddCardToHandRoutine());
    }
    private IEnumerator AddCardToHandRoutine(){

        _pickedCards = PickCardsFromDeck();
        yield return new WaitForSeconds(2);

        _cardsInHand = AddCardsToHand(_pickedCards);
        yield return new WaitForSeconds(2);
        
        MoveCardsToPosition(_cardsInHand);
    }

    private List<CardSO> PickCardsFromDeck(){
        List<CardSO> pickedCards = new();
        pickedCards.Clear();

        for(int i = 0; i < 5; i ++){
            int pickedCard = RandomValue(_deckManager.Deck);

            pickedCards.Add(_deckManager.Deck[pickedCard]);
            _deckManager.Deck.Remove(_deckManager.Deck[pickedCard]);
        }
        Debug.Log("Picked Cards");
        return pickedCards;
    }
    private int RandomValue(List<CardSO> deck){
        return Random.Range(0, deck.Count);
    }

    private List<CardSO> AddCardsToHand(List<CardSO> pickedCards){
        List<CardSO> cardsInHand = new();
        int data = 0;
        foreach (CardSO card in pickedCards){
            cardsInHand.Add(pickedCards[data]);
            data++;
        }
        Debug.Log("Cards in hand");
        return cardsInHand;
    }

    private void MoveCardsToPosition(List<CardSO> cardsInHand){
        foreach (CardSO card in cardsInHand){
            _cardPrefab.SetCardData(card);
            Instantiate(_cardPrefab, _handCardPositions[0].transform.position, _handCardPositions[0].transform.rotation);
            _handCardPositions.Remove(_handCardPositions[0]);
        }
        Debug.Log("Move to position");
    } 
}