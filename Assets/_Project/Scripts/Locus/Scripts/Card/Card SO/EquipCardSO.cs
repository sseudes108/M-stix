using UnityEngine;

[CreateAssetMenu(fileName = "EquipCardSO", menuName = "Mistix/Card/Arcane/EquipCardSO", order = 0)]
public class EquipCardSO : ArcaneCardSO {
    [Header("Equip")]
    public EAnimaType AnimaLink;
    public int AttackModifier;
    public int DefenseModifier;
    public int LevelModifier;
}