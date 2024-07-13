using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBattleScene : UIManager {
    [SerializeField] private CardEventHandlerSO CardManager;
    [SerializeField] private UIEventHandlerSO UIManager;
    [SerializeField] private BattleEventHandlerSO BattleManager;

    private Button _phaseButton, _phaseEndButton;

    private void OnEnable() {
        CardManager.OnSomeCardSelected.AddListener(CardManager_OnSomeCardSelected);
        CardManager.OnNoneCardSelected.AddListener(CardManager_OnNoneCardSelected);
        BattleManager.OnActionPhaseStart.AddListener(BattleManager_OnActionPhaseStart);
    }

    private void OnDisable() {
        CardManager.OnSomeCardSelected.RemoveListener(CardManager_OnSomeCardSelected);
        CardManager.OnNoneCardSelected.RemoveListener(CardManager_OnNoneCardSelected);
        BattleManager.OnActionPhaseStart.RemoveListener(BattleManager_OnActionPhaseStart);
    }

    public override void Awake() {
        base.Awake();
        SetElements();
        _phaseButton.style.display = DisplayStyle.None;
    }

    private void BattleManager_OnActionPhaseStart(){
        SetElements();
        _phaseEndButton.text = "End Phase";
        _phaseEndButton.style.display = DisplayStyle.Flex;
        
        if(_phaseButton != null){
            _phaseButton.clicked -= EndActionPhase;
        }

        _phaseEndButton.clicked -= EndActionPhase;
        _phaseEndButton.clicked += EndActionPhase;
        
    }

    private void EndActionPhase(){
        Debug.Log("UIBattleScene - EndActionPhase()");
        BattleManager.EndActionPhase();
        _phaseEndButton.style.display = DisplayStyle.None;

        if(_phaseEndButton != null){
            _phaseEndButton.clicked -= EndActionPhase;
        }
    }

    private void CardManager_OnSomeCardSelected(){
        SetElements();
        _phaseButton.text = "End Selection";
        _phaseButton.style.display = DisplayStyle.Flex;

        _phaseEndButton.clicked -= SelectionFinished;
        _phaseEndButton.clicked += SelectionFinished;
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
        _phaseEndButton = Root.Q<Button>("ActionPhaseButton");

        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;
    }
}