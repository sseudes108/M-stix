using UnityEngine;

public class AIStateManager : MonoBehaviour {
    private AIAbstract _AICurrentState;

    //States
    private AIStateStandBy _AIStateStandBy;
    private AIStateCardSelection _AIStateCardSelection;

    //Components
    private AICardSelector _AICardSelector;
    private AIAfterFusionSelector _AIAfterFusionSelector;
    private AIBoardPlaceSelector _AIBoardPlaceSelector;

    //Archetype
    private AIArchetype _currentArchetype;
    private AIArchetype _agroMonsterFocused;

    public string AICurrentState;

    private void Awake() {
        SetStates();
        SetComponents();
    }
    
    private void Start() {
        _AICurrentState = _AIStateStandBy;
        _AICurrentState.EnterState();
        _currentArchetype = _agroMonsterFocused;
    }

    private void Update() {
        AICurrentState = _AICurrentState.ToString();
    }

    public void ChangeState(AIAbstract newState){
        _AICurrentState.ExitState();
        _AICurrentState = newState;
        _AICurrentState.EnterState();
    }

    private void SetStates(){
        _AIStateCardSelection = new AIStateCardSelection();
        _AIStateStandBy = new AIStateStandBy();

        _agroMonsterFocused = new AIAgroMonstersFocused();
    }

    private void SetComponents(){
        _AICardSelector = GetComponent<AICardSelector>();
        _AIAfterFusionSelector = GetComponentInChildren<AIAfterFusionSelector>();
        _AIBoardPlaceSelector = GetComponent<AIBoardPlaceSelector>();
    }

    //States
    public AIStateCardSelection AICardSelection => _AIStateCardSelection;
    public AIStateStandBy AIStandby => _AIStateStandBy;

    //Components
    public AICardSelector CardSelector => _AICardSelector;
    public AIAfterFusionSelector AfterFusionSelector => _AIAfterFusionSelector;
    public AIBoardPlaceSelector BoardPlaceSelector => _AIBoardPlaceSelector;

    //Archetypes
    public AIArchetype CurrentArchetype => _currentArchetype;
}