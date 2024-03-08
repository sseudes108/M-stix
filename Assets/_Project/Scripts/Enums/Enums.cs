using System.Drawing;

public enum ECardType{
    Arcane,
    Monster,

    //
    Err,
    //
}
public enum EMonsterType{
    Angel,
    Dragon,
    Machina,
    Golem,
    Magician,
    Demon,

    //
    She,
    //
}
public enum EArcaneType{
    Trap,
    Equip,
    Field,
    Destroyer,
    DamageToPlayer,
    DamageToMonster,
}

public enum EAnimaType{
    Venus,
    Mars,
    Saturn,
    Jupiter,
    Mercury ,
    Sun,
    Moon,
}

public enum EStateMachinePhase{
    Start,
    Draw,
    CardSelection,
    Fusion,
    Selections,
    BoardPlaceSelection,
    Action,
    Attack,
    Damage,
    ActionTwo,
    End
}