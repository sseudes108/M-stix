using System.Collections.Generic;
using UnityEngine;

public class EnemyBoardPlaces : BoardPlace {
    public override List<Transform> MonsterPlaces => _monsterPlaces;
    public override List<Transform> ArcanePlaces => _arcanePlaces;

    public List<BoardCardMonsterPlace> GetMonstersPlacement(){
        List<BoardCardMonsterPlace> monstersPlacement = new();
        foreach(var place in MonsterPlaces){
            monstersPlacement.Add(place.GetComponentInChildren<BoardCardMonsterPlace>());
        }
        return monstersPlacement;
    }
}