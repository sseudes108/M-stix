using TMPro;
using UnityEngine;

public class CardArcane : Card {

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _effect;

    private CardArcaneSO _arcaneData;
    [SerializeField] private EArcaneType _arcaneType;

    public override void SetCardData(ScriptableObject cardData){
        _cardData = cardData as CardArcaneSO;
        SetUpCardVariables();
    }

    public override void SetUpCardVariables(){
        _arcaneData = _cardData as CardArcaneSO;
        _arcaneType = _arcaneData.ArcaneType;
        _ilustration = _arcaneData.Ilustration;
        _name.text = _arcaneData.Name;
        _effect.text = _arcaneData.Effect;
    }

    public override ECardType GetCardType(){return ECardType.Arcane;}

    public EArcaneType GetArcaneType() {return _arcaneType;}
}