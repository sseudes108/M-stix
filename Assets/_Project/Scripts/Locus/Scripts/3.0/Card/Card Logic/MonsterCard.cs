using TMPro;
using UnityEngine;

namespace Mistix{
    public class MonsterCard : Card {

        [Header("Monster Specifications")]
        public EMonsterType MonsterType {get; private set;}
        public EAnimaType FirstAnima {get; private set;}
        public EAnimaType SecondAnima {get; private set;}
        public EAnimaType ActiveAnima {get; private set;}

        [Header("Stats")]
        public int Level {get; private set;}
        public int Attack {get; private set;}
        public int Deffense {get; private set;}

        [Header("Card Settings")]
        public bool AnimaSelected {get; private set;} = false;
        public bool ModeSelected {get; private set;} = false;
        public bool IsInAttackMode {get; private set;} = true;
        public bool CanAttack {get; private set;}
        // public bool CanChangeMode {get; private set;}
        // public bool IsDead {get; private set;} = false;


        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private TextMeshProUGUI _attackLabel;
        [SerializeField] private TextMeshProUGUI _deffenseLabel;

        // private MonsterCardSO _cardData;

        public override void SetCardInfo(){
            base.SetCardInfo();
            var _cardData = Data as MonsterCardSO;
            MonsterType = _cardData.MonsterType;
            FirstAnima = _cardData.FirstAnima;
            SecondAnima = _cardData.SecondAnima;
            Level = _cardData.Level;
            Attack = _cardData.Attack;
            Deffense = _cardData.Deffense;

            SetCardText();
        }
        
        public override void SetCardText(){
            base.SetCardText();
            _levelLabel.text = Level.ToString();
            _attackLabel.text = Attack.ToString();
            _deffenseLabel.text = Deffense.ToString();
        }

        public void SelectAnima(int anima) { 
            AnimaSelected = true;

            if(anima == 1){
                _visuals.Anima.Anima1Selected();
                ActiveAnima = FirstAnima;
                return;
            }

            if(anima == 2){
                _visuals.Anima.Anima2Selected();
                ActiveAnima = SecondAnima;
                return;
            }
        }

        public void SelectMode() { ModeSelected = true; }
        public void SetDeffenseMode() { IsInAttackMode = false; }
        // public void SetAttackMode() { IsInAttackMode = true; }
        // public void SetCanChangeMode(bool canChangeMode) { CanChangeMode = canChangeMode; }
        public void SetCanAttack(bool canAttack) { CanAttack = canAttack; }

        // public void MonsterAttacked(){
        //     SetCanAttack(false);
        //     SetCanChangeMode(false);
        // }

        // public void Die() { 
        //     IsDead = true;
        //     BoardPlace.SetPlaceFree();
        // }

        public override void ResetCardStats(){ // Used to reset stats on board fusions
            base.ResetCardStats();
            AnimaSelected = false;   
            ModeSelected = false;
            _visuals.ResetAnimaColors();
        }

        // public void BuffAttack(){
        //     Attack += 500;
        // }
        
        // public void DebuffAttack(){
        //     Attack -= 500;
        // }

        // public void ResetAttack(){
        //     Attack = _cardData.Attack;
        // }
    }
}