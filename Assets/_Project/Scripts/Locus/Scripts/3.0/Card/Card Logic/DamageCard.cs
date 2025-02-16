namespace Mistix{
    public class DamageCard : ArcaneCard {
        public int Amount { get; set; }
        public override void SetCardInfo(){
            base.SetCardInfo();
            Amount = (_data as DamageCardSO).Amount;
        }
    }
}