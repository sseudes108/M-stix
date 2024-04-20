using UnityEngine;

public class AIAttacker : MonoBehaviour {
    public void StartAttackSelection(){
        BattleManager.Instance.AIManager.CurrentArchetype.CheckAttack();
    }
}