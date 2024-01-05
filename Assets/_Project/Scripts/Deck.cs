using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour{
    public List<ScriptableObject> DeckInUse => _deck;
    [SerializeField] private List<ScriptableObject> _deck;

    public void RemoveCardFromDeck(int cardToRemove){
        _deck.Remove(_deck[cardToRemove]);
    }
}