using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BattleManagerSO", menuName = "Mistix/Manager/Battle")]
public class BattleManagerSO : ScriptableObject {

#region Events
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
    // [HideInInspector] public UnityEvent OnActionPhaseEnd;

    // [HideInInspector] public UnityEvent OnActionPhaseTwoStart;

    [HideInInspector] public UnityEvent OnEndPhaseStart;

    public TurnManagerSO TurnManager;
    public bool IsPlayerTurn => TurnManager.IsPlayerTurn();

    private AbstractState _currentPhase;
    public AbstractState CurrentPhase => _currentPhase;
    private Battle _battle;
    public Battle Battle => _battle;
    
#endregion

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
        // OnActionPhaseEnd ??=new UnityEvent();

        // OnActionPhaseTwoStart ??=new UnityEvent();
        
        OnEndPhaseStart ??= new UnityEvent();
    }

    public void EndActionPhase(){
        _battle.ChangeState(_battle.EndPhase);
    }

#region Events
    public void ChangeState(AbstractState newState) { // Used for hold the current phase ref and notify the UI
        _currentPhase = newState;
        _battle = CurrentPhase.GetBattleController();
        OnStateChange?.Invoke(newState); //UI notification
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
    // public void ActionPhaseEnd() { OnActionPhaseEnd?.Invoke(); }

    // public void ActionPhaseTwoStart() { OnActionPhaseTwoStart?.Invoke(); }

    public void EndPhaseStart() { OnEndPhaseStart?.Invoke(); }

#endregion

#region Helper

    public IEnumerator ChangeStateRoutine(float wait, Battle battle, AbstractState newState){
        yield return new WaitForSeconds(wait);
        battle.ChangeState(newState);
        yield return null;
    }
    
#endregion
}