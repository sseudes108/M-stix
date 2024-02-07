using System;
using Mistix.Enums;
using UnityEngine;

namespace Mistix{
    public class Card: MonoBehaviour{
        private readonly ECardType _cardType;
        private readonly string _cardInfo;

        //Move
        private bool _canMove;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        //Select
        private bool _selected = false;
        private Vector3 _notSelectedPosition;

        protected bool _isPlayerCard;

        private void Start() {
            var handPosition = GetComponentInParent<HandPosition>();
            var handOwner = handPosition.GetComponentInParent<Hand>();
            if(handOwner is PlayerHand){
                _isPlayerCard = true;
            }else{
                _isPlayerCard = false;
            }
        }

        private void Update() {
            if(_canMove){
                GetComponent<Collider>().enabled = false;
                Move();
            }
        }

        private void Move(){
            float moveSpeed = 5.0f;
            float rotationSpeed = 300.0f;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, _targetPosition) < 0.02f){

                //** Dont enable collider if the card is in the fusion line **//
                var cardInFusionLine = GetComponentInParent<FusionPosition>();
                if(cardInFusionLine == null){
                    GetComponent<Collider>().enabled = true;
                }

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
                if(TurnSystem.Instance.IsPlayerTurn() && _isPlayerCard){
                    SelectCard();
                }
            }else{
                DeselectCard();
            }
            GetCardInfo();
        }

        private void SelectCard(){
            transform.position += new Vector3(0, 0.3f, 0.3f);
            _selected = true;

            if(TurnSystem.Instance.IsPlayerTurn() && _isPlayerCard){
                CardSelector.Instance.AddCardToPlayerSelectedList(this);
            }

            //Enemy Turn and Enemy Card
            if(!TurnSystem.Instance.IsPlayerTurn() && !_isPlayerCard){
                CardSelector.Instance.AddCardToEnemySelectedList(this);
            }
        }

        private void DeselectCard(){
            transform.position += new Vector3(0, -0.3f, -0.3f);
            _selected = false;

            if(TurnSystem.Instance.IsPlayerTurn() && _isPlayerCard){
                CardSelector.Instance.RemoveCardFromPlayerSelectedList(this);
            }

            //Enemy Turn and Enemy Card
            if(!TurnSystem.Instance.IsPlayerTurn() && !_isPlayerCard){
                CardSelector.Instance.AddCardToEnemySelectedList(this);
            }
        }
    }
}