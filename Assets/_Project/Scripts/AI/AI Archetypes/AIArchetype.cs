using System.Collections.Generic;
using UnityEngine;

public abstract class AIArchetype{
 
    public bool BoardFusion = false;
    public int BoardFusionLvl;

    protected AICardsList CardsList;

    //Functions
    public abstract void SelectCard();
 
    public abstract int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card cardToPutOnBoard);
    public abstract int SelectMonsterMode(CardMonster monster);

    public void SetCardList(AICardsList updatedCardList){
        CardsList = updatedCardList;
    }

    public AICardsList GetCardList(){
        return CardsList;
    }

}