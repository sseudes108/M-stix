using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    
    [SerializeField] private int _maxHP;
    private int _playerHP;
    private int _enemyHP;

    private void Start() {
        StartFillHPRoutine();
    }

    private void StartFillHPRoutine(){
        StartCoroutine(FillHP());
    }

    private IEnumerator FillHP(){
        _playerHP = 0;
        _enemyHP = 0;
        do{
            _playerHP += 100;
            _enemyHP += 100;
            yield return new WaitForSeconds(0.03f);
            BattleManager.Instance.UIBattleManager.UpdateHealth(_playerHP, _enemyHP);
        }while(_playerHP < _maxHP);
    }

    public void DamagePlayer(int amount){
        _playerHP -= amount;
        if(_playerHP <= 0){
            _playerHP = 0;
            Debug.Log("Enemy Win");
        }
        UpdateUI();
    }
    public void HealPlayer(int amount){
        _playerHP += amount;
        if(_playerHP > _maxHP){
            _playerHP = _maxHP;
        }
        UpdateUI();
    }

    public void DamageEnemy(int amount){
        _enemyHP -= amount;
        if(_enemyHP <= 0){
            _enemyHP = 0;
            Debug.Log("Player Win");
        }
        UpdateUI();
    }
    public void HealEnemy(int amount){
        _enemyHP += amount;
        if(_enemyHP > _maxHP){
            _enemyHP = _maxHP;
        }
        UpdateUI();
    }

    public int GetPlayerHP() {return _playerHP;}
    public int GetEnemyHP() {return _enemyHP;}

    private void UpdateUI(){
        BattleManager.Instance.UIBattleManager.UpdateHealth(_playerHP, _enemyHP);
    }
}