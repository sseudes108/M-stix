using UnityEngine;

public abstract class Card : MonoBehaviour {
    [Header("Managers")]
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private CardManagerSO _cardManager;
    [SerializeField] private UIEventHandlerSO _uIManager;

    [Header("Global Settings")]
    public CardSO Data; //For some reason, need to be public... makes no F* sense - It has 3 refencies. In ArcaneCard.cs, DamageCard.cs, MonsterCard.cs. None try to change the value, only here. And cannot be private with a public refence to it (Card => _card). Can't be serialized;. Needs to be public or otherwise it became null at the instatiation moment.
    public string Name { get; private set; }
    private Texture2D _illustration;
    public CardVisual Visuals { get; private set; }
    protected CardMovement CardMovement { get; private set; }
    public bool IsPlayerCard = false;
    public bool _canBeSelected = false;
    public bool IsOnHand = false;
    public bool _isSelected = false;

    public bool FusionedCard { get; private set; } = false;
    public bool FaceSelected { get; private set; } = false;
    public bool IsFaceDown { get; private set; } = false;
    public bool CanFlip { get; private set; } = false;
    public bool WasFlipedThisTurn { get; private set; } = false;
    public bool MustShowButtons { get; private set; } = false;
    
    private Transform _status;
    private Collider _collider;

    private HandPosition _handPosition;
    private BoardPlace _boardPlace;

#region Unity Methods

    private void OnEnable() {
        _battleManager.OnCardSelectionStart.AddListener(BattleManager_OnCardSelectionStart);
        _battleManager.OnCardSelectionEnd.AddListener(BattleManager_OnCardSelectionEnd);
    }

    private void OnDisable() {
        _battleManager.OnCardSelectionStart.RemoveListener(BattleManager_OnCardSelectionStart);
        _battleManager.OnCardSelectionEnd.RemoveListener(BattleManager_OnCardSelectionEnd);
    }
    
    private void Awake() {
        Visuals = GetComponent<CardVisual>();
        CardMovement = GetComponent<CardMovement>();
        _status = transform.Find("Canvas");
        _collider = GetComponent<Collider>();
    }

    private void Start(){
        SetCardInfo();
        Visuals.SetVisuals(_illustration);
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }

        if(IsPlayerCard && IsOnHand){
            Vector3 newPos;
            if(!_isSelected){
                newPos = new (0,+0.3f,0);
                Visuals.Border.SetBorderColor(new Color(191, 162, 57));
                _isSelected = true;
                _cardManager.Selector.AddToSelectedList(this);
            }else{
                newPos = new (0,-0.3f,0);
                Visuals.Border.ResetBorderColor();
                _isSelected = false;
                _cardManager.Selector.RemoveFromSelectedList(this);
            }

            transform.position += newPos;
        }
    }

    public void OnMouseOver(){
        if(!IsPlayerCard && IsOnHand) {return;} //Not player card, on hand

        if(IsPlayerCard){
            _uIManager.UpdateIllustration(Data.Illustration);
            return;
        }

        if(!IsOnHand && !IsFaceDown){ //Not player card, not on hand, not face down
            _uIManager.UpdateIllustration(Data.Illustration);
            return;
        }
    }

#endregion

#region Events Methods
    private void BattleManager_OnCardSelectionStart() { _canBeSelected = true; }
    private void BattleManager_OnCardSelectionEnd() { _canBeSelected = false; }

#endregion

#region Custom Methods Methods

    public void SetCardData(ScriptableObject cardData) { Data = cardData as CardSO; }
    public virtual void SetCardInfo() { _illustration = Data.Illustration; }
    public virtual void SetCardText() { Name = Data.Name; }
    public void SetPlayerCard() { IsPlayerCard = true; }
    public void SetCardOnHand(bool isOnHand) { IsOnHand = isOnHand; }
    public void SetFusionedCard() { FusionedCard = true; }
    public void DeselectCard() { _isSelected = false; }
    public void SelectFace() { FaceSelected = true; }
    public void SetFaceDown() { IsFaceDown = true; }
    public void SetFaceUp() { IsFaceDown = false; }
    public void SetCanFlip(bool canFlip) { CanFlip = canFlip; }
    public void SetWasFlipedThisTurn(bool flipedThisTurn) { WasFlipedThisTurn = flipedThisTurn; }
    public void SetShowButtons(bool mustShowButtons) { MustShowButtons = mustShowButtons; }
    
    public void MoveCard(Vector3 position){
        CardMovement.SetTargetPosition(position, 5f);
        CardMovement.SetTargetRotation(Quaternion.identity);
    }

    public void MoveCard(Transform targetTransform){
        transform.SetParent(targetTransform);
        CardMovement.AllowMovement(true);
        CardMovement.SetTargetPosition(targetTransform.position, 5f);
        CardMovement.SetTargetRotation(targetTransform.rotation);
    }

    public void MoveCard(Transform targetTransform, float speed){
        transform.SetParent(targetTransform);
        CardMovement.AllowMovement(true);
        CardMovement.SetTargetPosition(targetTransform.position, speed);
        CardMovement.SetTargetRotation(targetTransform.rotation);
    }

    public void MoveCard(Transform targetTransform, Quaternion rotation){
        transform.SetParent(targetTransform);
        CardMovement.AllowMovement(true);
        CardMovement.SetTargetPosition(targetTransform.position, 5f);
        CardMovement.SetTargetRotation(rotation);
    }
    
    public void DestroyCard() { Destroy(gameObject); }
    public void EnableStatCanvas() { _status.gameObject.SetActive(true); }
    public void DisableStatCanvas() { _status.gameObject.SetActive(false); }
    public void DisableCollider() { _collider.enabled = false; }

    public void SetHandPosition(HandPosition handPosition) { _handPosition = handPosition; }

    public void SetHandPositionFree() {
        if(_handPosition == null) { return; }
        _handPosition.SetPlaceFree();
    }

    public void SetCardAsFusioned() { FusionedCard = true; }
    public virtual void ResetCardStats() { FaceSelected = false; }

    public void SetBoardPlace(BoardPlace boardPlace) { _boardPlace = boardPlace; }
    public BoardPlace GetBoardPlace() { return _boardPlace; }
    public void HighLightBoardPlace(){
        if( _boardPlace == null ) return;
        _boardPlace.HighLight();
    }

    #endregion
}