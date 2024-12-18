using TMPro;
using UnityEngine;

public abstract class ArcaneCard : Card {
    protected EArcaneType ArcaneType;
    protected string Effect;
    [SerializeField] private TextMeshProUGUI _effect;

    public override void SetCardInfo(){
        var CardData = Data as ArcaneCardSO;
        base.SetCardInfo();
        ArcaneType = CardData.ArcaneType;

        Effect = CardData.Effect;
    }

    public override void SetCardText(){
        base.SetCardText();
        _effect.text = Effect;
    }
}