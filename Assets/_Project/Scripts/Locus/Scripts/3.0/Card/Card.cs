using System;
using UnityEngine;

namespace Mistix{
    public abstract class Card : MonoBehaviour {
        protected CardManager _cardManager;

        [Header("Global Settings")]
        public CardSO Data; //For some reason, need to be public... makes no F* sense - It has 3 refencies. In ArcaneCard.cs, DamageCard.cs, MonsterCard.cs. None try to change the value, only here. And cannot be private with a public refence to it (Card => _card). Can't be serialized;. Needs to be public or otherwise it became null at the instatiation moment.
        public string Name { get; private set; }
        private Texture2D _illustration;
        protected CardVisual _visuals;
        protected CardMovement _cardMovement;
        public bool IsPlayerCard = false;
        private bool _canBeSelected = false;
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
        public BoardPlace BoardPlace;

    #region Unity Methods        
        private void Awake() {
            _cardManager = FindFirstObjectByType<CardManager>();
            
            _visuals = GetComponent<CardVisual>();
            _cardMovement = GetComponent<CardMovement>();
            _status = transform.Find("Canvas");
            _collider = GetComponent<Collider>();
        }

        private void Start(){
            SetCardInfo();
            _visuals.SetVisuals(_illustration);
        }

        private void OnMouseDown() {
            if(!_canBeSelected) { return; }

            if(IsPlayerCard && IsOnHand){
                Vector3 newPos;
                if(!_isSelected){
                    newPos = Selected();
                }else{
                    newPos = Deselected();
                }

                transform.position += newPos;
            }
        }

        public void OnMouseOver(){
            if(!IsPlayerCard && IsOnHand) {return;} //Not player card, on hand

            if(IsPlayerCard){
                _cardManager.UpdateCardUilustration(Data.Illustration);
                return;
            }

            if(!IsOnHand && !IsFaceDown){ //Not player card, not on hand, not face down
                _cardManager.UpdateCardUilustration(Data.Illustration);
                return;
            }
        }

    #endregion

        public void SetCardData(ScriptableObject cardData) { Data = cardData as CardSO; }
        public virtual void SetCardInfo() { _illustration = Data.Illustration; }
        public virtual void SetCardText() { Name = Data.Name; }
        public void SetPlayerCard() { IsPlayerCard = true; }
        public void SetCardOnHand(bool isOnHand) { IsOnHand = isOnHand; }
        public void SetCanFlip(bool canFlip) { CanFlip = canFlip; }
        public void SetWasFlipedThisTurn(bool flipedThisTurn) { WasFlipedThisTurn = flipedThisTurn; }

    #region Visuals
        public void ResetBorderColor(){
            _visuals.Border.ResetBorderColor();
        }
        public void HighBorderColor(){
            _visuals.Border.HighBorderColor();
        }
        public void DissolveCard(Color color){
            _visuals.Dissolve.DissolveCard(color);
        }
        public void DisableRenderer(){
            _visuals.DisableRenderer();
        }
        public void EnableRenderer(){
            _visuals.EnableRenderer();
        }
        public void MakeCardInvisible(){
            _visuals.Dissolve.MakeCardInvisible();
        }
        public void SolidifyCard(Color color){
            _visuals.Dissolve.SolidifyCard(color);
        }

    #endregion
        
    #region Buttons
        public void SetShowButtons(bool mustShowButtons) { MustShowButtons = mustShowButtons; }
    #endregion
    
    #region Fusion
        public void SetFusionedCard() { FusionedCard = true; }
        public void SetCardAsFusioned() { FusionedCard = true; }
    #endregion

    #region Stats
        public virtual void ResetCardStats() { FaceSelected = false; }

        public void EnableStatCanvas() { _status.gameObject.SetActive(true); }
        public void DisableStatCanvas() { _status.gameObject.SetActive(false); }

        public void SelectFace() { FaceSelected = true; }
        public void SetFaceDown() { IsFaceDown = true; }
        public void SetFaceUp() { IsFaceDown = false; }

    #endregion
    
    #region Selection
        public void AllowCardSelection(){ _canBeSelected = true; }
        public void BlockCardSelection(){ _canBeSelected = false; }
        // public void DeselectCard() { _isSelected = false; }

        private Vector3 Selected(){
            Vector3 newPos = new(0, +0.3f, 0);
            HighBorderColor();
            _isSelected = true;
            _cardManager.SelectCard(this);
            return newPos;
        }

        private Vector3 Deselected(){
            Vector3 newPos = new(0, -0.3f, 0);
            ResetBorderColor();
            _isSelected = false;
            _cardManager.DeselectCard(this);
            return newPos;
        }
    #endregion

    #region Movement
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

        public void MoveCard(Transform targetTransform, float speed){
            transform.SetParent(targetTransform);
            _cardMovement.AllowMovement(true);
            _cardMovement.SetTargetPosition(targetTransform.position, speed);
            _cardMovement.SetTargetRotation(targetTransform.rotation);
        }

        public void MoveCard(Transform targetTransform, Quaternion rotation){
            transform.SetParent(targetTransform);
            _cardMovement.AllowMovement(true);
            _cardMovement.SetTargetPosition(targetTransform.position, 5f);
            _cardMovement.SetTargetRotation(rotation);
        }

    #endregion
        
        public void DestroyCard() { Destroy(gameObject); }

        public void DisableCollider() { _collider.enabled = false; }

        // public void SetHandPosition(HandPosition handPosition) { _handPosition = handPosition; }

        public void SetHandPositionFree() {
            if(_handPosition == null) { return; }
            _handPosition.SetPlaceFree();
        }

        public void SetBoardPlace(BoardPlace boardPlace) { 
            BoardPlace = boardPlace;
            SetShowButtons(true);
        }

        // public BoardPlace GetBoardPlace() { return BoardPlace; }

        // public void HighLightBoardPlace(){
        //     if( BoardPlace == null ) return;
        //     BoardPlace.HighLight();
        // }
    }
}