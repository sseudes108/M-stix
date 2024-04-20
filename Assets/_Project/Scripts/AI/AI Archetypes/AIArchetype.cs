using System.Collections.Generic;
using UnityEngine;

public abstract class AIArchetype{
    protected AICardsList CardsList;
    public bool BoardFusion = false;
    public int BoardFusionLvl;

    public abstract void SelectCard();
    public abstract int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card cardToPutOnBoard);
    public abstract int SelectMonsterMode(CardMonster monster);
    public abstract int SelectCardFace(Card card);
    public abstract void CheckAttack();

    public void SetCardList(AICardsList updatedCardList){
        CardsList = updatedCardList;
    }

    public AICardsList GetCardsList(){
        return CardsList;
    }
}