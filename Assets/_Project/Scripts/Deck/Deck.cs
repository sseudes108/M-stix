using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    [SerializeField] private List<ScriptableObject> _cardsInDeck;

    public List<ScriptableObject> DeckInUse => _cardsInDeck;

    public void RemoveCardFromDeck(ScriptableObject cardToRemove){
        _cardsInDeck.Remove(cardToRemove);
        BattleManager.Instance.UIBattleManager.UpdateDeckCount();
    }
}