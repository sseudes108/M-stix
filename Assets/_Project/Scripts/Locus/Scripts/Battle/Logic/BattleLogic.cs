using System.Collections;
using UnityEngine;

public class BattleLogic : MonoBehaviour {
    [SerializeField] private Transform _attackerPosition, _attackerPosition2;
    [SerializeField] private Transform _attackedPosition, _attackedPosition2;

    private MonsterCard _monsterAttacker, _monsterAttacked;

    float _timer = 1f;

    public void StartBattleRoutine(){
        StartCoroutine(BattlePositionsRoutine());
    }

    private IEnumerator BattlePositionsRoutine(){
        MoveCardsToFirstPosition();
        yield return new WaitForSeconds(2f);

        MoveCardsToSecondPosition();
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(Battle());
        yield return null;

        MoveCardsToFirstPosition(27);
        yield return null;
    }

    public void SetBattleCards(MonsterCard attacker, MonsterCard deffender){
        _monsterAttacker = attacker;
        _monsterAttacked = deffender;
    }

#region Positions

    private void MoveCardsToFirstPosition(){
        _monsterAttacker.MoveCard(_attackerPosition);
        _monsterAttacked.MoveCard(_attackedPosition);
    }

    private void MoveCardsToSecondPosition(){
        _monsterAttacker.MoveCard(_attackerPosition2, 27);
        _monsterAttacked.MoveCard(_attackedPosition2, 27);
    }

    private void MoveCardsToFirstPosition(float speed){
        _monsterAttacker.MoveCard(_attackerPosition, speed);
        _monsterAttacked.MoveCard(_attackedPosition, speed);
    }
#endregion

    private IEnumerator Battle(){
        if(_monsterAttacked.IsInAttackMode){ // Is in attack Mode
            int atkAttacker = _monsterAttacker.Attack;
            int atkAttacked = _monsterAttacked.Attack;

            if(atkAttacker > atkAttacked){ // Won
                _monsterAttacked.Visuals.Dissolve.DissolveCard(Color.red);

            }else{
                // Not won
                if(atkAttacker == atkAttacked){// Draw
                    _monsterAttacker.Visuals.Dissolve.DissolveCard(Color.red);
                    _monsterAttacked.Visuals.Dissolve.DissolveCard(Color.red);

                }else{// Lost
                    _monsterAttacker.Visuals.Dissolve.DissolveCard(Color.red);

                }
            }
        }else{// In Def
            int atkAttacker = _monsterAttacker.Attack;
            int defDeffender = _monsterAttacked.Attack;

            if(atkAttacker > defDeffender){// Won
                _monsterAttacked.Visuals.Dissolve.DissolveCard(Color.red);
                
            }else{
                if(atkAttacker < defDeffender){ // Lost
                    //Apply Damage on attacking player
                }
            }
        }

        yield return null;
    }
}