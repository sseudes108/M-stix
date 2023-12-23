using UnityEngine;

[CreateAssetMenu]
public class MonsterSO : ScriptableObject{
    public MonsterInfo MonsterInfo => GetInfo();
    public enum MonsterType{
        Alchemist, Angel, Beast, Witch,
        Demon, Dragon, Golem, Machine,
    }
    [SerializeField] private MonsterType _monsterType;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _anima, _character;
    [SerializeField] private int _atk, _def, _lvl;

    public MonsterInfo GetInfo(){
        return new MonsterInfo{
            Name = _name,
            Type = _monsterType,
            Anima = _anima,
            Character = _character,
            ATK = _atk,
            DEF = _def,
            LVL = _lvl,
        };
    }
}
public struct MonsterInfo{
    public string Name;
    public MonsterSO.MonsterType Type;
    public Sprite Anima, Character;
    public int ATK, DEF, LVL;
}
