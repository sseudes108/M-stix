using UnityEngine;

public class GameManager : MonoBehaviour {
    public BattleEventHandlerSO BattleManager;
    public static GameManager Instance { get; private set;}

    public UIManager UIManager;
    public TurnManager TurnManager;
    public CardManager CardManager;
    public Battle BattleStateManager;
    public FusionManager Fusion;
    public UIManager UI;
    public HandManager Hand;
    public CameraManager Camera;
    public VisualEffectManager Visual;
    public BoardPlaceManager Board;

    private void Awake() {
        SetInstance();
        SetComponents();
    }

    private void SetInstance(){
        if(Instance == null) {
            Instance = this;
        }else{
            Debug.LogError("More Than One Instance of BattleManager");
        }
    }

    private void SetComponents(){
        UIManager = GetComponentInChildren<UIManager>();
        TurnManager = GetComponentInChildren<TurnManager>();
        CardManager = GetComponentInChildren<CardManager>();
        BattleStateManager = GetComponentInChildren<Battle>();   
        Fusion = GetComponentInChildren<FusionManager>();
        UI = GetComponentInChildren<UIManager>();
        Hand = GetComponentInChildren<HandManager>();
        Camera = GetComponentInChildren<CameraManager>();
        Visual = GetComponentInChildren<VisualEffectManager>();
        Board = GetComponentInChildren<BoardPlaceManager>();
    }
}