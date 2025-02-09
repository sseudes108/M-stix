using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class Hand : MonoBehaviour {
        [SerializeField] private List<Transform> _handPositions;
        [SerializeField] private List<Transform> _freePositionsInHand;
        [SerializeField] private List<Card> _cardsInHand;
        [SerializeField] private Deck _deck;
        [SerializeField] private HandManager _handManager;
        [SerializeField] private bool _isPlayerHand;

        // public void CheckPositionsInHand() { CheckPositions(); }

        public void CheckPositionsInHand(){
            _cardsInHand.Clear();
            _freePositionsInHand.Clear();
        
            foreach(var position in _handPositions){
                var handPosition = position.GetComponent<HandPosition>();
                if(handPosition.IsFree){
                    _freePositionsInHand.Add(position);
                }else{
                    _cardsInHand.Add(handPosition.CardInPosition);
                }
            }
        }

        public void Draw() { StartCoroutine(DrawCardsRoutine()); }

        private IEnumerator DrawCardsRoutine(){
            foreach(var position in _freePositionsInHand){
                //Random card data
                var randomIndex = Random.Range(0, _deck.GetDeckCount());
                var cardData = _deck.GetDeck()[randomIndex];

                //Instantiate
                var drewCard = Instantiate(_handManager.InstantiateCard(cardData), _deck.transform.position, _deck.transform.rotation);

                //Remove from Deck
                _deck.RemoveCardFromDeck(cardData);

                //Card Owner
                if(_deck.IsPlayerDeck()){
                    drewCard.SetPlayerCard();
                }

                //Move to position
                drewCard.MoveCard(position);

                //Occupy place position
                _cardsInHand.Add(drewCard);
                drewCard.SetCardOnHand(true);
                position.GetComponent<HandPosition>().OccupyPlace(drewCard);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        public bool IsPlayerHand() { return _isPlayerHand; }
    }
}