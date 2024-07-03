public class DamageCard : ArcaneCard {
    public int Amount { get; set; }
    public override void SetCardInfo(){
        base.SetCardInfo();
        var CardData = Data as DamageCardSO;
        Amount = CardData.Amount;
    }
}