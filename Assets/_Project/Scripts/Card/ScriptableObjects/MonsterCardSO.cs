using UnityEngine;
using Mistix.Enums;

[CreateAssetMenu(fileName = "MonsterCardSO", menuName = "Mistix/MonsterCardSO", order = 0)]
public class MonsterCardSO : ScriptableObject {
    public EMonsterType MonsterType;
    public string Name;
    [Range(1,8)] public int Level;
    
    public int Atk, Def;
    public Sprite Character;
}