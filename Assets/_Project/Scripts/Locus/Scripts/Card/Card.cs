using System;
using UnityEngine;

[RequireComponent(typeof(CardVisual), typeof(CardMovement))]
public abstract class Card : MonoBehaviour {
    public static Action<Card> OnCardSelected;
    public static Action<Card> OnCardDeselected;

    public CardSO Data;
    public Texture2D _illustration;
    protected CardVisual _cardVisual;
    protected CardMovement _cardMovement;
    private bool _isPlayerCard = false;
    private bool _canBeSelected = false;
    private bool _isOnHand = false;
    private bool _isSelected = false;

#region Unity Methods

    private void OnEnable() {
        CardSelectionPhase.OnCardSelectionStart += CardSelectionPhase_OnCardSelectionStart;
        CardSelectionPhase.OnCardSelectionEnd += CardSelectionPhase_OnCardSelectionEnd;
    }

    private void OnDisable() {
        CardSelectionPhase.OnCardSelectionStart -= CardSelectionPhase_OnCardSelectionStart;
        CardSelectionPhase.OnCardSelectionEnd -= CardSelectionPhase_OnCardSelectionEnd;
    }
    
    private void Awake() {
        _cardVisual = GetComponent<CardVisual>();
        _cardMovement = GetComponent<CardMovement>();
    }

    private void Start(){
        SetCardInfo();
        _cardVisual.SetVisuals(_illustration);
        SetCardText();
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }
        if(_isPlayerCard && _isOnHand){
            Vector3 newPos;
            
            if(!_isSelected){
                newPos = new (0,+0.3f,0);
                _cardVisual.Shader.SetBoarderColor(new Color(191, 162, 57));
                _isSelected = true;
                OnCardSelected?.Invoke(this);

            }else{

                newPos = new (0,-0.3f,0);
                _cardVisual.Shader.ResetBoarderColor();
                _isSelected = false;
                OnCardDeselected?.Invoke(this);
            }

            transform.position += newPos;
        }
    }

#endregion

#region Events Methods

    private void CardSelectionPhase_OnCardSelectionStart(){
        _canBeSelected = true;
    }

    private void CardSelectionPhase_OnCardSelectionEnd(){
        _canBeSelected = false;
    }

#endregion

#region Custom Methods Methods

    public void SetCardData(ScriptableObject cardData){
        Data = cardData as CardSO;
    }

    public virtual void SetCardInfo(){
        _illustration = Data.Illustration;
    }
    
    public virtual void SetCardText(){}

    public void IsPlayeCard(){
        _isPlayerCard = true;
    }

    public void SetCardOnHand(bool isOnHand){
        _isOnHand = isOnHand;
    }

    public void MoveCard(Transform targetTransform){
        transform.SetParent(targetTransform);
        _cardMovement.AllowMovement(true);
        _cardMovement.SetTargetPosition(targetTransform.position, 5f);
        _cardMovement.SetTargetRotation(targetTransform.rotation);
    }

#endregion

}