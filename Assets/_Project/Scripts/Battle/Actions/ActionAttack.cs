using System.Collections;
using UnityEngine;

public class ActionAttack : MonoBehaviour {

    CardMonster _monster1, _monster2;
    Transform _monster1OriginalPosition, _monster2OriginalPosition;
    [SerializeField] private Transform _monsterPos1, _monsterPos2;

    private float _monsterMoveWait = 2f;

    private void OnEnable() {
        BoardCardMonsterPlace.OnAttack += BoardCardMonsterPlace_OnAttack;
    }

    private void OnDisable() {
        BoardCardMonsterPlace.OnAttack -= BoardCardMonsterPlace_OnAttack;
    }

    private void BoardCardMonsterPlace_OnAttack(BoardCardMonsterPlace place, CardMonster monster){
        _monster1 = null;
        _monster2 = null;
        _monster1OriginalPosition = null;
        _monster2OriginalPosition = null;

        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.AttackPhase);
        
        var oponentTargets = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces();
        BattleManager.Instance.BoardPlaceVisuals.HighLightAttackTargetPlaces(oponentTargets);

        _monster1OriginalPosition = place.transform;
        _monster1 = monster;
    }

    public void StartMonstarBattle(BoardCardMonsterPlace place, CardMonster monster2){
        StartCoroutine(StartMonsterBattleRoutine(place, monster2));
    }

    private IEnumerator StartMonsterBattleRoutine(BoardCardMonsterPlace place, CardMonster monster2){
        _monster2OriginalPosition = place.transform;
        _monster2 = monster2;

        _monster1.MoveCard(_monsterPos1);
        _monster2.MoveCard(_monsterPos2);

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.UIBattleManager.ClearUI();

        yield return new WaitForSeconds(1.2f);

        if(_monster2.IsInAttackMode()){
            StartCoroutine(AttackMonsterInAttackMode());
        }else{
            StartCoroutine(AttackMonsterInDefenseMode());
        }
        _monster1.SetMonsterAttacking(false);
    }

    private IEnumerator AttackMonsterInDefenseMode(){
        var monster1Atk = _monster1.GetAttack();
        var monster2Def = _monster2.GetDefense();

        if(monster1Atk > monster2Def){
            yield return new WaitForSeconds(_monsterMoveWait);
            DestroyMonster(_monster2);
            _monster1.MoveCard(_monster1OriginalPosition);

        }else if(monster1Atk < monster2Def){
            var damage = monster2Def - monster1Atk;

            if(damage > 0){
                if(_monster1.IsPlayerCard()){
                    BattleManager.Instance.HealthManager.DamagePlayer(damage);
                }else{
                    BattleManager.Instance.HealthManager.DamageEnemy(damage);
                }
            }
        }
    }

    private IEnumerator AttackMonsterInAttackMode(){
        var monster1Atk = _monster1.GetAttack();
        var monster2Atk = _monster2.GetAttack();

        if(monster1Atk > monster2Atk){
            var damage = monster1Atk - monster2Atk;

            if(damage > 0){
                if(_monster1.IsPlayerCard()){
                    BattleManager.Instance.HealthManager.DamageEnemy(damage);
                }else{
                    BattleManager.Instance.HealthManager.DamagePlayer(damage);
                }
            }

            DestroyMonster(_monster2);
            yield return new WaitForSeconds(_monsterMoveWait);
            _monster1.MoveCard(_monster1OriginalPosition);

        }else if(monster2Atk > monster1Atk){
            var damage = monster2Atk - monster1Atk;

            if(damage > 0){
                if(_monster1.IsPlayerCard()){
                    BattleManager.Instance.HealthManager.DamagePlayer(damage);
                }else{
                    BattleManager.Instance.HealthManager.DamageEnemy(damage);
                }
            }

            DestroyMonster(_monster1);
            yield return new WaitForSeconds(_monsterMoveWait);
            _monster1.MoveCard(_monster2OriginalPosition);

        }else if(monster1Atk == monster2Atk){
            DestroyMonster(_monster1);
            DestroyMonster(_monster2);
        }
    }

    private void DestroyMonster(CardMonster monster){
        StartCoroutine(DestroyMonsterRoutine(monster));
    }

    private IEnumerator DestroyMonsterRoutine(CardMonster monster){
        monster.Shader.DissolveCard(Color.red);
        yield return new WaitForSeconds(0.9f);

        monster.DestroyCard();

        yield return new WaitForSeconds(1.2f);
        BattleManager.Instance.UIBattleManager.BringUI();
    }
}