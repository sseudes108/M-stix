using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField] private CardCreator _cardCreator;
    public CardCreator CardCreator => _cardCreator;
}