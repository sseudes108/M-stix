using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardPlaces : BoardPlace {
    public override List<Transform> MonsterPlaces => _monsterPlaces;
    public override List<Transform> ArcanePlaces => _arcanePlaces;
}