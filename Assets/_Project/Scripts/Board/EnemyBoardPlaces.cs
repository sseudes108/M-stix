using System.Collections.Generic;
using UnityEngine;

public class EnemyBoardPlaces : BoardPlace {
    public override List<Transform> MonsterPlaces => _monsterPlaces;
    public override List<Transform> ArcanePlaces => _arcanePlaces;
}