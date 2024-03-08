using UnityEngine;

[CreateAssetMenu(fileName = "CardArcaneSO", menuName = "CardArcaneSO", order = 0)]
public class CardArcaneSO : ScriptableObject {
    public EArcaneType ArcaneType;
    public Texture2D Ilustration;
    public string Name;
    public string Effect;

    [Header("Equip Card")]
    public EAnimaType AnimaLink;
    public int AttackModifier;
    public int DefenseModifier;
    public int LevelModifier;

    [Header("Damage/Heal Player Card")]
    public int Amount;
    public bool DamageCard;
}