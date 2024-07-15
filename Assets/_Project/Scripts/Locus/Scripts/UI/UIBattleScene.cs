using UnityEngine;
using UnityEngine.UIElements;

public class UIBattleScene : UI {
    [SerializeField] private CardManagerSO _cardManager;
    [SerializeField] private UIEventHandlerSO _uIManager;
    [SerializeField] private BattleManagerSO _battleManager;

    private Button _phaseButton, _phaseEndButton;

    private void OnEnable() {
        _cardManager.OnSomeCardSelected.AddListener(CardManager_OnSomeCardSelected);
        _cardManager.OnNoneCardSelected.AddListener(CardManager_OnNoneCardSelected);
        _battleManager.OnActionPhaseStart.AddListener(BattleManager_OnActionPhaseStart);
    }

    private void OnDisable() {
        _cardManager.OnSomeCardSelected.RemoveListener(CardManager_OnSomeCardSelected);
        _cardManager.OnNoneCardSelected.RemoveListener(CardManager_OnNoneCardSelected);
        _battleManager.OnActionPhaseStart.RemoveListener(BattleManager_OnActionPhaseStart);
    }

    public override void Awake() {
        base.Awake();
        SetElements();
        _phaseButton.style.display = DisplayStyle.None;
    }

    // private void Start(){
    //     SetElements();
    //     _phaseButton.style.display = DisplayStyle.None;
    // }

    private void BattleManager_OnActionPhaseStart(){
        SetElements();
        _phaseEndButton.text = "End Phase";
        _phaseEndButton.style.display = DisplayStyle.Flex;
        
        _phaseButton.clicked -= EndActionPhase;

        _phaseEndButton.clicked -= EndActionPhase;
        _phaseEndButton.clicked += EndActionPhase;
        
    }

    private void EndActionPhase(){
        _battleManager.EndActionPhase();

        _phaseEndButton.style.display = DisplayStyle.None;
        _phaseEndButton.clicked -= EndActionPhase;

    }

    private void CardManager_OnSomeCardSelected(){
        SetElements();

        _phaseButton.clicked -= EndActionPhase;

        _phaseButton.text = "End Selection";
        _phaseButton.style.display = DisplayStyle.Flex;

        _phaseEndButton.clicked -= SelectionFinished;
        _phaseEndButton.clicked += SelectionFinished;
    }

    private void CardManager_OnNoneCardSelected(){
        _phaseButton.style.display = DisplayStyle.None;

    }

    private void SelectionFinished(){
        _uIManager.CardSelectionFinished();
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