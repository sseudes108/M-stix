using Mistix;
using UnityEngine;

public class CardManager : MonoBehaviour{
    private CardCreator _cardCreator;
    private CardSelector _cardSelector;
    private CardsDatabase _cardsDatabase;
    private CardSpawnLocations _cardSpawnLocation;

    private void Awake() {
        _cardCreator = GetComponent<CardCreator>();
        _cardSelector = GetComponent<CardSelector>();
        _cardsDatabase = GetComponent<CardsDatabase>();
        _cardSpawnLocation = GetComponent<CardSpawnLocations>();
    }

    public CardCreator CardCreator => _cardCreator;
    public CardSelector CardSelector => _cardSelector;
    public CardsDatabase CardsDatabase => _cardsDatabase;
    public CardSpawnLocations CardSpawnLocations => _cardSpawnLocation;
}
