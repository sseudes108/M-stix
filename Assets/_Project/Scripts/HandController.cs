using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour{
    public static HandController Instance;


    public List<Card> CardsInHand => _cardsInHand;
    private DeckManager _deckManager;
    [SerializeField] private Card _cardPrefab;
    //[SerializeField] private List<Transform> _handCardPositions;
    [SerializeField] private List<HandPlacementController> _handCardPositions;
    [SerializeField] private List<Card> _cardsInHand;
    [SerializeField] private List<CardSO> _pickedCards;
    [SerializeField] private List<CardSO> _cardsToAddToHand;
    [SerializeField] private List<HandPlacementController> _freeHandCardPositions;
    private int _freePositionsInHand;

    private void Awake() {
        if(Instance == null){Instance = this;}

        _deckManager = GetComponentInParent<DeckManager>();

        CheckFreePositionsInHand(_handCardPositions);
        StartCoroutine(DrawCardsRoutine());        
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            RemoveCardFromHand(_cardsInHand[2]);
        }

        if(Input.GetKeyDown(KeyCode.K)){
            CheckFreePositionsInHand(_handCardPositions);
            StartCoroutine(DrawCardsRoutine());
        }
    }
    private IEnumerator DrawCardsRoutine(){

        _pickedCards = PickCardsFromDeck();
        yield return new WaitForSeconds(2);

        _cardsToAddToHand = CardsToAddToHand(_pickedCards);
        yield return new WaitForSeconds(2);

        MoveCardsToPosition(_cardsToAddToHand);
    }

    private void CheckFreePositionsInHand(List<HandPlacementController> handCardPositions){
        Debug.Log("CheckFreePositionsInHand");

        _freeHandCardPositions.Clear();

        foreach(HandPlacementController cardPlace in handCardPositions){
            if(!cardPlace.ocuppied){
                _freeHandCardPositions.Add(cardPlace);
            }
        }
        _freePositionsInHand = _freeHandCardPositions.Count;
        Debug.Log(string.Format("_freePositionsInHand{0}", _freePositionsInHand));
    }

    private List<CardSO> PickCardsFromDeck(){
        List<CardSO> pickedCards = new();
        for(int i = 0; i < _freePositionsInHand ; i ++){
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

    private List<CardSO> CardsToAddToHand(List<CardSO> pickedCards){
        List<CardSO> cardsToAddToHand = new();
        int data = 0;
        foreach (CardSO card in pickedCards){
            cardsToAddToHand.Add(pickedCards[data]);
            data++;
        }
        Debug.Log("Cards in hand");
        return cardsToAddToHand;
    }

    private void MoveCardsToPosition(List<CardSO> cardsToAddToHand){
        Debug.Log("MoveCardsToPosition");
        int pos = 0;
        //CheckFreePositionsInHand(_handCardPositions);

        foreach (CardSO card in cardsToAddToHand){
            _cardPrefab.SetCardData(card);

            while (_handCardPositions[pos].ocuppied){
                Debug.Log("Ocuppied");
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
        CheckFreePositionsInHand(_handCardPositions);
        //Debug.Log("Move to position");
    }

    public void MoveFusionedCardToPositionInHand(Card fusionedCard){
        int pos = 0;

        CheckFreePositionsInHand(_handCardPositions);
        
        // while(_handCardPositions[pos].ocuppied){
        //         Debug.Log("Ocuppied");
        //         pos++;

        //         if(pos > _handCardPositions.Count){
        //             Debug.Log("_handCardPositions Greater than 5");
        //             pos = 0;
        //         }
        // }

        Debug.Log(string.Format("_freeHandCardPositions.Count:{0}, Pos{1}",_freeHandCardPositions.Count, pos));
        fusionedCard.transform.position = _freeHandCardPositions[pos].gameObject.transform.position;
        fusionedCard.transform.SetParent(_freeHandCardPositions[pos].gameObject.transform);
        
        _cardsInHand.Add(fusionedCard);
        _freeHandCardPositions[pos].ocuppied = true;

        CheckFreePositionsInHand(_handCardPositions);        
    }

    public void RemoveCardFromHand(Card cardToRemove){
        HandPlacementController handPlacementController = cardToRemove.GetComponentInParent<HandPlacementController>();
        handPlacementController.ocuppied = false;
        _cardsInHand.Remove(cardToRemove);
        Destroy(cardToRemove.gameObject);
        //Debug.Log("RemoveCardFromHand");
    }
}