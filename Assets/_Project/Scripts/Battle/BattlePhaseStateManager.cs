using UnityEngine;

public class BattlePhaseStateManager : MonoBehaviour {

    public BattleAbstract _currentState;
    private BattlePhaseStart _startPhase;
    private BattlePhaseDraw _drawPhase;
    private BattlePhaseCardSelection _selectionPhase;
    private BattlePhaseFusion _fusionPhase;
    private BattlePhaseFaceSelection _faceSelectionPhase;
    private BattlePhaseMonsterModeSelection _monsterModeSelectionPhase;
    private BattlePhaseSelectAnima _animaSelectionPhase;
    private BattlePhaseBoardPlaceSelection _boardPlaceSelectionPhase;
    private BattlePhaseAction _actionPhase;
    private BattlePhaseAttack _attackPhase;
    private BattlePhaseDamage _damagePhase;
    private BattlePhaseActionTwo _actionTwoPhase;
    private BattlePhaseEnd _endPhase;

    //DEBUG
    [SerializeField] private string CURRENTPHASE;
    private EStateMachinePhase _currentPhase;

    private void Awake() {
        SetStates();
    }

    private void Start() {
        _currentState = _startPhase;
        _currentState.EnterState();
    }

    private void Update(){
        _currentState.Update();
    }

    public void ChangeState(BattleAbstract newState){
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
        UpdateCurrentPhase();
    }

    private void SetStates(){
        _startPhase = new BattlePhaseStart();
        _drawPhase = new BattlePhaseDraw();
        _selectionPhase = new BattlePhaseCardSelection();
        _fusionPhase = new BattlePhaseFusion();
        _faceSelectionPhase = new BattlePhaseFaceSelection();
        _monsterModeSelectionPhase = new BattlePhaseMonsterModeSelection();
        _animaSelectionPhase = new BattlePhaseSelectAnima();
        _boardPlaceSelectionPhase = new BattlePhaseBoardPlaceSelection();
        _actionPhase = new BattlePhaseAction();
        _attackPhase = new BattlePhaseAttack();
        _damagePhase = new BattlePhaseDamage();
        _actionTwoPhase = new BattlePhaseActionTwo();
        _endPhase = new BattlePhaseEnd();
    }
    
    public BattleAbstract CurrentPhase => _currentState;
    public BattlePhaseStart BattlePhaseStart => _startPhase;
    public BattlePhaseDraw BattlePhaseDraw => _drawPhase;
    public BattlePhaseCardSelection BattlePhaseCardSelection => _selectionPhase;
    public BattlePhaseFusion BattlePhaseFusion => _fusionPhase;
    public BattlePhaseFaceSelection BattlePhaseFaceSelection => _faceSelectionPhase;
    public BattlePhaseMonsterModeSelection BattlePhaseMonsterModeSelection => _monsterModeSelectionPhase;
    public BattlePhaseSelectAnima BattlePhaseSelectAnima => _animaSelectionPhase;
    public BattlePhaseBoardPlaceSelection BattleBoardSelectionPhase => _boardPlaceSelectionPhase;
    public BattlePhaseAction BattlePhaseAction => _actionPhase;
    public BattlePhaseAttack BattlePhaseAttack => _attackPhase;
    public BattlePhaseDamage BattlePhaseDamage => _damagePhase;
    public BattlePhaseActionTwo BattlePhaseActionTwo => _actionTwoPhase;
    public BattlePhaseEnd BattlePhaseEnd => _endPhase;

    //Debug Label Update//
    public void SetBattlePhase(EStateMachinePhase _newPhase){_currentPhase = _newPhase;}
    public string GetCurrentBattlePhase() => CURRENTPHASE;
    private void UpdateCurrentPhase(){
        switch (_currentPhase){
            case EStateMachinePhase.Start:
                CURRENTPHASE = "Start";
                break;
            case EStateMachinePhase.Draw:
                CURRENTPHASE = "Draw";
                break;
            case EStateMachinePhase.CardSelection:
                CURRENTPHASE = "Card Sel.";
                break;
            case EStateMachinePhase.Fusion:
                CURRENTPHASE = "Fusion";
                break;
            case EStateMachinePhase.MonsterModeSelection:
                CURRENTPHASE = "Mons. Mode Sel.";
                break;
            case EStateMachinePhase.FaceSelection:
                CURRENTPHASE = "Face Sel.";
                break;
            case EStateMachinePhase.AnimaSelection:
                CURRENTPHASE = "Anima Sel.";
                break;
            case EStateMachinePhase.BoardPlaceSelection:
                CURRENTPHASE = "Board Place Sel.";
                break;
            case EStateMachinePhase.Action:
                CURRENTPHASE = "Action";
                break;
            case EStateMachinePhase.Attack:
                CURRENTPHASE = "Attack";
                break;
            case EStateMachinePhase.Damage:
                CURRENTPHASE = "Damage";
                break;
            case EStateMachinePhase.ActionTwo:
                CURRENTPHASE = "Action Two";
                break;
            case EStateMachinePhase.End:
                CURRENTPHASE = "End";
                break;
            default:
                CURRENTPHASE = "Error";
                break;
        }
    }
}