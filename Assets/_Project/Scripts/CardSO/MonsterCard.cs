using UnityEngine;
using TMPro;

public class MonsterCard : Card{
    private CardType _cardType = CardType.Monster;
    [SerializeField] private MonsterCardSO _monsterCardData;
    [SerializeField] private SpriteRenderer _monsterSpriteRenderer, _animaSpriteRenderer;
    [SerializeField] private MonsterCardSO.MonsterType _monsterType;
    [SerializeField] private TMP_Text _level, _atk, _def;

    private void Start() {
        SetCard();
        gameObject.SetActive(true);
    }

    private void SetCard(){
        _monsterType = _monsterCardData.Type;
        _monsterSpriteRenderer.sprite = _monsterCardData.Character;
        _animaSpriteRenderer.sprite = _monsterCardData.Anima;
        _level.text = _monsterCardData.Level.ToString();
        _atk.text = _monsterCardData.ATK.ToString();
        _def.text = _monsterCardData.DEF.ToString();
    }

    protected override CardType GetCardType(){
        return _cardType;
    }

    public override void SetCardData(ScriptableObject cardData){
        _monsterCardData = (MonsterCardSO)cardData;
    }

    public int GetLevel(){
        return _monsterCardData.Level;
    }
    public int GetAtk(){
        return _monsterCardData.ATK;
    }
    public MonsterCardSO.MonsterType GetMonsterType(){
        return _monsterCardData.Type;
    }
}