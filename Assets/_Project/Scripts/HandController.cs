using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandController : MonoBehaviour{
    public static HandController Instance;

    public List<Card> CardsInHand => _cardsInHand;
    private DeckManager _deckManager;
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private List<HandPlacementController> _handCardPositions;
    [SerializeField] private List<Card> _cardsInHand;
    [SerializeField] private List<CardSO> _pickedCards;
    [SerializeField] private List<CardSO> _cardsToAddToHand;
    [SerializeField] private List<HandPlacementController> _freeHandCardPositions;
    private int _freePositionsInHand;
    private Coroutine _drawRoutine;

    private void Awake() {
        if(Instance == null){Instance = this;}

        _deckManager = GetComponentInParent<DeckManager>();

        CheckFreePositionsInHand(_handCardPositions);
        _drawRoutine = StartCoroutine(DrawCardsRoutine());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)){
            DrawCards();
        }
    }

    private void DrawCards(){
        CheckFreePositionsInHand(_handCardPositions);
        StartCoroutine(DrawCardsRoutine());
    }

    private IEnumerator DrawCardsRoutine(){

        _pickedCards = PickCardsFromDeck();
        yield return new WaitForSeconds(2);

        _cardsToAddToHand = CardsToAddToHand(_pickedCards);
        yield return new WaitForSeconds(2);

        MoveCardsToPositionInHand(_cardsToAddToHand);
    }

    private void CheckFreePositionsInHand(List<HandPlacementController> handCardPositions){
        _freeHandCardPositions.Clear();

        foreach(HandPlacementController cardPlace in handCardPositions){
            if(!cardPlace.HandPlaceOcuppied){
                _freeHandCardPositions.Add(cardPlace);
            }
        }
        _freePositionsInHand = _freeHandCardPositions.Count;
    }

    private List<CardSO> PickCardsFromDeck(){
        List<CardSO> pickedCards = new();

        for(int i = 0; i < _freePositionsInHand ; i ++){
            int pickedCard = Random.Range(0, _deckManager.Deck.Count);

            if(_deckManager.Deck.Count == 0){
                Debug.Log("You Lose! No Remaining Cards on Deck");
                StopCoroutine(_drawRoutine);
            }
            pickedCards.Add(_deckManager.Deck[pickedCard]);
            _deckManager.RemovePickedCard(_deckManager.Deck[pickedCard]);
        }
        Debug.Log(string.Format("Cards remaining in deck:{0}", _deckManager.CardsRemaining()));
        return pickedCards;
    }

    private List<CardSO> CardsToAddToHand(List<CardSO> pickedCards){
        List<CardSO> cardsToAddToHand = new();
        int data = 0;
        foreach (CardSO card in pickedCards){
            cardsToAddToHand.Add(pickedCards[data]);
            data++;
        }
        return cardsToAddToHand;
    }

    private void MoveCardsToPositionInHand(List<CardSO> cardsToAddToHand){
        int pos = 0;

        foreach (CardSO card in cardsToAddToHand){
            _cardPrefab.SetCardData(card);

            while (_handCardPositions[pos].HandPlaceOcuppied){
                Debug.Log("HandPlaceOcuppied");
                pos++;
            }

            Card newCard = Instantiate(_cardPrefab, _handCardPositions[pos].gameObject.transform.position, _handCardPositions[pos].gameObject.transform.rotation);
            newCard.transform.SetParent(_handCardPositions[pos].transform);
            _handCardPositions[pos].HandPlaceOcuppied = true;
            _cardsInHand.Add(newCard);
            pos++;
        }
        cardsToAddToHand.Clear();
        _pickedCards.Clear();
        CheckFreePositionsInHand(_handCardPositions);
    }
    
    //Only for test
    public void MoveFusionedCardToPositionInHand(Card fusionedCard){
        //int pos = 0;
        CheckFreePositionsInHand(_handCardPositions);

        Debug.Log(string.Format("_freeHandCardPositions.Count:{0}",_freeHandCardPositions.Count));
        fusionedCard.transform.position = _freeHandCardPositions[0].gameObject.transform.position;
        fusionedCard.transform.SetParent(_freeHandCardPositions[0].gameObject.transform);
        
        _cardsInHand.Add(fusionedCard);
        _freeHandCardPositions[0].HandPlaceOcuppied = true;

        CheckFreePositionsInHand(_handCardPositions);        
    }//Only for test

    public void MoveCardToPlaceInBoard(Card fusionedCard){
        Debug.Log("MoveCardToPlaceInBoard");

        int pos = 0;

        while(BoardManager.Instance.PlayerMonsterPlaces[pos].BoardPlaceOccupied == true){
            pos++;
            if(pos == 5){
                break;
            }
        }

        fusionedCard.gameObject.transform.SetParent(BoardManager.Instance.PlayerMonsterPlaces[pos].gameObject.transform);

        fusionedCard.gameObject.transform.SetPositionAndRotation(BoardManager.Instance.PlayerMonsterPlaces[pos].transform.position, 
        BoardManager.Instance.PlayerMonsterPlaces[pos].transform.rotation);

        BoardManager.Instance.OcuppyPlace(BoardManager.Instance.PlayerMonsterPlaces, BoardManager.Instance.PlayerMonsterPlaces[pos]);
    }

    public void RemoveCardFromHand(Card cardToRemove){
        Debug.Log("RemoveCardFromHand");
        HandPlacementController handPlacementController = cardToRemove.GetComponentInParent<HandPlacementController>();

        if(handPlacementController != null){ handPlacementController.HandPlaceOcuppied = false;}

        _cardsInHand.Remove(cardToRemove);
        Destroy(cardToRemove.gameObject);
    }
}