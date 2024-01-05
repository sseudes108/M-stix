using UnityEngine;

[CreateAssetMenu]
public class MonsterCardSO : ScriptableObject {
    public enum MonsterType{
        Angel, Dragon, Machine, Golem,
        Witch, Alchemist, Beast, Demon
    }
    public MonsterType Type;
    public string Name;
    public Sprite Character, Anima;
    public int Level, ATK, DEF;
}