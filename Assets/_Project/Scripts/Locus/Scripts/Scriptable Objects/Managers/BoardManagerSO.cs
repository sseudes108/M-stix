using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoardPlaceManagerSO", menuName = "Mistix/Manager/BoardPlace", order = 0)]
public class BoardManagerSO : ScriptableObject {
    [HideInInspector] public UnityEvent OnBoardPlaceSelected;
    [HideInInspector] public UnityEvent<BoardPlace> OnShowOptions;
    [HideInInspector] public UnityEvent OnHideOptions;

    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private UIEventHandlerSO _uIManager;

    private BoardPlaceVisualController _boardVisualController;
    
    private void OnEnable() {
        OnBoardPlaceSelected ??= new UnityEvent();
        OnShowOptions ??= new UnityEvent<BoardPlace>();
        OnHideOptions ??= new UnityEvent();   

        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
        _battleManager.OnBoardPlaceSelectionStart.AddListener(BattleManager_BoardPlaceSelectionStart);
        _battleManager.OnBoardPlaceSelectionEnd.AddListener(BattleManager_BoardPlaceSelectionEnd);
        _uIManager.OnMonsterAttack.AddListener(UIManager_OnMonsterAttack);
    }

    public void OnDisable(){
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
        _battleManager.OnBoardPlaceSelectionStart.RemoveListener(BattleManager_BoardPlaceSelectionStart);
        _battleManager.OnBoardPlaceSelectionEnd.RemoveListener(BattleManager_BoardPlaceSelectionEnd);
        _uIManager.OnMonsterAttack.RemoveListener(UIManager_OnMonsterAttack);
    }

    //listen Events
    private void BattleManager_OnStartPhase() { _boardVisualController.OnStartPhase(); }
    private void BattleManager_BoardPlaceSelectionStart(Card card, bool isPlayerTurn) { _boardVisualController.OnBoardPlaceSelectionStart(card, isPlayerTurn); }
    private void BattleManager_BoardPlaceSelectionEnd(Card card, bool isPlayerTurn) { _boardVisualController.OnBoardPlaceSelectionEnd(isPlayerTurn); }
    private void UIManager_OnMonsterAttack(Card card, bool isPlayerTurn) { _boardVisualController.OnMonsterAttack(card, isPlayerTurn); }

    //Broad Events
    public void BoardPlaceSelected() { OnBoardPlaceSelected?.Invoke(); }
    public void ShowOptions(BoardPlace place) { OnShowOptions?.Invoke(place); }
    public void HideOptions() { OnHideOptions?.Invoke(); }

    //Custom Methods
    public void SetBoardPlaceVisualController(BoardPlaceVisualController boardPlaces){
        _boardVisualController = boardPlaces;
    }
}