using System;
using UnityEngine;

[RequireComponent(typeof(CardVisual), typeof(CardMovement))]
public abstract class Card : MonoBehaviour {
    public static Action<Card> OnCardSelected;
    public static Action<Card> OnCardDeselected;

    public static Action<Texture2D> OnMouseOverCard;

    public CardSO Data; //For some reason, need to be public... makes no F* sense - It has 3 refencies. In ArcaneCard.cs, DamageCard.cs, MonsterCard.cs. None try to change the value, only here. And cannot be private with a public refence to it (Card => _card). Can't be serielized;. Needs to be public or otherwise it became null at the instatiation moment.

    public string Name {get; private set;}
    public Texture2D _illustration {get; private set;}
    public CardVisual CardVisual {get; private set;}
    protected CardMovement _cardMovement {get; private set;}
    private bool _isPlayerCard = false;
    private bool _canBeSelected = false;
    private bool _isOnHand = false;
    private bool _isSelected = false;
    public bool FusionedCard { get; private set; } = false;

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
        CardVisual = GetComponent<CardVisual>();
        _cardMovement = GetComponent<CardMovement>();
    }

    private void Start(){
        SetCardInfo();
        CardVisual.SetVisuals(_illustration);
        SetCardText();
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }
        if(_isPlayerCard && _isOnHand){
            Vector3 newPos;
            if(!_isSelected){
                newPos = new (0,+0.3f,0);
                CardVisual.Shader.SetBorderColor(new Color(191, 162, 57));
                _isSelected = true;
                OnCardSelected?.Invoke(this);
            }else{
                newPos = new (0,-0.3f,0);
                CardVisual.Shader.ResetBorderColor();
                _isSelected = false;
                OnCardDeselected?.Invoke(this);
            }

            transform.position += newPos;
        }
    }

    private void OnMouseOver(){
        //If is face Up or if face up
        if(_isPlayerCard){
            OnMouseOverCard?.Invoke(_illustration);
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

    public void SetFusionedCard(){
        FusionedCard = true;
    }

    public void MoveCard(Transform targetTransform){
        transform.SetParent(targetTransform);
        _cardMovement.AllowMovement(true);
        _cardMovement.SetTargetPosition(targetTransform.position, 5f);
        _cardMovement.SetTargetRotation(targetTransform.rotation);
    }

    public void EnableModelVisual(){
        CardVisual.Renderer.gameObject.SetActive(true);
    }

    public void DisableModelVisual(){
        CardVisual.Renderer.gameObject.SetActive(false);
    }

    public void DestroyCard(){
        Destroy(gameObject);
    }

#endregion

}