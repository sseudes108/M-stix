using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UIEventHandlerSO", menuName = "Mistix/Events/UI", order = 0)]
public class UIEventHandlerSO : ScriptableObject {
    public UnityEvent  OnCardSelectionFinished;
    public UnityEvent<Card, bool>  OnMonsterAttack;
    public UnityEvent<Texture2D> OnUpdateIllustration;

    private void OnEnable() {
        OnCardSelectionFinished ??= new UnityEvent();
        OnMonsterAttack ??= new UnityEvent<Card, bool> ();
    }

    public void CardSelectionFinished() { OnCardSelectionFinished?.Invoke(); }
    public void MonsterAttack(Card card, bool isPlayerTurn) { OnMonsterAttack?.Invoke(card, isPlayerTurn); }
    public void UpdateIllustration(Texture2D newIllustration) { OnUpdateIllustration?.Invoke(newIllustration); }
}