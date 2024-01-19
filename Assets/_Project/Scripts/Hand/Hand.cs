using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public static Action<Hand> OnAnyCardDraw;
    [SerializeField] private List<Transform> _handPlaces;
    [SerializeField] private List<Transform> _freePositionsInHand;
    [SerializeField] private Deck _deck;

    private void OnEnable() {
        Card.OnAnyCardSelected += Card_OnAnyCardSelected;
    }

    private void OnDisable() {
        Card.OnAnyCardSelected -= Card_OnAnyCardSelected;
    }

    private void Start() {
        CheckFreePositionsInHand();
        DrawCards();
    }

    public void DrawCards(){
        StartCoroutine(DrawRoutine());
    }

    private IEnumerator DrawRoutine(){
        int numberOfFreePositionsInHand =  _freePositionsInHand.Count;
        
        for(int i = 0; i < numberOfFreePositionsInHand; i++){
            int randomIndexFromDeck = UnityEngine.Random.Range(0,_deck.DeckInUse.Count);
            ScriptableObject cardata = _deck.DeckInUse[randomIndexFromDeck];

            Card cardDrew = Instantiate(CardCreator.Instance.CreateCard(cardata));

            _deck.RemoveCardFromDeck(randomIndexFromDeck);
                        
            cardDrew.transform.SetLocalPositionAndRotation(_freePositionsInHand[0].position, _freePositionsInHand[0].rotation);
            cardDrew.transform.SetParent(_freePositionsInHand[0].transform);

            HandPositions playerHandPositions = _freePositionsInHand[0].GetComponent<HandPositions>();
            playerHandPositions.SetOccupied();    
            
            CheckFreePositionsInHand();
            OnAnyCardDraw?.Invoke(this);

            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("End Draw Routine");        
    }

    private void Card_OnAnyCardSelected(Card card){
        CheckFreePositionsInHand();
    }

    private void CheckFreePositionsInHand(){
        _freePositionsInHand.Clear();
        foreach(Transform handPosition in _handPlaces){
            HandPositions handPositions = handPosition.GetComponent<HandPositions>();
            if(handPositions.Isfree()){
                _freePositionsInHand.Add(handPosition);
            }
        }
    }

    public int GetCountDeckInUse(){
        return _deck.DeckInUse.Count;
    }
}