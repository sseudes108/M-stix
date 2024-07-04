public class DamageCard : ArcaneCard {
    public int Amount { get; set; }
    public override void SetCardInfo(){
        base.SetCardInfo();
        Amount = (Data as DamageCardSO).Amount;
    }
}