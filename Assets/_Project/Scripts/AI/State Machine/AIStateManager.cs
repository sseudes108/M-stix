using UnityEngine;

public class AIStateManager : MonoBehaviour {
    private AIAbstract _AICurrentState;

    //States
    private AIStateStandBy _AIStateStandBy;
    private AIStateCardSelection _AIStateCardSelection;
    private AIStateAttack _AIStateAttack;

    //Components
    private AICardSelector _AICardSelector;
    private AIAfterFusionSelector _AIAfterFusionSelector;
    private AIBoardPlaceSelector _AIBoardPlaceSelector;
    private AIAttacker _AIAttacker;

    //Archetype
    private AIArchetype _currentArchetype;
    private AIArchetype _agroMonsterFocused;

    public string AICurrentState;

    private void Awake() {
        SetStates();
        SetArchetypes();
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
        _AIStateAttack = new AIStateAttack();

    }

    private void SetArchetypes(){
        _agroMonsterFocused = new AIAgroMonstersFocused();
    }

    private void SetComponents(){
        _AICardSelector = GetComponent<AICardSelector>();
        _AIAfterFusionSelector = GetComponentInChildren<AIAfterFusionSelector>();
        _AIBoardPlaceSelector = GetComponent<AIBoardPlaceSelector>();
        _AIAttacker = GetComponent<AIAttacker>();
    }

#region Public References

    // States
    public AIStateCardSelection AICardSelection => _AIStateCardSelection;
    public AIStateStandBy AIStandby => _AIStateStandBy;
    public AIStateAttack AIAttack => _AIStateAttack;

    // Components
    public AICardSelector CardSelector => _AICardSelector;
    public AIAfterFusionSelector AfterFusionSelector => _AIAfterFusionSelector;
    public AIBoardPlaceSelector BoardPlaceSelector => _AIBoardPlaceSelector;
    public AIAttacker AIAttacker => _AIAttacker;

    // Archetypes
    public AIArchetype CurrentArchetype => _currentArchetype;

#endregion
}