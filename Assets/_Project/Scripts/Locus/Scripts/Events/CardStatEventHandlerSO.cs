using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardStatEventHandlerSO", menuName = "Mistix/Events/CardStat", order = 0)]
public class CardStatEventHandlerSO : ScriptableObject {
    public UnityEvent OnOption1Clicked;
    public UnityEvent OnOption2Clicked;

    public UnityEvent<Card> OnSelectAnother;
    public UnityEvent OnSelectionsEnd;

    private void OnEnable() {
        OnOption1Clicked ??= new UnityEvent();
        OnOption2Clicked ??= new UnityEvent();

        OnSelectAnother ??= new UnityEvent<Card>();
        OnSelectionsEnd ??= new UnityEvent();
    }
    public void Option1Clicked() { OnOption1Clicked?.Invoke(); }
    public void Option2Clicked() { OnOption2Clicked?.Invoke(); }
    public void SelectAnother(Card card) { OnSelectAnother?.Invoke(card); }
    public void SelectionsEnd() { OnSelectionsEnd?.Invoke(); }
}