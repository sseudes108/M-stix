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

    //
    She,
    //
}
public enum EArcaneType{
    Magic,
    Trap,
    Equip,
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