using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIArchetype{

    protected AICardsList CardsList;
    public bool BoardFusion = false;
    public int BoardFusionLvl;

    //Selection Phase
    public abstract void SelectCard();
    public abstract int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card cardToPutOnBoard);
    public abstract int SelectMonsterMode(CardMonster monster);
    public abstract int SelectCardFace(Card card);

    //Attack
    public void StartCheckAttackRoutine(){
        BattleManager.Instance.StartCoroutine(CheckAttackRoutine());
    }

    public abstract IEnumerator CheckAttackRoutine();

    //Card List
    public void SetCardList(AICardsList updatedCardList){
        CardsList = updatedCardList;
    }

    public AICardsList GetCardsList(){
        return CardsList;
    }
}