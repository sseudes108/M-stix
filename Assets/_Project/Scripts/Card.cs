using UnityEngine;

public class Card : MonoBehaviour{
    private MonsterCard _monsterCard;
    private ArcaneCard _arcaneCard;

    private void Start() {
        gameObject.TryGetComponent<MonsterCard>(out _monsterCard);
        gameObject.TryGetComponent<ArcaneCard>(out _arcaneCard);
    }

    public CardSO.CardType GetCardType(){
        if(_monsterCard != null){
            return CardSO.CardType.Monster;
        }else{
            return CardSO.CardType.Arcane;
        }
    }

    public MonsterCard GetMonsterInfo(){
        return _monsterCard;
    }
    public ArcaneCard GetArcaneInfo(){
        return _arcaneCard;
    }

}
