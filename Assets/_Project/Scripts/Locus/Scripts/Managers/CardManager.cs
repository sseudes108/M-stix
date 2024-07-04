using UnityEngine;

[RequireComponent(typeof(CardCreator), typeof(CardSelector), typeof(CardDatabase))]
public class CardManager : MonoBehaviour {
    public CardCreator CardCreator { get; private set; }

    private void Awake() {
        CardCreator = GetComponent<CardCreator>();
    }
}