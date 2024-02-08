using System;
using Mistix.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace Mistix{
    public class Card: MonoBehaviour{
        protected ECardType _cardType;
        private readonly string _cardInfo;

        //Move
        private bool _canMove;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        //Select
        private bool _selected = false;

        protected bool _isPlayerCard;

        private void Start() {
            TryGetComponent<Hand>(out Hand handOwner);

            if(handOwner != null){
                if(handOwner is PlayerHand){
                    _isPlayerCard = true;
                }else{
                    _isPlayerCard = false;
                }
            }else{
                if(BattleManager.Instance.TurnSystem.IsPlayerTurn()){
                    _isPlayerCard = true;
                }else{
                    _isPlayerCard = false;
                }
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

        public ECardType GetCardType(){
            var isMonster = GetComponent<MonsterCard>();

            if(isMonster != null){
                _cardType = ECardType.Monster;
            }else{
                _cardType = ECardType.Arcane;
            }

            return _cardType;
        }

        public virtual string GetCardInfo(){return _cardInfo;}

        public void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
            _canMove = true;
            _targetPosition = targetPosition;
            _targetRotation = targetRotation;
        }

        protected virtual void OnMouseDown(){
            if(!_selected){
                if(BattleManager.Instance.TurnSystem.IsPlayerTurn() && _isPlayerCard){
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

            if(BattleManager.Instance.TurnSystem.IsPlayerTurn() && _isPlayerCard){
                BattleManager.Instance.CardSelector.AddCardToPlayerSelectedList(this);
            }

            //Enemy Turn and Enemy Card
            if(!BattleManager.Instance.TurnSystem.IsPlayerTurn() && !_isPlayerCard){
                BattleManager.Instance.CardSelector.AddCardToEnemySelectedList(this);
            }
        }

        private void DeselectCard(){
            transform.position += new Vector3(0, -0.3f, -0.3f);
            _selected = false;

            if(BattleManager.Instance.TurnSystem.IsPlayerTurn() && _isPlayerCard){
                BattleManager.Instance.CardSelector.RemoveCardFromPlayerSelectedList(this);
            }

            //Enemy Turn and Enemy Card
            if(!BattleManager.Instance.TurnSystem.IsPlayerTurn() && !_isPlayerCard){
                BattleManager.Instance.CardSelector.AddCardToEnemySelectedList(this);
            }
        }
    }
}