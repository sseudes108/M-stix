using System.Collections.Generic;

public class PlayerBoardPlaces : BoardPlace {
    public override List<BoardCardArcanePlace> ArcanePlacements => _arcanesPlacement;
    public override List<BoardCardMonsterPlace> MonsterPlacements => _monstersPlacement;
}