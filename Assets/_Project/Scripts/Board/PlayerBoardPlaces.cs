using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardPlaces : BoardPlace {
    public override List<BoardCardArcanePlace> ArcanePlacements => _arcanesPlacement;
    public override List<BoardCardMonsterPlace> MonsterPlacements => _monstersPlacement;
}