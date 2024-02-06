using System;
using Mistix.Enums;
using UnityEngine;

namespace Mistix{
    public class Card: MonoBehaviour{
        private readonly ECardType _cardType;
        private readonly string _cardInfo;

        public static Action<Card> OnCardSelected, OnCardDeselected;

        //Move
        private bool _canMove;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        //Select
        private bool _selected = false;
        private Vector3 _notSelectedPosition;

        private void OnEnable() {
            OnCardSelected += OnCardSelected;
            OnCardDeselected += OnCardDeselected;
        }

        private void OnDisable() {
            OnCardSelected -= SelectCard;
            OnCardDeselected -= DeselectCard;
        }

        private void Update() {
            if(_canMove){
                Move();
            }
        }

        private void Move(){
            float moveSpeed = 5.0f;
            float rotationSpeed = 300.0f;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, _targetPosition) < 0.01f){
                _canMove = false;
            }
        }

        public virtual void SetUpCardData(ScriptableObject CardData){}

        public virtual ECardType GetCardType(){return _cardType;}

        public virtual string GetCardInfo(){return _cardInfo;}

        public void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
            _canMove = true;
            _targetPosition = targetPosition;
            _targetRotation = targetRotation;
        }

        protected virtual void OnMouseDown(){
            if(!_selected){
                OnCardSelected?.Invoke(this);
            }else{
                OnCardDeselected?.Invoke(this);
            }

            GetCardInfo();
        }

        private void SelectCard(Card sender){
            _notSelectedPosition = transform.position;
            transform.position += new Vector3(0, 0.5f, 0.5f);
            _selected = true;
            CardSelector.Instance.AddCardToSelectedList(sender);
        }

        private void DeselectCard(Card sender){
            transform.position = _notSelectedPosition;
            _selected = false;
            CardSelector.Instance.RemoveCardFromSelectedList(sender);
        }
    }
}