using System.Collections;
using System.Collections.Generic;
using Mistix.FusionLogic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Mistix{
    public abstract class Hand : MonoBehaviour{
        [SerializeField] private List<HandPosition> _handPlaces;
        [SerializeField] protected List<Transform> _freePositionsInHand;
        protected Deck _deck;
        private bool _canMove;
        Vector3 _targetPosition;

        private void Awake() {
            _deck = GetComponentInParent<Deck>();
            _freePositionsInHand = new();
        }

        private void Start() {
            VerifyPositionsInHand();
            StartDrawCardRoutine();
        }

        private void Update() {
            if(_canMove){
                Move();
            }
        }

        //PUBLIC WHILE TESTING
        public void StartDrawCardRoutine(){
            StartCoroutine(DrawCardRoutine());
        }

        protected abstract IEnumerator DrawCardRoutine();

        protected void SetCardInHand(Card cardDrew){
            cardDrew.MoveCard(_freePositionsInHand[0].position, _freePositionsInHand[0].rotation);

            cardDrew.transform.SetParent(_freePositionsInHand[0]);
            cardDrew.name = $"{cardDrew.GetCardInfo()}";
            
            SetPositionInHandOcupied(cardDrew);
            VerifyPositionsInHand();
        }

        private void SetPositionInHandFree(Card cardToRemove){
            cardToRemove.GetComponentInParent<HandPosition>().FreePlace();
        }
        
        private void SetPositionInHandOcupied(Card cardToRemove){
            cardToRemove.GetComponentInParent<HandPosition>().OcupyPlace();
        }

        private void VerifyPositionsInHand(){
            _freePositionsInHand.Clear();
            foreach(var position in _handPlaces){
                if(position.IsFree()){
                    var positionTransform = position.gameObject.transform;
                    _freePositionsInHand.Add(positionTransform);
                }
            }
        }

        //Move Hand Position
        public void MoveHand(Vector3 targetPosition){
            _canMove = true;
            _targetPosition = targetPosition;
        }

        private void Move(){
            float moveSpeed = 5.0f;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, _targetPosition) < 0.02f){
                _canMove = false;
            }
        }
    }
}
