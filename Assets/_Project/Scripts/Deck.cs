using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour{
    public List<CardSO> DeckBase => _deck;
    [SerializeField] private List<CardSO> _deck;
}
