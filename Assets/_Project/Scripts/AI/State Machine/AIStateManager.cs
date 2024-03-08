using UnityEngine;

public class AIStateManager : MonoBehaviour {
    private AIAbstract _AICurrentState;

    //States
    private AIStateStandBy _AIStateStandBy;
    private AIStateCardSelection _AIStateCardSelection;

    //Components
    private AICardSelector _AICardSelector;
    private AIAfterFusionSelector _AIAfterFusionSelector;

    public string AICurrentState;

    private void Awake() {
        SetStates();
        SetComponents();
    }
    
    private void Start() {
        _AICurrentState = _AIStateStandBy;
        _AICurrentState.EnterState();
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
    }

    private void SetComponents(){
        _AICardSelector = GetComponent<AICardSelector>();
    }

    //States
    public AIStateCardSelection AICardSelection => _AIStateCardSelection;
    public AIStateStandBy AIStandby => _AIStateStandBy;

    //Components
    public AICardSelector CardSelector => _AICardSelector;
    public AIAfterFusionSelector AfterFusionSelector => _AIAfterFusionSelector;
}