using UnityEngine;
using UnityEngine.UIElements;

public class UIBattleScene : UIManager {
    [SerializeField] private CardEventHandlerSO CardManager;
    [SerializeField] private UIEventHandlerSO UIManager;
    private Button _phaseButton;

    private void OnEnable() {
        CardManager.OnSomeCardSelected.AddListener(CardManager_OnSomeCardSelected);
        CardManager.OnNoneCardSelected.AddListener(CardManager_OnNoneCardSelected);
    }

    private void OnDisable() {
        CardManager.OnSomeCardSelected.RemoveListener(CardManager_OnSomeCardSelected);
        CardManager.OnNoneCardSelected.RemoveListener(CardManager_OnNoneCardSelected);
    }

    public override void Awake() {
        base.Awake();
        SetElements();
        _phaseButton.style.display = DisplayStyle.None;
    }

    private void CardManager_OnSomeCardSelected(){
        SetElements();
        _phaseButton.text = "End Selection";
        _phaseButton.style.display = DisplayStyle.Flex;
        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;
    }

    private void CardManager_OnNoneCardSelected(){
        _phaseButton.style.display = DisplayStyle.None;
    }

    private void SelectionFinished(){
        UIManager.CardSelectionFinished();
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