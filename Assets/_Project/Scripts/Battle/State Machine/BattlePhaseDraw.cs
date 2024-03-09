using System.Collections;
using UnityEngine;

public class BattlePhaseDraw : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Draw);

        //First Turn
        if(BattleManager.Instance.TurnManager.GetTurn() == 1){
            BattleManager.Instance.PlayerHand.DrawCards();
            BattleManager.Instance.EnemyHand.DrawCards();

        }else if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            //player turn
            BattleManager.Instance.PlayerHand.DrawCards();
        }else{
            //enemy turn
            BattleManager.Instance.EnemyHand.DrawCards();
        }
    }

    public override void ExitState(){
        
    }

    public override void Update(){
        
    }
}