using UnityEngine;

namespace Mistix{
    public class HandPosition : MonoBehaviour {
        public bool IsFree = true;  
        public Card CardInPosition {get; private set;}
        
        public void SetPlaceFree(){
            IsFree = true;
            CardInPosition = null;
        }

        public void OccupyPlace(Card card){
            IsFree = false;
            SetCardInPosition(card);
            // card.SetHandPosition(this);
        }

        private void SetCardInPosition(Card card){
            CardInPosition = card;
        }
    }
}