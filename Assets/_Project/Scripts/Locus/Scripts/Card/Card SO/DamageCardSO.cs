using UnityEngine;

[CreateAssetMenu(fileName = "DamageCardSo", menuName = "Mistix/Card/Arcane/DamageCardSo", order = 0)]
public class DamageCardSO : ArcaneCardSO {
    [Header("Damage/Heal")]
    public int Amount;
    public EAnimaType AnimaType;
}