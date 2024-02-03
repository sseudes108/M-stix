using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mistix{
    public class Deck : MonoBehaviour{
        [SerializeField] List<ScriptableObject> _deckInUse;

        public List<ScriptableObject> GetDeckInUse() => _deckInUse;

        public void RemoveCardFromDeck(ScriptableObject cardDataToRemove){
            _deckInUse.Remove(cardDataToRemove);
        }
    }
}
