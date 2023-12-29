using UnityEngine;

[CreateAssetMenu]
public class CardSO : ScriptableObject{

    public enum CardType{
        Arcane, Monster
    }
    public CardType cardType;
    public string _name;

    [Header("Arcane Card")]
    public Arcane.ArcaneType _arcaneType;
    public string _effect;
    public Sprite _illustration, _frame;
    
    [Header("Monster Card")]
    public Monster.MonsterType _monsterType;
    public int _level, _atk, _def;
    public Sprite _monsterSprite, _animaSprite;

    public Monster GetMonsterInfo(){
        Monster monster = new(_monsterType, _name, _level, _atk, _def, _monsterSprite, _animaSprite);
        return monster;
    }

    public Arcane GetArcaneInfo(){
        Arcane arcane = new(_arcaneType, _illustration, _frame, _name, _effect);
        return arcane;
    }
}