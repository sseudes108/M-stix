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
        UpdateTurnLabels();
    }

    private void UpdateTurnLabels(){
        _turnNumber.text = $"Turn: {BattleManager.Instance.TurnManager.GetTurn()}";
        
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            _turnOwner.text = "Player Turn";
        }else{
            _turnOwner.text = "Enemy Turn";
        }
        _state.text = $"{BattleManager.Instance.BattleStateManager.GetCurrentBattlePhase()}";

        _player.text = @$"Player LP : 8000 Deck: {BattleManager.Instance.PlayerHand.GetDeck().DeckInUse.Count}";
        _enemy.text = @$"Enemy LP : 8000 Deck: {BattleManager.Instance.EnemyHand.GetDeck().DeckInUse.Count}";
    }

    public UICardPlaceHolder UICardPlaceHolder => _UICardPlaceHolder;

    
}