using TMPro;
using UnityEngine;

public class CardArcane : Card {

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _effect;

    public override void SetCardData(ScriptableObject cardData){
        _cardData = cardData as CardArcaneSO;

        SetUpCardVariables();
    }

    public override void SetUpCardVariables(){
        var arcaneData = _cardData as CardArcaneSO;
        _ilustration = arcaneData.Ilustration;
        _name.text = arcaneData.Name;
        _effect.text = arcaneData.Effect;
    }

    public override ECardType GetCardType(){return ECardType.Arcane;}

}