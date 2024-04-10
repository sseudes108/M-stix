using System.Collections.Generic;
using UnityEngine;

public abstract class AIArchetype : MonoBehaviour {
    public abstract void SelectCard(List<CardMonster> lvl1MonstersList,List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList);
}