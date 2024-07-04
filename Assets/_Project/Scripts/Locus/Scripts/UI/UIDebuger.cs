using UnityEngine.UIElements;

public class UIDebuger : UIManager {
    private Label _battleStateLabel;
    
    private void OnEnable() {
        Battle.OnStateChange += Battle_OnStateChange;
    }

    private void OnDisable() {
        Battle.OnStateChange -= Battle_OnStateChange;
    }

    private void Battle_OnStateChange(AbstractState state){
        SetElements();
        _battleStateLabel.text = $"Battle Phase: {state}";
    }

    private void SetElements(){
        _battleStateLabel = Root.Q<Label>("BattleState");
    }
}