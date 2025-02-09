using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class Deck : MonoBehaviour {
        [SerializeField] private List<CardSO> _deckInUse;
        [SerializeField] private bool _isPlayerDeck;

        public void RemoveCardFromDeck(CardSO cardToRemove) { 
            _deckInUse.Remove(cardToRemove);
        }
        public int GetDeckCount() { return _deckInUse.Count; }
        public List<CardSO> GetDeck() { return _deckInUse; }
        public bool IsPlayerDeck() { return _isPlayerDeck; }
    }
}