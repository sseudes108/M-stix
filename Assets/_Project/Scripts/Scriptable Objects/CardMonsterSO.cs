using System;
using System.Net.Http.Headers;
using UnityEngine;

[CreateAssetMenu(fileName = "CardMonsterSO", menuName = "CardMonsterSO", order = 0)]
public class CardMonsterSO : ScriptableObject {
    public EMonsterType MonsterType;
    public Texture2D Ilustration;
    public string Name;
    public string Description;
    
    [Range(1,8)]
    public int Level;
    public int Attack;
    public int Defense;
}
