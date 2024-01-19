using UnityEngine;

public class CardCreator : MonoBehaviour{
    public static CardCreator Instance {get; private set;}
    [SerializeField] private MonsterCard _monsterCardPrefab;
    [SerializeField] private ArcaneCard _arcaneCardPrefab;

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one CardCreator instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }

    public Card CreateCard(ScriptableObject cardData){
        if(cardData is MonsterCardSO){
            MonsterCardSO monsterCardData = cardData as MonsterCardSO;
            MonsterCard newMonsterCard = _monsterCardPrefab;
            newMonsterCard.SetCardData(monsterCardData);
            return newMonsterCard;
        }else{
            ArcaneCardSO arcaneCardData = cardData as ArcaneCardSO;
            ArcaneCard newArcaneCard = _arcaneCardPrefab;
            newArcaneCard.SetCardData(arcaneCardData);
            return newArcaneCard;
        }
    }
}
