using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour{
    [SerializeField] List<ScriptableObject> _deckInUse;

    public List<ScriptableObject> GetDeckInUse() => _deckInUse;
}
