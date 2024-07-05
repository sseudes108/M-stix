using UnityEngine;

[RequireComponent(typeof(CardCreator), typeof(CardSelector), typeof(CardDatabase))]
[RequireComponent(typeof(StatSelections))]
public class CardManager : MonoBehaviour {
    public CardCreator CardCreator { get; private set; }
    public FusionManager Fusion { get; private set; }

    private void Awake() {
        CardCreator = GetComponent<CardCreator>();
        Fusion = GetComponent<FusionManager>();
    }
}