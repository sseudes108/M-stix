using TMPro;
using UnityEngine;
using Mistix.Enums;

namespace Mistix{
    public class MonsterCard : Card{
        [SerializeField] private MonsterCardSO _monsterCardData; 
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshProUGUI _monsterAtk, _monsterDef, _monsterLevel;
        private string _monsterName;
        private EMonsterType _monsterType;

        private void Start() {
            SetUpMonterCard();
        }

        public override void SetUpCardData(ScriptableObject CardData){
            _monsterCardData = CardData as MonsterCardSO;
        }

        public void SetUpMonterCard(){
            _monsterName = _monsterCardData.Name;
            _monsterAtk.text = _monsterCardData.Atk.ToString();
            _monsterDef.text = _monsterCardData.Def.ToString();
            _monsterLevel.text = _monsterCardData.Level.ToString();
            _spriteRenderer.sprite = _monsterCardData.Character;
            _monsterType = _monsterCardData.MonsterType;
        }

        public override ECardType GetCardType(){
            return ECardType.Monster;
        }

        public int GetMonsterAtk(){
            return _monsterCardData.Atk;
        }

        public int GetMonsterLevel(){
            return _monsterCardData.Level;
        }

        public EMonsterType GetMonsterType(){
            return _monsterType;
        }

        public override string GetCardInfo(){
            return $"Nome: {_monsterCardData.Name}, Level: {_monsterCardData.Level.ToString()}, Atk: {_monsterCardData.Atk.ToString()}";
        }
    }
}