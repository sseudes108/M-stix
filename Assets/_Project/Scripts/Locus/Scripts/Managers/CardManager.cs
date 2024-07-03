using UnityEngine;

[RequireComponent(typeof(CardCreator))]
public class CardManager : MonoBehaviour {
    public CardCreator CardCreator { get; private set; }

    private void Awake() {
        CardCreator = GetComponent<CardCreator>();
    }
}