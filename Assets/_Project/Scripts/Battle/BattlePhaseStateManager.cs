using UnityEngine;

public class BattlePhaseStateManager : MonoBehaviour {

    public BattleAbstract _currentState;

    private BattlePhaseStart _startPhase;
    private BattlePhaseDraw _drawPhase;
    private BattlePhaseSelection _selectionPhase;
    private BattlePhaseFusion _fusionPhase;
    private BattlePhaseAction _actionPhase;
    private BattlePhaseAttack _attackPhase;
    private BattlePhaseDamage _damagePhase;
    private BattlePhaseActionTwo _actionTwoPhase;
    private BattlePhaseEnd _endPhase;

    //DEBUG
    [SerializeField] private string CURRENTSTATE;

    public BattleAbstract CurrentState => _currentState;

    private void Awake() {
        SetStates();
    }

    private void Start() {
        _currentState = _startPhase;
        _currentState.EnterState();
    }

    private void Update() {
        CURRENTSTATE = _currentState.ToString();
        _currentState.Update();
    }


    public void ChangeState(BattleAbstract newState){
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    private void SetStates(){
        _startPhase = new BattlePhaseStart();
        _drawPhase = new BattlePhaseDraw();
        _selectionPhase = new BattlePhaseSelection();
        _fusionPhase = new BattlePhaseFusion();
        _actionPhase = new BattlePhaseAction();
        _attackPhase = new BattlePhaseAttack();
        _damagePhase = new BattlePhaseDamage();
        _actionTwoPhase = new BattlePhaseActionTwo();
        _endPhase = new BattlePhaseEnd();
    }
    public BattlePhaseStart BattlePhaseStart => _startPhase;
    public BattlePhaseDraw BattlePhaseDraw => _drawPhase;
    public BattlePhaseSelection BattlePhaseSelection => _selectionPhase;
    public BattlePhaseFusion BattlePhaseFusion => _fusionPhase;
    public BattlePhaseAction BattlePhaseAction => _actionPhase;
    public BattlePhaseAttack BattlePhaseAttack => _attackPhase;
    public BattlePhaseDamage BattlePhaseDamage => _damagePhase;
    public BattlePhaseActionTwo BattlePhaseActionTwo => _actionTwoPhase;
    public BattlePhaseEnd BattlePhaseEnd => _endPhase;
}