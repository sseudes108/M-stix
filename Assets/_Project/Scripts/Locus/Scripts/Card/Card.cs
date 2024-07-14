using System;
using UnityEngine;

// [RequireComponent(typeof(CardVisual), typeof(CardMovement))]
public abstract class Card : MonoBehaviour {
    [Header("Managers")]
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private CardManagerSO _cardManager;

    [Header("Global Settings")]
    public CardSO Data; //For some reason, need to be public... makes no F* sense - It has 3 refencies. In ArcaneCard.cs, DamageCard.cs, MonsterCard.cs. None try to change the value, only here. And cannot be private with a public refence to it (Card => _card). Can't be serielized;. Needs to be public or otherwise it became null at the instatiation moment.
    public string Name {get; private set;}
    private Texture2D _illustration;
    public CardVisual Visuals {get; private set;}
    protected CardMovement _cardMovement {get; private set;}
    public bool _isPlayerCard = false;
    public bool _canBeSelected = false;
    public bool _isOnHand = false;
    public bool _isSelected = false;
    public bool FusionedCard = false; // {get; private set;}
    public bool FaceSelected = false; // {get; private set;}
    public bool IsFaceDown = false; // {get; private set;}
    public bool CanFlip = false; // {get; private set;}
    
    // private Transform _model;
    private Transform _status;
    private Collider _collider;

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
        _cardMovement = GetComponent<CardMovement>();
        _status = transform.Find("Canvas");
        _collider = GetComponent<Collider>();
    }

    private void Start(){
        SetCardInfo();
        Visuals.SetVisuals(_illustration);
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }
        if(_isPlayerCard && _isOnHand){
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

    // private void OnMouseOver(){
    //     //If is face Up or if face up
    //     if(_isPlayerCard){
    //         OnMouseOverCard?.Invoke(_illustration);
    //     }
    // }

#endregion

#region Events Methods

    private void BattleManager_OnCardSelectionStart() { _canBeSelected = true; }
    private void BattleManager_OnCardSelectionEnd() { _canBeSelected = false; }

#endregion

#region Custom Methods Methods

    public void SetCardData(ScriptableObject cardData) { Data = cardData as CardSO; }
    public virtual void SetCardInfo() { _illustration = Data.Illustration; }
    public virtual void SetCardText() { Name = Data.Name; }
    public void IsPlayerCard() { _isPlayerCard = true; }
    public void SetCardOnHand(bool isOnHand) { _isOnHand = isOnHand; }
    public void SetFusionedCard() { FusionedCard = true; }
    public void DeselectCard() { _isSelected = false; }
    public void SelectFace() { FaceSelected = true; }
    public void SetFaceDown() { IsFaceDown = true; }
    public void SetCanFlip() { CanFlip = true; }

    public void MoveCard(Vector3 position){
        _cardMovement.SetTargetPosition(position, 5f);
        _cardMovement.SetTargetRotation(Quaternion.identity);
    }

    public void MoveCard(Transform targetTransform){
        transform.SetParent(targetTransform);
        _cardMovement.AllowMovement(true);
        _cardMovement.SetTargetPosition(targetTransform.position, 5f);
        _cardMovement.SetTargetRotation(targetTransform.rotation);
    }

    public void MoveCard(Transform targetTransform, Quaternion rotation){
        transform.SetParent(targetTransform);
        _cardMovement.AllowMovement(true);
        _cardMovement.SetTargetPosition(targetTransform.position, 5f);
        _cardMovement.SetTargetRotation(rotation);
    }
    
    public void DestroyCard() { Destroy(gameObject); }
    public void EnableStatCanvas() { _status.gameObject.SetActive(true); }
    public void DisableStatCanvas() { _status.gameObject.SetActive(false); }
    public void DisableCollider() { _collider.enabled = false; }

#endregion
}