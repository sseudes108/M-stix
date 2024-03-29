using UnityEngine;

public class BattlePhaseStateManager : MonoBehaviour {

    private BattleAbstract _currentState;
    private BattlePhaseStart _startPhase;
    private BattlePhaseDraw _drawPhase;
    private BattlePhaseCardSelection _cardSelectionPhase;
    private BattlePhaseFusion _fusionPhase;
    private BattlePhaseSelections _selectionsPhase;
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
        _cardSelectionPhase = new BattlePhaseCardSelection();
        _fusionPhase = new BattlePhaseFusion();
        _selectionsPhase = new BattlePhaseSelections();
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
    public BattlePhaseCardSelection BattlePhaseCardSelection => _cardSelectionPhase;
    public BattlePhaseFusion BattlePhaseFusion => _fusionPhase;
    public BattlePhaseSelections BattlePhaseSelections => _selectionsPhase;
    public BattlePhaseBoardPlaceSelection BattleBoardSelectionPhase => _boardPlaceSelectionPhase;
    public BattlePhaseAction BattlePhaseAction => _actionPhase;
    public BattlePhaseAttack BattlePhaseAttack => _attackPhase;
    public BattlePhaseDamage BattlePhaseDamage => _damagePhase;
    public BattlePhaseActionTwo BattlePhaseActionTwo => _actionTwoPhase;
    public BattlePhaseEnd BattlePhaseEnd => _endPhase;

    //Debug Label Update//
    public void SetBattlePhase(EStateMachinePhase _newPhase){
        _currentPhase = _newPhase;
    }
    
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
            case EStateMachinePhase.Selections:
                CURRENTPHASE = "Selections";
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
        BattleManager.Instance.UIBattleManager.UpdateStateMachineState(CURRENTPHASE);
    }
}