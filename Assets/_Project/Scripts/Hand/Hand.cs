using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class Hand : MonoBehaviour{
        [SerializeField] List<HandPosition> _handPlaces;
        [SerializeField] private List<Transform> _freePositionsInHand;
        private Deck _deck;

        private void Awake() {
            _deck = GetComponentInParent<Deck>();
            _freePositionsInHand = new();
        }

        private void Start() {
            VerifyPositionsInHand();
        }

        //PUBLIC WHILE TESTING
        public void StartDrawCardRoutine(){
            StartCoroutine(DrawCardRoutine());
        }

        private IEnumerator DrawCardRoutine(){
            do{
                var randomIndex = Random.Range(0, _deck.GetDeckInUse().Count);

                SetCardInHand(CardCreator.Instance.CreateCard(_deck.GetDeckInUse()[randomIndex]));
                yield return new WaitForSeconds(0.5f);

            }while(_freePositionsInHand.Count > 0);
        }

        private void SetCardInHand(Card cardDrew){
            cardDrew.MoveCard(_freePositionsInHand[0].position, _freePositionsInHand[0].rotation);
            cardDrew.transform.SetParent(_freePositionsInHand[0]);
            cardDrew.name = $"{cardDrew.GetCardInfo()}";
            
            _freePositionsInHand[0].GetComponent<HandPosition>().OcupyPlace();
            VerifyPositionsInHand();
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
