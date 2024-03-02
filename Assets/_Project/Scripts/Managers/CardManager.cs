using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField] private CardCreator _cardCreator;
    [SerializeField] private CardSelector _cardSelector;
    [SerializeField] private CardsDatabase _cardsDatabase;

    public CardCreator CardCreator => _cardCreator;
    public CardSelector CardSelector => _cardSelector;
    public CardsDatabase CardsDatabase => _cardsDatabase;

    private void Awake() {
        _cardCreator = GetComponent<CardCreator>();
        _cardSelector = GetComponent<CardSelector>();
        _cardsDatabase = GetComponent<CardsDatabase>();
    }

}