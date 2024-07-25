using UnityEngine;

public class UICard : MonoBehaviour {
    [SerializeField] private UIEventHandlerSO _uIManager;
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private TurnManagerSO _turnManager;
    private Renderer _renderer;
    private CardMovement _cardMovement;
    private Vector3 _startPosition;
    [SerializeField] private Transform _offScreenPosition;
    private float _moveSpeed = 5f;

    private void OnEnable() {
        _uIManager.OnUpdateIllustration.AddListener(UIManager_OnUpdateIllustration);
        _battleManager.OnCardSelectionEnd.AddListener(BattleManager_OnCardSelectionEnd);
        _battleManager.OnBoardPlaceSelectionEnd.AddListener(BattleManager_OnBoardPlaceSelectionEnd);
    }

    private void OnDisable() {
        _uIManager.OnUpdateIllustration.RemoveListener(UIManager_OnUpdateIllustration);
        _battleManager.OnCardSelectionEnd.RemoveListener(BattleManager_OnCardSelectionEnd);
        _battleManager.OnBoardPlaceSelectionEnd.RemoveListener(BattleManager_OnBoardPlaceSelectionEnd);
    }

    private void Awake() {
        _renderer = transform.Find("Card").GetComponentInChildren<Renderer>();
        _cardMovement = transform.Find("Card").GetComponentInChildren<CardMovement>();
    }

    private void Start(){
        _startPosition = _renderer.transform.position;
    }

    private void UIManager_OnUpdateIllustration(Texture2D newIllustration){
        UpdateIllustration(newIllustration);
    }

    private void BattleManager_OnCardSelectionEnd(){
        if(!_turnManager.IsPlayerTurn) { return; }

        _cardMovement.AllowMovement(true);
        _cardMovement.SetTargetPosition(_offScreenPosition.position, _moveSpeed);
    }

    private void BattleManager_OnBoardPlaceSelectionEnd(Card arg0, bool arg1){ //Args not used
        if(!_turnManager.IsPlayerTurn) { return; }
        
        _cardMovement.AllowMovement(true);
        _cardMovement.SetTargetPosition(_startPosition, _moveSpeed);
    }

    public void UpdateIllustration(Texture2D illustration){
        var faceMat = new Material(_renderer.sharedMaterials[1]);
        faceMat.SetTexture("_Ilustration", illustration);

        _renderer.materials = new[] { _renderer.sharedMaterials[0], faceMat, _renderer.sharedMaterials[2] };
    }
}