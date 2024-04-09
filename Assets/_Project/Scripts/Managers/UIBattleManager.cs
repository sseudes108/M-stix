using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleManager : MonoBehaviour {
    [SerializeField] private UICardPlaceHolder _UICardPlaceHolder;
    private Vector3 _UICardOriginalPosition;
    [SerializeField] private Transform _offScenePlaceHolderPosition;
    [SerializeField] private TextMeshProUGUI _turnNumber, _turnOwner;
    [SerializeField] private TextMeshProUGUI _state;
    [SerializeField] private GameObject _canvas;

    [Header("Player")]
    [SerializeField] private TextMeshProUGUI _playerHP;
    [SerializeField] private TextMeshProUGUI _playerDeck;

    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI _enemyHP;
    [SerializeField] private TextMeshProUGUI _enemyDeck;

    [SerializeField] private Button _endPhaseButton;
    [SerializeField] private Button _endSelectionButton;

    [Header("Battle Damage")]
    [SerializeField] private TextMeshProUGUI _p1Damage;
    [SerializeField] private TextMeshProUGUI _p2Damage;

    private void OnEnable() {
        BattleManager.Instance.TurnManager.OnTurnEnd += UpdateTurn;
    }

    private void OnDisable() {
        BattleManager.Instance.TurnManager.OnTurnEnd -= UpdateTurn;
    }

    private void Start() {
        _UICardOriginalPosition = _UICardPlaceHolder.transform.position;
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

    public void EndSelectionButton(){
        if(BattleManager.Instance.BattleStateManager.CurrentPhase == BattleManager.Instance.CardSelectionPhase){
            _endSelectionButton.gameObject.SetActive(true);
            _endSelectionButton.onClick.AddListener(TriggerEndSelectionEvent);
        }
    }
    private void TriggerEndSelectionEvent(){
        if(BattleManager.Instance.BattleStateManager.CurrentPhase == BattleManager.Instance.CardSelectionPhase &&
            BattleManager.Instance.CardSelector.GetSelectedCards().Count > 0){
            BattleManager.Instance.CardSelectionPhase.EndSelection();
            _endSelectionButton.onClick.RemoveAllListeners();
            DisableEndSelectionButton();
        }
    }
    public void DisableEndSelectionButton(){
        _endSelectionButton.gameObject.SetActive(false);
    }

    public void ClearUI(){
        _canvas.SetActive(false);
        _UICardPlaceHolder.Movement.SetTargetPosition(_offScenePlaceHolderPosition.position, 5f);
    }
    public void BringUI(){
        _canvas.SetActive(true);
        _UICardPlaceHolder.Movement.SetTargetPosition(_UICardOriginalPosition, 5f);
    }

    //Damage//
    public void StartDamageUIRoutine(int damage, bool playerDamage){
        if(playerDamage){
            StartCoroutine(DamageUIRoutine(_p1Damage, damage));
        }else{
            StartCoroutine(DamageUIRoutine(_p2Damage, damage));
        }
    }
    private IEnumerator DamageUIRoutine(TextMeshProUGUI damageText, int damage){
        yield return new WaitForSeconds(0.5f);
        
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true);
        damageText.alpha = 1;

        do{
            damageText.alpha -= 0.06f;
            yield return new WaitForSeconds(0.05f);
        }while(damageText.alpha > 0);

        damageText.gameObject.SetActive(false);
    }

    public UICardPlaceHolder UICardPlaceHolder => _UICardPlaceHolder;
}