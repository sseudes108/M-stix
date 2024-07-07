using System;
using UnityEngine.UIElements;

public class UIBattleScene : UIManager {

    public static Action OnSelectionFinished;
    private Button _phaseButton;

    private void OnEnable() {
        CardSelector.OnSomeCardSelected += CardSelector_OnSomeCardSelected;
        CardSelector.OnNoneCardSelected += CardSelector_OnNoneCardSelected;
    }

    private void OnDisable() {
        CardSelector.OnSomeCardSelected -= CardSelector_OnSomeCardSelected;
        CardSelector.OnNoneCardSelected -= CardSelector_OnNoneCardSelected;
    }

    public override void Awake() {
        base.Awake();
        SetElements();
        _phaseButton.style.display = DisplayStyle.None;
    }

    private void CardSelector_OnSomeCardSelected(){
        SetElements();
        _phaseButton.text = "End Selection";
        _phaseButton.style.display = DisplayStyle.Flex;
        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;
    }

    private void CardSelector_OnNoneCardSelected(){
        _phaseButton.style.display = DisplayStyle.None;
    }

    private void SelectionFinished(){
        OnSelectionFinished?.Invoke();
        _phaseButton.style.display = DisplayStyle.None;
        _phaseButton.clicked -= SelectionFinished;
    }

    private void SetElements(){
        _phaseButton = null;
        _phaseButton = Root.Q<Button>("PhaseButton");
        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;
    }
}