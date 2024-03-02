using UnityEngine;

public class HandPlayer : Hand {

    protected override void GetHand(){
        _hand = GetComponent<HandPlayer>();
    }
    protected override void GetDeck(){
        deck = GetComponentInChildren<Deck>();
    }
}