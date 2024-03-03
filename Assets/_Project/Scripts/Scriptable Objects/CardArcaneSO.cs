using UnityEngine;

[CreateAssetMenu(fileName = "CardArcaneSO", menuName = "CardArcaneSO", order = 0)]
public class CardArcaneSO : ScriptableObject {
    public EArcaneType ArcaneType;
    public Texture2D Ilustration;
    public string Name;
    public string Effect;

    [Header("If is Equip Card")]
    public EAnimaType AnimaLink;
    public int AttackModifier;
    public int DefenseModifier;
    public int LevelModifier;
}