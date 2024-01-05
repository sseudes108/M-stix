using UnityEngine;

public abstract class Card : MonoBehaviour{
    protected enum CardType{
        Monster, Arcane
    }

    protected bool _selected = false;

    protected abstract CardType GetCardType();
    public abstract void SetCardData(ScriptableObject cardData);

    protected void OnMouseDown() {
        if(!_selected){
            _selected = true;
            CardSelector.Instance.AddCardToSelectedList(this);
        }else{
            _selected = false;
            GetComponentInParent<PlayerHandPositions>().SetPositionOccupied();
            transform.position += new Vector3(0f, -0.5f, -0.5f);
        }
    }
}