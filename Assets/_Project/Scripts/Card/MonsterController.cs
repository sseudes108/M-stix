using UnityEngine;
using TMPro;

public class MonsterController : MonoBehaviour{
    public MonsterInfo MonsterInfo => _monsterInfo;

    [SerializeField] private MonsterSO _monsterData;
    [SerializeField] private MonsterInfo _monsterInfo;
    [SerializeField] private SpriteRenderer _anima;
    [SerializeField] private SpriteRenderer _character;
    [SerializeField] private TMP_Text _atk;
    [SerializeField] private TMP_Text _def;
    [SerializeField] private TMP_Text _lvl;

    private void Awake() {
        _monsterInfo = _monsterData.GetInfo();
    }

    private void Start() {
        SetVisuals();
        SetStats();
    }

    private void SetVisuals(){
        _anima.sprite = MonsterInfo.Anima;
        _character.sprite = MonsterInfo.Character;        
    }
    private void SetStats(){
        _atk.text = MonsterInfo.ATK.ToString();
        _def.text = MonsterInfo.DEF.ToString();
        _lvl.text = MonsterInfo.LVL.ToString();
    }

    public void SetMonsterData(MonsterSO  data){
        _monsterData = data;
    }
}
