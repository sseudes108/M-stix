using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BattleEventHandlerSO", menuName = "Mistix/Events/Battle", order = 0)]
public class BattleEventHandlerSO : ScriptableObject {
    public UnityEvent <AbstractState> OnStateChange;

    public UnityEvent OnStartPhase;

    public UnityEvent OnPlayerDraw;
    public UnityEvent OnEnemyDraw;

    public UnityEvent OnCardSelectionStart;
    public UnityEvent OnCardSelectionEnd;

    public UnityEvent<Card> OnStatSelectStart;
    public UnityEvent<Card, bool>  OnStatSelectEnd;

    public UnityEvent<Card, bool> OnBoardPlaceSelectionStart;
    public UnityEvent<Card, bool> OnBoardPlaceSelectionEnd;

    private void OnEnable() {
        OnStateChange ??= new UnityEvent<AbstractState>();

        OnStartPhase ??= new UnityEvent();

        OnPlayerDraw ??= new UnityEvent();
        OnEnemyDraw ??= new UnityEvent();

        OnCardSelectionStart ??= new UnityEvent();
        OnCardSelectionEnd ??= new UnityEvent();

        OnStatSelectStart ??= new UnityEvent<Card>();
        OnStatSelectEnd ??= new UnityEvent<Card, bool>();

        OnBoardPlaceSelectionStart ??= new UnityEvent<Card, bool>();
        OnBoardPlaceSelectionEnd ??= new UnityEvent<Card, bool>();
    }

    public void ChangeState(AbstractState newState) { OnStateChange?.Invoke(newState); }
    public void StartPhase() { OnStartPhase?.Invoke(); }
    public void PlayerDraw() { OnPlayerDraw?.Invoke(); }
    public void EnemyDraw() { OnEnemyDraw?.Invoke(); }
    public void CardSelectionStart() { OnCardSelectionStart?.Invoke(); }
    public void CardSelectionEnd() { OnCardSelectionEnd?.Invoke(); }
    public void StatSelectStart(Card card) { OnStatSelectStart?.Invoke(card); }
    public void StatSelectEnd(Card card, bool isPlayerTurn) { OnStatSelectEnd?.Invoke(card, isPlayerTurn); }
    public void BoardPlaceSelectionStart(Card card, bool isPlayerTurn) { OnStatSelectEnd?.Invoke(card, isPlayerTurn); }
    public void BoardPlaceSelectionEnd(Card card, bool isPlayerTurn) { OnStatSelectEnd?.Invoke(card, isPlayerTurn); }
}