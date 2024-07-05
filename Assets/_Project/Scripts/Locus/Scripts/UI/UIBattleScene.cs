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
        _phaseButton.style.opacity = 0;
    }

    private void CardSelector_OnSomeCardSelected(){
        // Debug.Log("CardSelector_OnSomeCardSelected called");
        SetElements();
        _phaseButton.text = "End Selection";
        _phaseButton.style.opacity = 1;
        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;
    }

    private void CardSelector_OnNoneCardSelected(){
        // Debug.Log("CardSelector_OnNoneCardSelected called");
        _phaseButton.style.opacity = 0;
    }

    private void SelectionFinished(){
        // Debug.Log("SelectionFinished called");
        OnSelectionFinished?.Invoke();
        _phaseButton.style.opacity = 0;
        _phaseButton.clicked -= SelectionFinished;
    }

    private void SetElements(){
        // StartCoroutine(ElementsRoutine());
        // PhaseButton.clicked -= SelectionFinished;
        _phaseButton = null;
        _phaseButton = Root.Q<Button>("PhaseButton");
        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;
    }


}