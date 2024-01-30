using Mistix.Enums;
using UnityEngine;

public class Card: MonoBehaviour{

    private ECardType _cardType;
    private string _cardInfo;

    public virtual void SetUpCardData(ScriptableObject CardData){

    }

    public virtual ECardType GetCardType(){
        return _cardType;
    }

    public virtual string GetCardInfo(){
        return _cardInfo;
    }

}