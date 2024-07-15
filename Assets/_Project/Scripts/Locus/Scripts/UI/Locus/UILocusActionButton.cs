using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILocusActionButton : UILocus{
    [SerializeField] private CardManagerSO _cardManager;
    [SerializeField] private UIEventHandlerSO _uIManager;
    [SerializeField] private BattleManagerSO _battleManager;

    public GameObject _actionButtonContainer;
    public Button _actionButton;
    public TextMeshProUGUI _actionButtonText;

    private void OnEnable() {
        _cardManager.OnSomeCardSelected.AddListener(CardManager_OnSomeCardSelected);
        _cardManager.OnNoneCardSelected.AddListener(CardManager_OnNoneCardSelected);
        _battleManager.OnActionPhaseStart.AddListener(BattleManager_OnActionPhaseStart);
    }

    private void OnDisable() {
        _cardManager.OnSomeCardSelected.RemoveListener(CardManager_OnSomeCardSelected);
        _cardManager.OnNoneCardSelected.RemoveListener(CardManager_OnNoneCardSelected);
        _battleManager.OnActionPhaseStart.RemoveListener(BattleManager_OnActionPhaseStart);

        _actionButton.onClick.RemoveAllListeners();
    }

    private void BattleManager_OnActionPhaseStart(){
        _actionButtonText.text = "End Phase";
        _actionButtonContainer.SetActive(true);
        _actionButton.onClick.AddListener(EndActionPhase);
    }

    private void CardManager_OnSomeCardSelected(){
        _actionButtonText.text = "End Selection";
        _actionButtonContainer.SetActive(true);
        _actionButton.onClick.AddListener(EndSelection);
    }

    private void CardManager_OnNoneCardSelected(){
        _actionButtonContainer.SetActive(false);
    }

    private void EndActionPhase(){
        _battleManager.EndActionPhase();
        _actionButtonContainer.SetActive(false);
        _actionButton.onClick.RemoveListener(EndActionPhase);
    }

    private void EndSelection(){
        _uIManager.CardSelectionFinished();
        _actionButtonContainer.SetActive(false);
        _actionButton.onClick.RemoveListener(EndSelection);
    }
}