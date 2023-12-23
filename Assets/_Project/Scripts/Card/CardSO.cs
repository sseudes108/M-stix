using UnityEngine;
[CreateAssetMenu]
public class CardSO : ScriptableObject{
    public enum CardType{
        Arcane, Monster,
    }

    //Arcane
    public CardType _cardType;

    public enum ArcaneType{
        Magic, Trap, NotArcane,
    }
    [Header("Arcane")]
    public ArcaneType _arcaneType;
    public string _arcaneName, _effect;
    public Sprite _front, _ilustration;

    public ArcaneInfo GetArcaneInfo(){
        return new ArcaneInfo{
            Name = _arcaneName,
            ArcaneType = _arcaneType,
            Effect = _effect,
            Front = _front,
            Ilustration = _ilustration
        };
    }

    //Monster
    public MonsterInfo MonsterInfo => GetMonsterInfo();
    public enum MonsterType{
        Alchemist, Angel, Beast, Witch,
        Demon, Dragon, Golem, Machine, NotMonster,
    }
    [Header("Monster")]
    [SerializeField] private MonsterType _monsterType;
    [SerializeField] private string _monstarName;
    [SerializeField] private Sprite _anima, _character;
    [SerializeField] private int _atk, _def, _lvl;

    public MonsterInfo GetMonsterInfo(){
        return new MonsterInfo{
            Name = _monstarName,
            Type = _monsterType,
            Anima = _anima,
            Character = _character,
            ATK = _atk,
            DEF = _def,
            LVL = _lvl,
        };
    }
}
public struct ArcaneInfo{
    public CardSO.ArcaneType ArcaneType;
    public string Name, Effect;
    public Sprite Front, Ilustration;
}
public struct MonsterInfo{
    public string Name;
    public CardSO.MonsterType Type;
    public Sprite Anima, Character;
    public int ATK, DEF, LVL;
}
