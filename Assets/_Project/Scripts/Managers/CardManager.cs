using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField] private CardCreator _cardCreator;
    [SerializeField] private CardSelector _cardSelector;
    [SerializeField] private CardDatabase _cardsDatabase;
    [SerializeField] private CardVisuals _cardsVisual;

    public CardCreator CardCreator => _cardCreator;
    public CardSelector CardSelector => _cardSelector;
    public CardDatabase CardsDatabase => _cardsDatabase;
    public CardVisuals CardVisuals => _cardsVisual;

    private void Awake() {
        _cardCreator = GetComponent<CardCreator>();
        _cardSelector = GetComponent<CardSelector>();
        _cardsDatabase = GetComponent<CardDatabase>();
        _cardsVisual = GetComponent<CardVisuals>();
    }

}