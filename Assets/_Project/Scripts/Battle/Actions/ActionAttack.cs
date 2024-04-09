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
        bool isPlayerTurn = BattleManager.Instance.TurnManager.IsPlayerTurn();

        yield return new WaitForSeconds(0.5f);

        BattleManager.Instance.UIBattleManager.ClearUI();

        yield return new WaitForSeconds(1.2f);

        //Particle Effect
        int damage = _monster1.GetAttack();
        Transform position;
        if (isPlayerTurn){
            position = _monsterPos2;
        }else{
            position = _monsterPos1;
        }

        //Damage Health
        if(isPlayerTurn){
            BattleManager.Instance.HealthManager.DamageEnemy(damage);
        }else{
            BattleManager.Instance.HealthManager.DamagePlayer(damage);
        }

        ParticleEffect(position, damage, out float timeBringUI);

        yield return new WaitForSeconds(timeBringUI + 0.7f);

        BattleManager.Instance.UIBattleManager.BringUI();
        _monster1.SetMonsterAttacking(false);
        _monster1.MoveCard(_monster1OriginalPosition);

        if(isPlayerTurn){
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
            DestroyMonster(_monster2, 0);
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

                ParticleEffect(_monster1.transform.transform, damage, out float timeBringUI);
                yield return new WaitForSeconds(timeBringUI);

                _monster1.MoveCard(_monster1OriginalPosition);
                _monster2.MoveCard(_monster2OriginalPosition);
                _monster2.RotateCard(BattleManager.Instance.BoardPlaceManager.DefenseFaceUpRotation(_monster2));

                yield return new WaitForSeconds(0.5f);
                BattleManager.Instance.UIBattleManager.BringUI();
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

            DestroyMonster(_monster2, damage);
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

            DestroyMonster(_monster1, damage);
            SetPlaceFree(_monsterPlace1);
            yield return new WaitForSeconds(2.5f);
            _monster2.MoveCard(_monster2OriginalPosition);

        }else if(monster1Atk == monster2Atk){
            DestroyMonster(_monster1, 0);
            DestroyMonster(_monster2, 0);
            SetPlaceFree(_monsterPlace1);
            SetPlaceFree(_monsterPlace2);
        }
    }
#endregion

    private void DestroyMonster(CardMonster monster, int damage){
        StartCoroutine(DestroyMonsterRoutine(monster, damage));
    }

    private IEnumerator DestroyMonsterRoutine(CardMonster monster, int damage){
        ParticleEffect(monster.transform, damage, out float timeBringUI);

        yield return new WaitForSeconds(0.5f);

        monster.Shader.DissolveCard(Color.red);
        yield return new WaitForSeconds(0.9f);

        monster.DestroyCard();

        yield return new WaitForSeconds(timeBringUI);
        BattleManager.Instance.UIBattleManager.BringUI();

        if (BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.BoardPlaceVisuals.LightUpEnemyMonsterPlaces();
        }
        else{
            BattleManager.Instance.BoardPlaceVisuals.LightUpPlayerMonsterPlaces();
        }
    }

    private static void ParticleEffect(Transform monster, int damage, out float timeBringUI){
        if (damage < 2700){
            timeBringUI = 1.5f;
            BattleManager.Instance.VFXManager.VFXLowDamageParticle(monster.transform);
        }
        else if (damage >= 2700 && damage < 7200){
            timeBringUI = 2f;
            BattleManager.Instance.VFXManager.VFXMediumDamageParticle(monster.transform);
        }
        else{
            timeBringUI = 2f;
            //high damage particle
        }
    }

    private void SetPlaceFree(BoardCardMonsterPlace place){
        place.SetPlaceFree();
    }
}