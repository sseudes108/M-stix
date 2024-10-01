using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoardPlaceManagerSO", menuName = "Mistix/Manager/BoardPlace", order = 0)]
public class BoardManagerSO : ScriptableObject {
    [HideInInspector] public UnityEvent OnBoardPlaceSelected;
    [HideInInspector] public UnityEvent<BoardPlace> OnShowOptions;
    [HideInInspector] public UnityEvent OnHideOptions;
    [HideInInspector] public UnityEvent OnBoardFusion;

    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private UIEventHandlerSO _uIManager;
    [SerializeField] private FusionManagerSO _fusionManager;

    public BoardPlaceVisualController BoardVisualController {get; private set;}

    private void OnEnable() {
        OnBoardFusion ??= new UnityEvent();
        OnBoardPlaceSelected ??= new UnityEvent();
        OnShowOptions ??= new UnityEvent<BoardPlace>();
        OnHideOptions ??= new UnityEvent();

        _battleManager.OnStartPhase.AddListener(BattleManager_OnStartPhase);
        _battleManager.OnBoardPlaceSelectionStart.AddListener(BattleManager_BoardPlaceSelectionStart);
        _battleManager.OnBoardPlaceSelectionEnd.AddListener(BattleManager_BoardPlaceSelectionEnd);
        _uIManager.OnMonsterAttack.AddListener(UIManager_MonsterAttack);
    }

    public void OnDisable(){
        _battleManager.OnStartPhase.RemoveListener(BattleManager_OnStartPhase);
        _battleManager.OnBoardPlaceSelectionStart.RemoveListener(BattleManager_BoardPlaceSelectionStart);
        _battleManager.OnBoardPlaceSelectionEnd.RemoveListener(BattleManager_BoardPlaceSelectionEnd);
        _uIManager.OnMonsterAttack.RemoveListener(UIManager_MonsterAttack);
    }

    //listen Events
    private void BattleManager_OnStartPhase() { BoardVisualController.OnStartPhase(); }
    private void BattleManager_BoardPlaceSelectionStart(Card card, bool isPlayerTurn) { BoardVisualController.OnBoardPlaceSelectionStart(card, isPlayerTurn); }
    private void BattleManager_BoardPlaceSelectionEnd(Card card, bool isPlayerTurn) { BoardVisualController.OnBoardPlaceSelectionEnd(isPlayerTurn); }
    private void UIManager_MonsterAttack(Card card, bool isPlayerTurn) { BoardVisualController.OnMonsterAttack(card, isPlayerTurn); }

    //Board Events
    public void BoardPlaceSelected() { OnBoardPlaceSelected?.Invoke(); }
    public void ShowOptions(BoardPlace place) { OnShowOptions?.Invoke(place); }
    public void HideOptions() { OnHideOptions?.Invoke(); }

    public void BoardFusion() {
        OnBoardFusion?.Invoke();
    }

    //Custom Methods
    public void SetBoardPlaceVisualController(BoardPlaceVisualController boardPlaces){
        BoardVisualController = boardPlaces;
    }
}