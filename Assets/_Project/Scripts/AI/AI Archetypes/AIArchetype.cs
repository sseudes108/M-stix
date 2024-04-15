using System.Collections.Generic;
using UnityEngine;

public abstract class AIArchetype{
    protected List<CardMonster> Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList, 
    Lvl4MonstersList, Lvl5MonstersList, Lvl6MonstersList, Lvl7MonstersList;
    protected List<CardMonster> MonstersOnAIField;

    protected bool BoardFusion = false;
    protected int BoardFusionLvl;

    // protected bool  strongestFusion = true;


    
    // List<CardMonster> lvl1MonstersList,List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList
    public abstract void SelectCard(List<CardMonster> monstersOnField);
    public abstract int SelectMonsterMode(int atk, List<CardMonster> faceDownMonsters, List<CardMonster> faceUpMonsters, List<CardMonster> monstersInDefense, List<CardMonster> monstersInAttack);

    public abstract int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card cardToPutOnBoard);

    public void SetMonstersList(List<CardMonster> monstersOnField, List<CardMonster> lvl1MonstersList,List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardMonster> lvl4MonstersList, List<CardMonster> lvl5MonstersList, List<CardMonster> lvl6MonstersList, List<CardMonster> lvl7MonstersList){
        MonstersOnAIField = monstersOnField;
        Lvl1MonstersList = lvl1MonstersList;
        Lvl2MonstersList = lvl2MonstersList;
        Lvl3MonstersList = lvl3MonstersList;
        Lvl4MonstersList = lvl4MonstersList;
        Lvl5MonstersList = lvl5MonstersList;
        Lvl6MonstersList = lvl6MonstersList;
        Lvl7MonstersList = lvl7MonstersList;
    }
}