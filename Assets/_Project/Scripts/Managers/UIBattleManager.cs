using TMPro;
using UnityEngine;

public class UIBattleManager : MonoBehaviour {
    [SerializeField] private UICardPlaceHolder _UICardPlaceHolder;
    [SerializeField] private TextMeshProUGUI _turnNumber, _turnOwner;
    [SerializeField] private TextMeshProUGUI _state;
    [SerializeField] private TextMeshProUGUI _player, _enemy;
    
    private void Awake() {
        _UICardPlaceHolder = GetComponentInChildren<UICardPlaceHolder>();
    }

    private void Update() {
        UpdateStateMachineState();
    }

    private void UpdateStateMachineState(){
        _state.text = $"{BattleManager.Instance.BattleStateManager.GetCurrentBattlePhase()}";
    }

    public UICardPlaceHolder UICardPlaceHolder => _UICardPlaceHolder;

    public void UpdateHealth(int playerHP, int enemyHP){
        _player.text = @$"Player LP : {playerHP} Deck: {BattleManager.Instance.PlayerHand.GetDeck().DeckInUse.Count}";
        _enemy.text = @$"Enemy LP : {enemyHP} Deck: {BattleManager.Instance.EnemyHand.GetDeck().DeckInUse.Count}";
    }
    
    public void UpdateDeckCount(){
        _player.text = @$"Player LP : {BattleManager.Instance.HealthManager.GetPlayerHP()} Deck: {BattleManager.Instance.PlayerHand.GetDeck().DeckInUse.Count}";
        _enemy.text = @$"Enemy LP : {BattleManager.Instance.HealthManager.GetEnemyHP()} Deck: {BattleManager.Instance.EnemyHand.GetDeck().DeckInUse.Count}";
    }

    public void UpdateTurn(int turn, bool isPlayerTurn){
        _turnNumber.text = $"Turn: {turn}";

        if(isPlayerTurn){
            _turnOwner.text = "Player Turn";
        }else{
            _turnOwner.text = "Enemy Turn";
        }
    }
}