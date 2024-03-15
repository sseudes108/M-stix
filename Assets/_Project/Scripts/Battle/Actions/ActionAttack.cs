using System.Collections;
using UnityEngine;

public class ActionAttack : MonoBehaviour {

    CardMonster _monster1, _monster2;
    Transform _monster1OriginalPosition, _monster2OriginalPosition;
    [SerializeField] private Transform _monsterPos1, _monsterPos2;
    BoardCardMonsterPlace _monsterPlace1, _monsterPlace2;

    private void OnEnable() {
        BoardCardMonsterPlace.OnAttack += BoardCardMonsterPlace_OnAttack;
    }

    private void OnDisable() {
        BoardCardMonsterPlace.OnAttack -= BoardCardMonsterPlace_OnAttack;
    }

    //Attacking Monster - Event trigged
    private void BoardCardMonsterPlace_OnAttack(BoardCardMonsterPlace place, CardMonster monster){
        _monster1 = null;
        _monster2 = null;
        _monster1OriginalPosition = null;
        _monster2OriginalPosition = null;
        _monsterPlace1 = null;
        _monsterPlace2 = null;

        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.AttackPhase);
        
        var oponentTargets = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces();

        //If the oponent field has no monsters
        if(oponentTargets.Count == 0){
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                oponentTargets = BattleManager.Instance.EnemyBoardPlaces.MonsterPlacements;
            }else{
                oponentTargets = BattleManager.Instance.PlayerBoardPlaces.MonsterPlacements;
            }
        }

        BattleManager.Instance.BoardPlaceVisuals.HighLightAttackTargetPlaces(oponentTargets);

        _monster1OriginalPosition = place.transform;
        _monster1 = monster;

        _monsterPlace1 = place;
    }

    //Attacked Monster - Clicked by the player
    public void StartMonsterBattle(BoardCardMonsterPlace place, CardMonster monster2){
        StartCoroutine(StartMonsterBattleRoutine(place, monster2));
    }

    private IEnumerator StartMonsterBattleRoutine(BoardCardMonsterPlace place, CardMonster monster2){
        _monster2OriginalPosition = place.transform;
        _monsterPlace2 = place;
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

#region Direct Attack
    //Direct Attack
    public void DirectAttack(){
        StartCoroutine(DirectAttackRoutine());
    }

    public IEnumerator DirectAttackRoutine(){
        _monster1.MoveCard(_monsterPos1);

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.UIBattleManager.ClearUI();

        yield return new WaitForSeconds(1.2f);

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.HealthManager.DamageEnemy(_monster1.GetAttack());
        }else{
            BattleManager.Instance.HealthManager.DamagePlayer(_monster1.GetAttack());
        }

        _monster1.SetMonsterAttacking(false);
        _monster1.MoveCard(_monster1OriginalPosition);

        yield return new WaitForSeconds(1.2f);
        BattleManager.Instance.UIBattleManager.BringUI();

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.BoardPlaceVisuals.LightUpEnemyMonsterPlaces();
        }else{
            BattleManager.Instance.BoardPlaceVisuals.LightUpPlayerMonsterPlaces();
        }
    }
#endregion

#region Monster x Monster
    private IEnumerator AttackMonsterInDefenseMode(){
        var monster1Atk = _monster1.GetAttack();
        var monster2Def = _monster2.GetDefense();

        if(monster1Atk > monster2Def){
            DestroyMonster(_monster2);
            SetPlaceFree(_monsterPlace2);
            yield return new WaitForSeconds(2.5f);
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
            SetPlaceFree(_monsterPlace2);
            yield return new WaitForSeconds(2.5f);
            _monster1.MoveCard(_monster1OriginalPosition);

            //Monstro2 mais forte
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
            SetPlaceFree(_monsterPlace1);
            yield return new WaitForSeconds(2.5f);
            _monster1.MoveCard(_monster2OriginalPosition);

        }else if(monster1Atk == monster2Atk){
            DestroyMonster(_monster1);
            DestroyMonster(_monster2);
            SetPlaceFree(_monsterPlace1);
            SetPlaceFree(_monsterPlace2);
        }
    }
#endregion

    private void DestroyMonster(CardMonster monster){
        StartCoroutine(DestroyMonsterRoutine(monster));
    }

    private IEnumerator DestroyMonsterRoutine(CardMonster monster){
        BattleManager.Instance.VFXManager.VFXLowDamageParticle(monster.transform);
        yield return new WaitForSeconds(0.5f);

        monster.Shader.DissolveCard(Color.red);
        yield return new WaitForSeconds(0.9f);

        monster.DestroyCard();

        yield return new WaitForSeconds(1.5f);
        BattleManager.Instance.UIBattleManager.BringUI();

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.BoardPlaceVisuals.LightUpEnemyMonsterPlaces();
        }else{
            BattleManager.Instance.BoardPlaceVisuals.LightUpPlayerMonsterPlaces();
        }
    }

    private void SetPlaceFree(BoardCardMonsterPlace place){
        place.SetPlaceFree();
    }

}

// public class ShotLogic : MonoBehaviour{
//     bool canShoot = true;
//     Bullet bulletPrefab;
//     Transform firePoint;
//     private void Shot(){
//         if(Input.GetMouseButtonDown(1) && canShoot){
//             canShoot = false;

//             madCalculations;

//             var spawnedBullet = bulletPrefab;
//             spawnedBullet.Init(madCalculationsRotation);
//             Instantiate(spawnedBullet, firePoint);
//         }
//     }
// }
// public class Bullet : MonoBehaviour{
//     float shotSpeed;
//     public void Init(Quaternion rot){
//         transform.rotation = rot;
//     }
//     private void Update() {
//         transform.position += Vector3.forward * shotSpeed * Time.deltaTime;
//     }
// }