using UnityEngine;
using UnityEngine.UIElements;

public class UIDebuger : UIManager {
    [SerializeField] private BattleEventHandlerSO BattleManager;

    private Label _battleStateLabel;
    
    private void OnEnable() {
        // Battle.OnStateChange += Battle_OnStateChange;
        BattleManager.OnStateChange.AddListener(BattleManager_OnStateChange);
    }

    private void OnDisable() {
        // Battle.OnStateChange -= Battle_OnStateChange;
        BattleManager.OnStateChange.RemoveListener(BattleManager_OnStateChange);
    }

    private void BattleManager_OnStateChange(AbstractState state){
        SetElements();
        _battleStateLabel.text = $"Battle Phase: {state}";
    }

    private void SetElements(){
        _battleStateLabel = Root.Q<Label>("BattleState");
    }
}