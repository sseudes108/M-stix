using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardStatEventHandlerSO", menuName = "Mistix/Events/CardStat", order = 0)]
public class CardStatEventHandlerSO : ScriptableObject {
    [HideInInspector] public UnityEvent OnOption1Clicked;
    [HideInInspector] public UnityEvent OnOption2Clicked;
    // [HideInInspector] public UnityEvent<Card> OnOption1Clicked;
    // [HideInInspector] public UnityEvent<Card> OnOption2Clicked;

    [HideInInspector] public UnityEvent<Card> OnSelectAnother;
    [HideInInspector] public UnityEvent OnSelectionsEnd;

    private void OnEnable() {
        OnOption1Clicked ??= new UnityEvent();
        OnOption2Clicked ??= new UnityEvent();
        // OnOption1Clicked ??= new UnityEvent<Card>();
        // OnOption2Clicked ??= new UnityEvent<Card>();

        OnSelectAnother ??= new UnityEvent<Card>();
        OnSelectionsEnd ??= new UnityEvent();
    }
    public void Option1Clicked() { OnOption1Clicked?.Invoke(); }
    public void Option2Clicked() { OnOption2Clicked?.Invoke(); }
    // public void Option1Clicked(Card card) { OnOption1Clicked?.Invoke(card); }
    // public void Option2Clicked(Card card) { OnOption2Clicked?.Invoke(card); }
    public void SelectAnother(Card card) { OnSelectAnother?.Invoke(card); }
    public void SelectionsEnd() { OnSelectionsEnd?.Invoke(); }
}