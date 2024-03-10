using System.Collections;
using UnityEngine;

public class ActionAttack : MonoBehaviour {

    CardMonster _monster1, _monster2;

    private void OnEnable() {
        BoardCardMonsterPlace.OnAttack += BoardCardMonsterPlace_OnAttack;
    }

    private void OnDisable() {
        BoardCardMonsterPlace.OnAttack -= BoardCardMonsterPlace_OnAttack;
    }

    private void BoardCardMonsterPlace_OnAttack(BoardCardMonsterPlace place, CardMonster monster){
        _monster1 = null;
        _monster2 = null;

        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.AttackPhase);
        
        var oponentTargets = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces();
        BattleManager.Instance.BoardPlaceVisuals.HighLightAttackTargetPlaces(oponentTargets);
        
        _monster1 = monster;
    }

    public void StartMonsterBattle(CardMonster monster2){
        //Make animation
        _monster2 = monster2;

        if(_monster2.IsInAttackMode()){
            AttackMonsterInAttackMode();
        }else{
            AttackMonsterInDefenseMode();
        }

        _monster1.SetMonsterAttacking(false);
    }

    private void AttackMonsterInDefenseMode(){
        var monster1Atk = _monster1.GetAttack();
        var monster2Def = _monster2.GetDefense();

        if(monster1Atk > monster2Def){
            DestroyMonster(_monster2);
        }else if(monster1Atk < monster2Def){
            var damage = monster2Def - monster1Atk;

            if(damage > 0){
                if(_monster1.IsPlayerCard()){
                    BattleManager.Instance.HealthManager.DamagePlayer(damage);
                }else{
                    BattleManager.Instance.HealthManager.DamageEnemy(damage);
                }
            }
        }else if(monster1Atk == monster2Def){

        }
    }

    private void AttackMonsterInAttackMode(){
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
    }
}