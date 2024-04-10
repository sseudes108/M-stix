using System.Collections.Generic;

public abstract class AIArchetype{
    public abstract void SelectCard(List<CardMonster> lvl1MonstersList,List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList, List<CardMonster> monstersOnField);
    public abstract int SelectMonsterMode(int atk, List<CardMonster> faceDownMonsters, List<CardMonster> faceUpMonsters, List<CardMonster> monstersInDefense, List<CardMonster> monstersInAttack);
}