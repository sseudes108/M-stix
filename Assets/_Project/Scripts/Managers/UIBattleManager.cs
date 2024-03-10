using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleManager : MonoBehaviour {
    [SerializeField] private UICardPlaceHolder _UICardPlaceHolder;
    [SerializeField] private TextMeshProUGUI _turnNumber, _turnOwner;
    [SerializeField] private TextMeshProUGUI _state;

    [Header("Player")]
    [SerializeField] private TextMeshProUGUI _playerHP;
    [SerializeField] private TextMeshProUGUI _playerDeck;

    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI _enemyHP;
    [SerializeField] private TextMeshProUGUI _enemyDeck;

    [SerializeField] private Button _endPhaseButton;

    private void OnEnable() {
        BattleManager.Instance.TurnManager.OnTurnEnd += UpdateTurn;
    }

    private void OnDisable() {
        BattleManager.Instance.TurnManager.OnTurnEnd -= UpdateTurn;
    }
    
    private void Awake() {
        _UICardPlaceHolder = GetComponentInChildren<UICardPlaceHolder>();
    }

    public void UpdateStateMachineState(string battlePhase){
        _state.text = $"{battlePhase}";
    }

    public void UpdateDeckCount(Hand hand){
        if(hand is HandPlayer){
            _playerDeck.text = $"Deck : {BattleManager.Instance.PlayerHand.GetDeck().DeckInUse.Count}";
        }else{
            _enemyDeck.text = $"Deck : {BattleManager.Instance.EnemyHand.GetDeck().DeckInUse.Count}";
        }
    }

    public void UpdatePlayerHealth(int playerHP){
        _playerHP.text = $"Player Hp : {playerHP}";
    }

    public void UpdateEnemyHealth(int enemyHP){
        _enemyHP.text = $"Enemy Hp : {enemyHP}";
    }
    
    public void UpdateTurn(bool isPlayerTurn){
        _turnNumber.text = $"Turn: {BattleManager.Instance.TurnManager.GetTurn()}";

        if(isPlayerTurn){
            _turnOwner.text = "Player Turn";
        }else{
            _turnOwner.text = "Enemy Turn";
        }
    }

    public void EndPhaseButton(){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _endPhaseButton.gameObject.SetActive(true);
            _endPhaseButton.onClick.AddListener(TriggerEndPhaseEvent);
        }
    }

    private void TriggerEndPhaseEvent(){
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.EndPhase);
        _endPhaseButton.onClick.RemoveAllListeners();
        _endPhaseButton.gameObject.SetActive(false);
    }

    public UICardPlaceHolder UICardPlaceHolder => _UICardPlaceHolder;
}