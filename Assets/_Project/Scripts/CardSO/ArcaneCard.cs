using TMPro;
using UnityEngine;

public class ArcaneCard : Card{
    private CardType _cardType = CardType.Arcane;

    [SerializeField] private ArcaneCardSO _arcaneCardData;
    [SerializeField] private SpriteRenderer _front, _illustration;
    [SerializeField] private TMP_Text _name, _effect;

    private void Start(){
        SetCard();
    }

    private void SetCard(){
        _front.sprite = _arcaneCardData.Front;
        _illustration.sprite = _arcaneCardData.Illustration;
        _name.text = _arcaneCardData.Name;
        _effect.text = _arcaneCardData.Effect;
    }

    private string GetName(){
        return _arcaneCardData.Name;
    }

    protected override CardType GetCardType(){
        return _cardType;
    }

    public override void SetCardData(ScriptableObject cardData){
        _arcaneCardData = (ArcaneCardSO)cardData;
    }

    protected override void OnMouseEnter(){
        BattleUI.Instance.UpdateArcaneCardUIInfo(GetName());
    }
}
