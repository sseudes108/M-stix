using TMPro;
using UnityEngine;

public class MonsterCard : Card {
    public EMonsterType MonsterType { get; set; }
    public EAnimaType FirstAnima { get; set; }
    public EAnimaType SecondAnima { get; set; }
    public int Level { get; set; }
    public int Attack { get; set; }
    public int Deffense { get; set; }

    [SerializeField] private TextMeshProUGUI _levelLabel;
    [SerializeField] private TextMeshProUGUI _attackLabel;
    [SerializeField] private TextMeshProUGUI _deffenseLabel;

    public override void SetCardInfo(){
        base.SetCardInfo();
        var CardData = Data as MonsterCardSO;
        MonsterType = CardData.MonsterType;
        FirstAnima = CardData.FirstAnima;
        SecondAnima = CardData.SecondAnima;
        Level = CardData.Level;
        Attack = CardData.Attack;
        Deffense = CardData.Deffense;
    }
    
    public override void SetCardText(){
        base.SetCardText();
        _levelLabel.text = Level.ToString();
        _attackLabel.text = Attack.ToString();
        _deffenseLabel.text = Deffense.ToString();
    }
}