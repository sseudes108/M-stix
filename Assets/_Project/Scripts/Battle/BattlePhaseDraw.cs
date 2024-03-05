using UnityEngine;

public class BattlePhaseDraw : BattleAbstract {
    public override void EnterState(){
        Debug.Log("EnterState BattlePhaseDraw");
        BattleManager.Instance.PlayerHand.DrawCards();
        BattleManager.Instance.EnemyHand.DrawCards();
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }
}