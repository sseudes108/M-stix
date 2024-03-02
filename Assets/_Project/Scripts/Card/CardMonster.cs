using TMPro;
using UnityEngine;

public class CardMonster : Card {
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _attack, _defense;

    // -- //
    [SerializeField] private int _lvl, _atk, _def;
    [SerializeField]private EMonsterType _monsterType;
    //Needs to be Serialize Fields (Dont Know Why)//

    public override void SetCardData(ScriptableObject cardData){
        _cardData = cardData as CardMonsterSO;
        SetUpCardVariables();
    }

    public override void SetUpCardVariables(){
        var monsterData = _cardData as CardMonsterSO;

        _ilustration = monsterData.Ilustration;

        _level.text = monsterData.Level.ToString();
        _lvl = monsterData.Level;

        _attack.text = monsterData.Attack.ToString();
        _atk = monsterData.Attack;

        _defense.text = monsterData.Defense.ToString();
        _def = monsterData.Defense;

        _monsterType = monsterData.MonsterType;
    }

    public int GetLevel() => _lvl;
    public int GetAttack() => _atk;
    public int GetDefense() => _def;
    public EMonsterType GetMonsterType() => _monsterType;
    public override ECardType GetCardType(){return ECardType.Monster;}
}