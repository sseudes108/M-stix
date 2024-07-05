using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set;}

    public UIManager UIManager { get; private set; }
    public TurnManager TurnManager { get; private set; }
    public CardManager CardManager { get; private set; }
    public Battle BattleStateManager { get; private set; }
    public FusionManager Fusion { get; private set; }
    public UIManager UI { get; private set; }

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
    }
}