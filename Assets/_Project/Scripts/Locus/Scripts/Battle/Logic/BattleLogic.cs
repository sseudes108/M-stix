using System.Collections;
using UnityEngine;

public class BattleLogic : MonoBehaviour {

    private Battle _battle;

    [SerializeField] private Transform _attackerPosition, _attackerPosition2;
    [SerializeField] private Transform _attackedPosition, _attackedPosition2;

    private MonsterCard _monsterAttacker, _monsterTarget;
    private Transform _attackerOriginalPos, _targetOriginalPos;

    public void StartBattleRoutine(){
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine(){
        MoveCardsToFirstPosition();
        yield return new WaitForSeconds(1f);

        MoveCardsToSecondPosition();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(MoveCardsToFirstPosition(27));
        yield return new WaitForSeconds(0.7f);

        StartCoroutine(Battle());
        yield return null;
    }

    public void SetBattleCards(MonsterCard attacker, MonsterCard target){
        _attackerOriginalPos = attacker.GetBoardPlace().transform;
        _targetOriginalPos = target.GetBoardPlace().transform;

        _monsterAttacker = attacker;
        _monsterTarget = target;
    }

    public void SetBattleController(Battle battle) { _battle = battle; }

#region Positions
    private void MoveCardsToFirstPosition(){
        _monsterAttacker.MoveCard(_attackerPosition);
        _monsterTarget.MoveCard(_attackedPosition);
    }

    private void MoveCardsToSecondPosition(){
        _monsterAttacker.MoveCard(_attackerPosition2, 27);
        _monsterTarget.MoveCard(_attackedPosition2, 27);
    }

    private IEnumerator MoveCardsToFirstPosition(float speed){
        if(!_monsterAttacker.IsDead){
            _monsterAttacker.MoveCard(_attackerPosition, speed);
        }else{
            _monsterAttacker.MoveCard(_attackerPosition, speed);
        }

        if(!_monsterTarget.IsDead){
            _monsterTarget.MoveCard(_attackedPosition, speed);
        }else{
            _monsterTarget.MoveCard(_attackedPosition, speed);
        }

        yield return null;
    }
#endregion

    private IEnumerator Battle(){
        _monsterAttacker.MonsterAttacked();
        
        if(_monsterTarget.IsInAttackMode){ // Is in attack Mode
            int atkAttacker = _monsterAttacker.Attack;
            int atkTarget = _monsterTarget.Attack;

            if(atkAttacker > atkTarget){ // Won
                StartCoroutine(KillMonster(_monsterTarget));
            }else{
                // Not won
                if(atkAttacker == atkTarget){// Draw
                    StartCoroutine(KillMonster(_monsterAttacker));
                    StartCoroutine(KillMonster(_monsterTarget));

                }else{// Lost
                    StartCoroutine(KillMonster(_monsterAttacker));
                }
            }
        }else{// In Def
            int atkAttacker = _monsterAttacker.Attack;
            int defTarget = _monsterTarget.Attack;

            if(atkAttacker > defTarget){// Won
                StartCoroutine(KillMonster(_monsterTarget));

            }else{
                if(atkAttacker < defTarget){ // Lost
                    //Apply Damage on attacking player
                }
            }
        }

        yield return null;
        _battle.BattleManager.AttackEnded();

        ReturnToOriginalPositions();
    }

    private IEnumerator KillMonster(MonsterCard monster){
        monster.Visuals.Dissolve.DissolveCard(Color.red);
        monster.Die();

        yield return new WaitForSeconds(1f);
        monster.DestroyCard();
    }

    private void ReturnToOriginalPositions(){
        if(!_monsterAttacker.IsDead){
            _monsterAttacker.GetBoardPlace().SetCardInPlace(_monsterAttacker);
        }

        if(!_monsterTarget.IsDead){
            _monsterTarget.GetBoardPlace().SetCardInPlace(_monsterTarget);
        }
    }
}