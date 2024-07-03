using System;
using UnityEngine.UIElements;

public class UIBattleScene : UIManager {
    public static Action OnSelectionFinished;
    public Button _phaseButton;


#region Unity Methods
    private void OnEnable() {
        CardSelector.OnSomeCardSelected += CardSelector_OnSomeCardSelected;
        CardSelector.OnNoneCardSelected += CardSelector_OnNoneCardSelected;

        if (_phaseButton != null) {
            _phaseButton.clicked += SelectionFinished;
        }
    }
    private void OnDisable() {
        CardSelector.OnSomeCardSelected += CardSelector_OnSomeCardSelected;
        CardSelector.OnNoneCardSelected += CardSelector_OnNoneCardSelected;

        if (_phaseButton != null) {
            _phaseButton.clicked -= SelectionFinished;
        }
    }

    public override void Awake() {
        base.Awake();
        SetElements();
    }

#endregion

#region Events Methods

    private void CardSelector_OnSomeCardSelected(){
        SetElements();
        _phaseButton.text = "End Selection";
        _phaseButton.style.opacity = 1;
    }

    private void CardSelector_OnNoneCardSelected(){
        _phaseButton.style.opacity = 0;
    }

    private void SelectionFinished(){
        OnSelectionFinished?.Invoke();
        _phaseButton.style.opacity = 0;
        _phaseButton.clicked -= SelectionFinished;
    }

#endregion

#region Custom Methods Methods

    private void SetElements(){
        _phaseButton = Root.Q<Button>("PhaseButton");
        if(_phaseButton != null){
            _phaseButton.clicked -= SelectionFinished;
            _phaseButton.clicked += SelectionFinished;
        }
    }

#endregion

}