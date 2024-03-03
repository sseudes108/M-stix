using UnityEngine;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private FusionManager _fusionManager;
    [SerializeField] private UIBattleManager _uiBattleManager;

    [SerializeField] private HandPlayer _playerHand;
    [SerializeField] private HandEnemy _enemyHand;

    //Public Refs//

    //UI
    public UIBattleManager UIBattleManager => _uiBattleManager;

    //Card
    public CardCreator CardCreator => _cardManager.CardCreator;
    public CardSelector CardSelector => _cardManager.CardSelector;
    public CardsDatabase CardsDatabase => _cardManager.CardsDatabase;

    //Fusion
    public Fusion Fusion => _fusionManager.Fusion;
    public FusionMonster FusionMonster => _fusionManager.FusionMonster;
    public FusionArcane FusionArcane => _fusionManager.FusionArcane;
    public FusionEquip FusionEquip => _fusionManager.FusionEquip;

    public FusionPositions FusionPositions => _fusionManager.FusionPositions;
    public FusionVisuals FusionVisuals => _fusionManager.FusionVisuals;

    //Hands
    public HandPlayer PlayerHand => _playerHand;
    public HandEnemy EnemyHand => _enemyHand;

    private void Awake(){
        SetSingleton();
        SetComponents();
    }

    private void SetSingleton(){
        if (Instance != null){
            Debug.Log("More than one instance of BattleManager");
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void SetComponents(){
        _cardManager = GetComponentInChildren<CardManager>();
        _fusionManager = GetComponentInChildren<FusionManager>();
        _uiBattleManager = GetComponentInChildren<UIBattleManager>();
    }
}