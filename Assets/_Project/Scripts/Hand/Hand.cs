using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public abstract class Hand : MonoBehaviour{
        [SerializeField] private List<HandPosition> _handPlaces;
        [SerializeField] protected List<Transform> _freePositionsInHand;
        protected Deck _deck;

        private void Awake() {
            _deck = GetComponentInParent<Deck>();
            _freePositionsInHand = new();
        }
        private void Start() {
            VerifyPositionsInHand();
            StartDrawCardRoutine();
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
    }
}
