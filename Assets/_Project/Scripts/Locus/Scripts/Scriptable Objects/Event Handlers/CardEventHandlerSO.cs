using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardEventHandlerSO", menuName = "Mistix/Events/Card", order = 0)]
public class CardEventHandlerSO : ScriptableObject {
    public UnityEvent<Card> OnCardSelected;
    public UnityEvent<Card> OnCardDeselected;

    public UnityEvent OnSomeCardSelected;
    public UnityEvent OnNoneCardSelected;


    private void OnEnable() {
        OnCardSelected ??= new UnityEvent<Card>();
        OnCardDeselected ??= new UnityEvent<Card>();

        OnSomeCardSelected ??= new UnityEvent();
        OnNoneCardSelected ??= new UnityEvent();
    }

    public void CardSelected(Card card) { OnCardSelected?.Invoke(card); }
    public void CardDeselected(Card card) { OnCardDeselected?.Invoke(card); }
    public void SomeCardSelected() { OnSomeCardSelected?.Invoke(); }
    public void NoneCardSelected() { OnNoneCardSelected?.Invoke(); }

}