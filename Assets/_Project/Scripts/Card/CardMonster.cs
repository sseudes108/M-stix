using TMPro;
using UnityEngine;

public class CardMonster : Card {
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _attack, _defense;

    public override void SetCardData(ScriptableObject cardData){
        _cardData = cardData as CardMonsterSO;
        SetUpCardVariables();
    }

    public override void SetUpCardVariables(){
        var monsterData = _cardData as CardMonsterSO;

        _ilustration = monsterData.Ilustration;
        _level.text = monsterData.Level.ToString();
        _attack.text = monsterData.Attack.ToString();
        _defense.text = monsterData.Defense.ToString();
    }
}