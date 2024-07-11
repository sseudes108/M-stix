using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardEventHandlerSO", menuName = "Mistix/Events/Card", order = 0)]
public class CardEventHandlerSO : ScriptableObject {
    public UnityEvent<Card> OnCardSelected;
    public UnityEvent<Card> OnCardDeselected;

    private void OnEnable() {
        OnCardSelected ??= new UnityEvent<Card>();
        OnCardDeselected ??= new UnityEvent<Card>();
    }

    public void CardSelected(Card card) { OnCardSelected?.Invoke(card); }
    public void CardDeselected(Card card) { OnCardDeselected?.Invoke(card); }
}