using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckManager : MonoBehaviour{
    public List<CardSO> Deck => _deck;

    //Prefabs
    [SerializeField] private Card _cardPrefab;

    //Lists
    [SerializeField] public List<CardSO> _deck;

}
