using System;
using TMPro;
using UnityEngine;

public abstract class Card : MonoBehaviour{
    public enum CardType{
        Monster, Arcane
    }

    public Action OnAnyCardSelectionChanged;

    [SerializeField] private GameObject _lineNumber;
    [SerializeField] private TextMeshProUGUI _lineInNumberText;

    public static Action<Card> OnAnyCardSelected;

    protected bool _selected = false;
    public abstract CardType GetCardType();
    public abstract void SetCardData(ScriptableObject cardData);
    protected abstract void OnMouseEnter();
    
    protected void OnMouseDown() {
        if(!_selected){
            _selected = true;
            transform.position += new Vector3(0f, 0.5f, 0.5f);

            OnAnyCardSelected?.Invoke(this);
        }else{
            _selected = false;
            transform.position += new Vector3(0f, -0.5f, -0.5f);

            OnAnyCardSelected?.Invoke(this);
        }
    }

    public void UpdateNumberInLine(int numberInLine){
        _lineNumber.gameObject.SetActive(true);
        _lineInNumberText.text = numberInLine.ToString();
    }

    public void DeactiveNumberInLine(){
        _lineNumber.gameObject.SetActive(false);
    }

    public bool IsSelected() => _selected;
}