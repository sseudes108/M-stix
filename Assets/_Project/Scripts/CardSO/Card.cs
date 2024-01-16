using TMPro;
using UnityEngine;

public abstract class Card : MonoBehaviour{
    protected enum CardType{
        Monster, Arcane
    }

    [SerializeField] private GameObject _lineNumber;
    [SerializeField] private TextMeshProUGUI _lineInNumberText;
    protected bool _selected = false;
    protected abstract CardType GetCardType();
    public abstract void SetCardData(ScriptableObject cardData);
    protected abstract void OnMouseEnter();
    protected void OnMouseDown() {
        if(!_selected){
            _selected = true;
            transform.position += new Vector3(0f, 0.5f, 0.5f);

            CardSelector.Instance.AddCardToSelectedList(this);
        }else{
            _selected = false;
            transform.position += new Vector3(0f, -0.5f, -0.5f);

            // GetComponentInParent<PlayerHandPositions>().SetPositionOccupied();
            CardSelector.Instance.RemoveCardFromSelectedList(this);
        }
    }

    public void UpdateNumberInLine(int numberInLine){
        _lineNumber.gameObject.SetActive(true);
        _lineInNumberText.text = numberInLine.ToString();
    }

    public void DeactiveNumberInLine(){
        _lineNumber.gameObject.SetActive(false);
    }
}