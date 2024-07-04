using TMPro;
using UnityEngine;

public abstract class ArcaneCard : Card {
    protected EArcaneType ArcaneType;
    // protected string Name;
    protected string Effect;

    [SerializeField] private TextMeshProUGUI _Name;
    [SerializeField] private TextMeshProUGUI _effect;

    public override void SetCardInfo(){
        var CardData = Data as ArcaneCardSO;
        base.SetCardInfo();
        ArcaneType = CardData.ArcaneType;
        // Name = CardData.Name;
        Effect = CardData.Effect;
    }

    public override void SetCardText(){
        _Name.text = Name;
        _effect.text = Effect;
    }
}