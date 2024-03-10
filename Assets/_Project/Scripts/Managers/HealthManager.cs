using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    
    [SerializeField] private int _maxHP;
    private int _playerHP;
    private int _enemyHP;

    public void StartFillHPRoutine(){
        StartCoroutine(FillHP());
    }

    private IEnumerator FillHP(){
        _playerHP = 0;
        _enemyHP = 0;
        do{
            _playerHP += 100;
            _enemyHP += 100;
            yield return new WaitForSeconds(0.03f);
            BattleManager.Instance.UIBattleManager.UpdatePlayerHealth(_playerHP);
            BattleManager.Instance.UIBattleManager.UpdateEnemyHealth(_enemyHP);
        }while(_playerHP < _maxHP);
    }

    //Player
    public void DamagePlayer(int amount){
        StartCoroutine(DamagePlayerRoutine(amount));
    }

    public IEnumerator DamagePlayerRoutine(int amount){
        var targetHP = _playerHP - amount;
        do{
            _playerHP -= 100;
            yield return new WaitForSeconds(0.03f);
            BattleManager.Instance.UIBattleManager.UpdatePlayerHealth(_playerHP);
        }while(_playerHP > targetHP);
    }

    public void HealPlayer(int amount){
        StartCoroutine(HealPlayerRoutine(amount));
    }
    
    public IEnumerator HealPlayerRoutine(int amount){
        var targetHP = _playerHP + amount;
        do{
            _playerHP -= 100;
            yield return new WaitForSeconds(0.03f);
            BattleManager.Instance.UIBattleManager.UpdatePlayerHealth(_playerHP);
        }while(_playerHP < targetHP);
    }

    //Enemy
    public void DamageEnemy(int amount){
        StartCoroutine(DamagePlayerRoutine(amount));
    }

    public IEnumerator DamageEnemyRoutine(int amount){
        var targetHP = _enemyHP - amount;
        do{
            _enemyHP -= 100;
            yield return new WaitForSeconds(0.03f);
            BattleManager.Instance.UIBattleManager.UpdateEnemyHealth(_enemyHP);
        }while(_enemyHP > targetHP);
    }
    
    public void HealEnemy(int amount){
        StartCoroutine(HealPlayerRoutine(amount));
    }
    
    public IEnumerator HealEnemyRoutine(int amount){
        var targetHP = _enemyHP + amount;
        do{
            _enemyHP -= 100;
            yield return new WaitForSeconds(0.03f);
            BattleManager.Instance.UIBattleManager.UpdateEnemyHealth(_enemyHP);
        }while(_enemyHP < targetHP);
    }

    public int GetPlayerHP() {return _playerHP;}
    public int GetEnemyHP() {return _enemyHP;}
}