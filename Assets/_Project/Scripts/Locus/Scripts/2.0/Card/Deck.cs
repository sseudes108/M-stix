// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Deck : MonoBehaviour {
//     [field:SerializeField] public List<CardSO> DeckInUse { get; private set; }
//     [field:SerializeField] private UIEventHandlerSO _UIEventHandler;
//     [SerializeField] private bool _isPlayerDeck;
//     private int _deckCount;

//     private void Start() {
//         _deckCount = 0;
//         _UIEventHandler.UpadateDeckCount(_isPlayerDeck, _deckCount);
//         StartCoroutine(ResetDeckCount());
//     }

//     public void RemoveCardFromDeck(CardSO cardToRemove) { 
//         DeckInUse.Remove(cardToRemove);
//         _UIEventHandler.UpadateDeckCount(_isPlayerDeck, DeckInUse.Count);
//     }

//     private IEnumerator ResetDeckCount(){
//         while(_deckCount < DeckInUse.Count){
//             _deckCount++;
//             _UIEventHandler.UpadateDeckCount(_isPlayerDeck, _deckCount);
//             yield return new WaitForSeconds(0.03f);
//         }
//         yield return null;
//     }
// }