using UnityEngine;

public class BattlePhaseDraw : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Draw);
        Debug.Log(_currentTurn);
        Debug.Log(BattleManager.Instance.TurnManager.GetTurn());
        //First Turn
        if(BattleManager.Instance.TurnManager.GetTurn() == 1){
            BattleManager.Instance.PlayerHand.DrawCards();
            BattleManager.Instance.EnemyHand.DrawCards();

        }else if(_playerTurn){
            //player turn
            BattleManager.Instance.PlayerHand.DrawCards();
        }else{
            //enemy turn
            BattleManager.Instance.EnemyHand.DrawCards();
        }
    }

    public override void ExitState(){
        BattleManager.Instance.EnemyHand.GetCardsInHand();
    }

    public override void Update(){
        
    }
}