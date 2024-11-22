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

    [HideInInspector] public UnityEvent<bool, bool> OnAttackSelectionStart;

    [HideInInspector] public UnityEvent<MonsterCard, MonsterCard> OnDamageStart;
    [HideInInspector] public UnityEvent OnDamageStartUI;
    [HideInInspector] public UnityEvent OnAttackEnd;

    [HideInInspector] public UnityEvent OnEndPhaseStart;

    #endregion

    private AbstractState _currentPhase;
    public AbstractState CurrentPhase => _currentPhase;
    public Battle Battle { get; private set; }

    public MonsterCard AttackerMonster { get; private set; }
    public MonsterCard TargetMonster { get; private set; }

    private void OnEnable() {
        CreateEvents();
    }

    public void EndActionPhase() { Battle.ChangeState(Battle.EndPhase); }

    public void SetBattleController(StateMachine battle) { Battle = battle as Battle; }

    private void CreateEvents(){
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

        OnAttackSelectionStart ??=new UnityEvent<bool, bool>();

        OnDamageStart ??=new UnityEvent<MonsterCard, MonsterCard>();
        OnDamageStartUI ??=new UnityEvent();
        OnAttackEnd ??=new UnityEvent();
        
        OnEndPhaseStart ??= new UnityEvent();
    }

#region Events
    public void ChangeState(AbstractState newState) { // Used for hold the current phase ref and notify the UI
        _currentPhase = newState;
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

    public void AttackSelectionStart(bool isPlayerTurn, bool isDirectAttack, MonsterCard attacker) {
        AttackerMonster = attacker;

        OnAttackSelectionStart?.Invoke(isPlayerTurn, isDirectAttack);
        
        Battle.ChangeState(Battle.AttackSelectionPhase);
    }

    public void StartDamagePhase(MonsterCard target){
        TargetMonster = target;

        OnDamageStart?.Invoke(AttackerMonster, TargetMonster);
        OnDamageStartUI?.Invoke();

        Battle.ChangeState(Battle.DamagePhase);
    }

    public void AttackEnded(){
        OnAttackEnd.Invoke();
                
        Battle.ChangeState(Battle.Action);
    }

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