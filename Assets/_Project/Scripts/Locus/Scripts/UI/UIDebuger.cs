using UnityEngine;
using UnityEngine.UIElements;

public class UIDebuger : UIManager {
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private TurnManagerSO _turnManager;
    [SerializeField] private AIManagerSO _aiManager;

    private Label _battleStateLabel;
    private Label _turnInfoLabel;
    private Label _AIStateLabel;
    
    private void OnEnable() {
        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
        _battleManager.OnStateChange.AddListener(BattleManager_OnStateChange);
        _aiManager.OnStateChange.AddListener(AIManager_OnStateChange);
    }

    private void OnDisable() {
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
        _battleManager.OnStateChange.RemoveListener(BattleManager_OnStateChange);
        _aiManager.OnStateChange.AddListener(AIManager_OnStateChange);
    }

    private void BattleManager_OnStateChange(AbstractState state){
        SetElements();
        _battleStateLabel.text = $"Battle Phase: {state}";
    }

    private void BattleManager_OnStartPhase(){
        UpdateTurnLabel();
    }

    private void AIManager_OnStateChange(){
        Debug.Log("UIDebuger - AIManager_OnStateChange()");
        UpdateAiStateLabel();
    }

    private void SetElements(){
        _battleStateLabel = Root.Q<Label>("BattleState");
        _turnInfoLabel = Root.Q<Label>("TurnInfo");
        _AIStateLabel = Root.Q<Label>("AIState");
    }

    private void UpdateTurnLabel(){
        SetElements();
        _turnInfoLabel.text = $"Turn: {_turnManager.CurrentTurn} - {_turnManager.GetTurnOwner()}";
    }

    private void UpdateAiStateLabel(){
        Debug.Log($"UIDebuger - UpdateAiStateLabel() - CurrentState {_aiManager.CurrentState}");
        SetElements();
        _AIStateLabel.text = $"AI State: {_aiManager.CurrentState}";
    }
}