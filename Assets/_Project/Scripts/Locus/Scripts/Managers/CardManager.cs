using UnityEngine;

[RequireComponent(typeof(CardCreator), typeof(CardSelector), typeof(CardDatabase))]
[RequireComponent(typeof(CardStatSelections))]
public class CardManager : MonoBehaviour {
    public CardCreator Creator { get; private set; }
    public CardSelector Selector { get; private set; }

    private void Awake() {
        Creator = GetComponent<CardCreator>();
        Selector = GetComponent<CardSelector>();
    }
}