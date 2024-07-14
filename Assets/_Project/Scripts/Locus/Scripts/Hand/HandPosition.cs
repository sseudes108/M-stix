using UnityEngine;

public class HandPosition : MonoBehaviour {
    public bool IsFree {get; private set;} = true;
    public Card CardInPosition {get; private set;}
    
    public void SetPlaceFree(){
        IsFree = true;
        CardInPosition = null;
    }

    public void OccupyPlace(Card card){
        IsFree = false;
        SetCardInPosition(card);
    }

    private void SetCardInPosition(Card card){
        CardInPosition = card;
    }
}