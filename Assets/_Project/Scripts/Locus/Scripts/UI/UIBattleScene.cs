using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBattleScene : UIManager {
    public static Action OnSelectionFinished;
    public Button _phaseButton;

#region Unity Methods
    private void OnEnable() {
        Debug.Log("OnEnable called");

        CardSelector.OnSomeCardSelected += CardSelector_OnSomeCardSelected;
        CardSelector.OnNoneCardSelected += CardSelector_OnNoneCardSelected;

        if (_phaseButton != null) {
            _phaseButton.clicked += SelectionFinished;
            Debug.Log("Button click event registered in OnEnable");
        }
    }

    private void OnDisable() {
        Debug.Log("OnDisable called");

        CardSelector.OnSomeCardSelected -= CardSelector_OnSomeCardSelected;
        CardSelector.OnNoneCardSelected -= CardSelector_OnNoneCardSelected;

        if (_phaseButton != null) {
            _phaseButton.clicked -= SelectionFinished;
            Debug.Log("Button click event unregistered in OnDisable");
        }
    }

    public override void Awake() {
        base.Awake();
        Debug.Log("Awake called");
        SetElements();
    }

#endregion

#region Events Methods

    private void CardSelector_OnSomeCardSelected(){
        Debug.Log("CardSelector_OnSomeCardSelected called");
        SetElements();
        _phaseButton.text = "End Selection";
        _phaseButton.style.opacity = 1;
    }

    private void CardSelector_OnNoneCardSelected(){
        Debug.Log("CardSelector_OnNoneCardSelected called");
        _phaseButton.style.opacity = 0;
    }

    private void SelectionFinished(){
        Debug.Log("SelectionFinished called");
        OnSelectionFinished?.Invoke();
        _phaseButton.style.opacity = 0;
        _phaseButton.clicked -= SelectionFinished;
    }

#endregion

#region Custom Methods Methods

    private void SetElements(){
        Debug.Log("SetElements called");
        _phaseButton = Root.Q<Button>("PhaseButton");
        if (_phaseButton != null) {
            _phaseButton.clicked -= SelectionFinished; // Desregistrar primeiro para evitar múltiplos registros
            _phaseButton.clicked += SelectionFinished;
            Debug.Log("Button click event registered in SetElements");
        } else {
            Debug.LogWarning("PhaseButton not found in SetElements");
        }
    }

#endregion
}
