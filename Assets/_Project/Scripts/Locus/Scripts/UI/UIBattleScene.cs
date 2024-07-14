using UnityEngine;
using UnityEngine.UIElements;

public class UIBattleScene : UIManager {
    [SerializeField] private CardManagerSO CardManager;
    [SerializeField] private UIEventHandlerSO UIManager;
    [SerializeField] private BattleManagerSO BattleManager;
    // [SerializeField] private BattleManagerSO BattleManager;

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
        // if(_phaseButton != null){
        //     _phaseButton.style.display = DisplayStyle.None;
        // }
    }

    private void BattleManager_OnActionPhaseStart(){
        SetElements();
        _phaseEndButton.text = "End Phase";
        _phaseEndButton.style.display = DisplayStyle.Flex;
        
        // if(_phaseButton != null){
        //     _phaseButton.clicked -= EndActionPhase;
        // }
        _phaseButton.clicked -= EndActionPhase;

        _phaseEndButton.clicked -= EndActionPhase;
        _phaseEndButton.clicked += EndActionPhase;
        
    }

    private void EndActionPhase(){
        // Debug.Log("UIBattleScene - EndActionPhase()");
        BattleManager.EndActionPhase();
        // if(_phaseEndButton != null){
        //     _phaseEndButton.style.display = DisplayStyle.None;
        //     _phaseEndButton.clicked -= EndActionPhase;
        // }
        _phaseEndButton.style.display = DisplayStyle.None;
        _phaseEndButton.clicked -= EndActionPhase;

    }

    private void CardManager_OnSomeCardSelected(){
        // Debug.Log("UIBattleScene - CardManager_OnSomeCardSelected()");
        SetElements();
        // if(_phaseButton != null){
        //     _phaseButton.clicked -= EndActionPhase;

        //     _phaseButton.text = "End Selection";
        //     _phaseButton.style.display = DisplayStyle.Flex;

        //     _phaseEndButton.clicked -= SelectionFinished;
        //     _phaseEndButton.clicked += SelectionFinished;
        // }

        _phaseButton.clicked -= EndActionPhase;

        _phaseButton.text = "End Selection";
        _phaseButton.style.display = DisplayStyle.Flex;

        _phaseEndButton.clicked -= SelectionFinished;
        _phaseEndButton.clicked += SelectionFinished;
    }

    private void CardManager_OnNoneCardSelected(){
        // if(_phaseButton != null){
        //     _phaseButton.style.display = DisplayStyle.None;
        // }
        _phaseButton.style.display = DisplayStyle.None;

    }

    private void SelectionFinished(){
        UIManager.CardSelectionFinished();
        // if(_phaseButton != null){
        //     _phaseButton.style.display = DisplayStyle.None;
        //     _phaseButton.clicked -= SelectionFinished;
        // }
        _phaseButton.style.display = DisplayStyle.None;
        _phaseButton.clicked -= SelectionFinished;
    }

    private void SetElements(){
        _phaseButton = null;
        _phaseButton = Root.Q<Button>("PhaseButton");
        _phaseEndButton = Root.Q<Button>("ActionPhaseButton");

        _phaseButton.clicked -= SelectionFinished;
        _phaseButton.clicked += SelectionFinished;

        // if(_phaseButton != null){
        //     _phaseButton.clicked -= SelectionFinished;
        //     _phaseButton.clicked += SelectionFinished;
        // }
    }
}