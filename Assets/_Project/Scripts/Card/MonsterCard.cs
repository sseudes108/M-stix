using UnityEngine;
using TMPro;

public class MonsterCard : MonoBehaviour{
    public Monster MonsterInfo => _monsterInfo;

    [SerializeField] private CardSO _cardData;
    [SerializeField] private SpriteRenderer _monsterSpriteRenderer, _animaSpriteRenderer;
    
    [SerializeField] private TMP_Text _lvl, _atk, _def;
    private Monster _monsterInfo;
    private Monster.MonsterType _type;
    private string _name;
    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _monsterInfo = _cardData.GetMonsterInfo();
        SetCard(_monsterInfo);
    }

    private void Start(){
        // SetCard(_monsterInfo);
    }

    private void SetCard(Monster monsterInfo){
        _monsterSpriteRenderer.sprite = monsterInfo.MonsterSprite;
        _animaSpriteRenderer.sprite = monsterInfo.AnimaSprite;
        _type = monsterInfo.Type;
        _name = monsterInfo.Name;
        _lvl.text = monsterInfo.Level.ToString();
        _atk.text = monsterInfo.Atk.ToString();
        _def.text = monsterInfo.Def.ToString();
    }

    public void SetMonsterData(CardSO cardSO){
        _cardData = cardSO;
    }
}
