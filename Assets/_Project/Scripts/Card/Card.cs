using UnityEngine;
using TMPro;

public class Card : MonoBehaviour{
        
    public MonsterInfo MonsterInfo => _monsterInfo;
    public ArcaneInfo ArcaneInfo => _arcaneInfo;
    public CardSO.CardType CardType => _cardType;

    [SerializeField] private CardSO _cardData;

    //Monster
    [Header("Monster")]
    [SerializeField] private GameObject _monsterObject;
    private CardSO _monsterData;
    [SerializeField] private MonsterInfo _monsterInfo;
    [SerializeField] private SpriteRenderer _anima;
    [SerializeField] private SpriteRenderer _character;
    [SerializeField] private TMP_Text _atk;
    [SerializeField] private TMP_Text _def;
    [SerializeField] private TMP_Text _lvl;

    //Arcane
    [Header("Arcane")]
    [SerializeField] private GameObject _arcaneObject;
    private CardSO _arcaneData;
    [SerializeField] private ArcaneInfo _arcaneInfo;
    [SerializeField] private SpriteRenderer _front, _ilustration;
    [SerializeField] private TMP_Text _name, _effect;

    private CardSO.CardType _cardType;
    
    private void Start() {
        _cardType = _cardData._cardType;

        if(_cardType == CardSO.CardType.Monster){
            SetMonsterCard();
        }
        if(_cardType == CardSO.CardType.Arcane){
            SetArcaneCard();
        }
    }

    public void SetCardData(CardSO data){
        _cardData = data;
    }

    //Monster
    private void SetMonsterCard(){
        _arcaneObject.gameObject.SetActive(false);
        _monsterObject.gameObject.SetActive(true);

        _monsterInfo = _cardData.GetMonsterInfo();

        SetMonsterVisuals();
        SetMonsterStats();
    }
    private void SetMonsterVisuals(){
        _anima.sprite = MonsterInfo.Anima;
        _character.sprite = MonsterInfo.Character;        
    }
    private void SetMonsterStats(){
        _atk.text = MonsterInfo.ATK.ToString();
        _def.text = MonsterInfo.DEF.ToString();
        _lvl.text = MonsterInfo.LVL.ToString();
    }

    //Arcane
    private void SetArcaneCard(){
        _monsterObject.gameObject.SetActive(false);
        _arcaneObject.gameObject.SetActive(true);

        _arcaneInfo = _cardData.GetArcaneInfo();

        SetArcaneVisuals();
        SetArcaneTexts();
    }
    private void SetArcaneVisuals(){
        _front.sprite = ArcaneInfo.Front;
        _ilustration.sprite = ArcaneInfo.Ilustration;
    }
    private void SetArcaneTexts(){
        _name.text = ArcaneInfo.Name;
        _effect.text = ArcaneInfo.Effect;
    }
}
