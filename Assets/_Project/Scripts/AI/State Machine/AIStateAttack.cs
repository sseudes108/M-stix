using UnityEngine;

public class AIStateAttack : AIAbstract
{
    public override void EnterState(){
        Debug.Log("AI Attack State");
        BattleManager.Instance.AIManager.AIAttacker.StartAttackSelection();
    }

    public override void ExitState(){
        
    }

    public override void UpdateState(){
        
    }
}