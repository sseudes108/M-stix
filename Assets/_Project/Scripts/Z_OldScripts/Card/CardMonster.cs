// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public class CardMonster : Card {
//     [Header("Stats")]
//     [SerializeField] private TextMeshProUGUI _level;
//     [SerializeField] private TextMeshProUGUI _attack, _defense;

//     [SerializeField] private int _lvl, _atk, _def;
//     [SerializeField] private EMonsterType _monsterType;
//     [SerializeField] private List<EAnimaType> _animas;
//     [SerializeField] private EAnimaType _anima;
//     [SerializeField] private bool _attackMode = true;
//     [SerializeField] private bool _isAttacking = false;

//     private void Start(){
//         SetUpCardVariables();
//     }

//     public override void SetCardData(ScriptableObject cardData){
//         _cardData = cardData as CardMonsterSO;
//         SetUpCardVariables();
//     }

//     public override void SetUpCardVariables(){
//         var monsterData = _cardData as CardMonsterSO;

//         _ilustration = monsterData.Ilustration;
        
//         _attack.text = monsterData.Attack.ToString();
//         _atk = monsterData.Attack;

//         _defense.text = monsterData.Defense.ToString();
//         _def = monsterData.Defense;

//         _level.text = monsterData.Level.ToString();
//         _lvl = monsterData.Level;

//         _animas = new(){
//             monsterData.FirstAnima,
//             monsterData.SecondAnima,
//         };

//         _monsterType = monsterData.MonsterType;
//     }

//     public void ChangeMonsterStats(int newAtk, int newDef, int newLvl){
//         _atk = newAtk;
//         _def = newDef;
//         _lvl = newLvl;

//         //UI Card
//         _attack.text = newAtk.ToString();
//         _defense.text = newDef.ToString();
//         _level.text = newLvl.ToString();
//     }

//     public int GetLevel() => _lvl;
//     public int GetAttack() => _atk;
//     public int GetDefense() => _def;
//     public (int, int, int) GetMonsterStats(){return (GetAttack(), GetDefense(), GetLevel());}
//     public List<EAnimaType> GetAnimas() => _animas;
//     public EMonsterType GetMonsterType() => _monsterType;
//     public override ECardType GetCardType(){return ECardType.Monster;}

//     public void SetAnima(EAnimaType selectedAnima){
//         _anima = selectedAnima;
//     }

//     public EAnimaType GetAnima(){return _anima;}

//     public void SetAttackMode(){
//         _attackMode = true;
//     }

//     public void SetDefenseMode(){
//         _attackMode = false;
//     }

//     public bool IsInAttackMode(){
//         return _attackMode;
//     }
//     public void SetMonsterAttacking(bool isAttacking){
//         _isAttacking = isAttacking;
//     }
//     public bool IsAttacking(){
//         return _isAttacking;
//     }
 
//     public void ShowMonsterModeOptions(){
//         ShowOptions();
//         _selection1.text = "Attack";
//         _selection2.text = "Defense";
//     }
    
//     public void ShowAnimaOptions(){
//         ShowOptions();
//         _selection1.text = $"{_animas[0]}";
//         _selection2.text = $"{_animas[1]}";
//     }
// }