using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    [field:SerializeField] public List<CardSO> DeckInUse { get; private set; }

    public void RemoveCardFromDeck(CardSO cardToRemove) { DeckInUse.Remove(cardToRemove); }
}