using System.Collections.Generic;

public class EnemyBoardPlaces : BoardPlace {
    public override List<BoardCardArcanePlace> ArcanePlacements => _arcanesPlacement;
    public override List<BoardCardMonsterPlace> MonsterPlacements => _monstersPlacement;
}