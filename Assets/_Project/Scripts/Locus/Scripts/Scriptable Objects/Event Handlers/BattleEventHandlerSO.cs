using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BattleEventHandler", menuName = "Mistix/Managers/Battle")]
public class BattleEventHandlerSO : ScriptableObject {
    [HideInInspector] public UnityEvent <AbstractState> OnStateChange;

    [HideInInspector] public UnityEvent OnStartPhase;

    [HideInInspector] public UnityEvent OnPlayerDraw;
    [HideInInspector] public UnityEvent OnEnemyDraw;

    [HideInInspector] public UnityEvent OnCardSelectionStart;
    [HideInInspector] public UnityEvent OnCardSelectionEnd;

    [HideInInspector] public UnityEvent<Card> OnStatSelectStart;
    [HideInInspector] public UnityEvent<Card, bool>  OnStatSelectEnd;

    [HideInInspector] public UnityEvent<Card, bool> OnBoardPlaceSelectionStart;
    [HideInInspector] public UnityEvent<Card, bool> OnBoardPlaceSelectionEnd;

    [HideInInspector] public UnityEvent OnActionPhaseStart;
    [HideInInspector] public UnityEvent OnActionPhaseTwoStart;

    public AbstractState CurrentPhase;

    public Battle _battle;

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

        OnActionPhaseStart ??=new UnityEvent();
        OnActionPhaseTwoStart ??=new UnityEvent();
    }

    public void ChangeState(AbstractState newState) { // Used for hold the current phase ref and notify the UI
        CurrentPhase = newState;
        OnStateChange?.Invoke(newState); //UI notification
    }

    public void EndActionPhase(){
        _battle.ChangeState(_battle.ActionTwo);
    }
    
    public void StartPhase() { OnStartPhase?.Invoke(); }
    public void PlayerDraw() { OnPlayerDraw?.Invoke(); }
    public void EnemyDraw() { OnEnemyDraw?.Invoke(); }
    public void CardSelectionStart() { OnCardSelectionStart?.Invoke(); }
    public void CardSelectionEnd() { OnCardSelectionEnd?.Invoke(); }
    public void StatSelectStart(Card card) { OnStatSelectStart?.Invoke(card); }
    public void StatSelectEnd(Card card, bool isPlayerTurn) { OnStatSelectEnd?.Invoke(card, isPlayerTurn); }
    public void BoardPlaceSelectionStart(Card card, bool isPlayerTurn) { OnBoardPlaceSelectionStart?.Invoke(card, isPlayerTurn); }
    public void BoardPlaceSelectionEnd(Card card, bool isPlayerTurn) { OnBoardPlaceSelectionEnd?.Invoke(card, isPlayerTurn); }
    public void ActionPhaseStart() { OnActionPhaseStart?.Invoke(); }
    public void ActionPhaseTwoStart() { OnActionPhaseTwoStart?.Invoke(); }

    public void SetBattle(Battle battle) { 
        if(_battle == null){
            _battle = battle;
        }
    }
}