using UnityEngine;

public readonly struct Monster{
    public enum MonsterType{
        Angel, Dragon, Machine, Golem,
        Alchemist, Witch, Beast, Demon,
    }
    public readonly MonsterType Type;
    public readonly string Name;
    public readonly int Level, Atk, Def;
    public readonly Sprite MonsterSprite;
    public readonly Sprite AnimaSprite;

    public Monster(MonsterType monsterType, string name, int lvl, int atk, int def, Sprite monsterSprite, Sprite animaSprite){
        Type = monsterType;
        Name = name;
        Level = lvl;
        Atk = atk;
        Def = def;
        MonsterSprite = monsterSprite;
        AnimaSprite = animaSprite;
    }
}