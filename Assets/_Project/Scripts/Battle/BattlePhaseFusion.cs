using UnityEngine;

public class BattlePhaseFusion : BattleAbstract {
   public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Fusion);

        //Move hand off camera
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.PlayerHand.MoveHand(BattleManager.Instance.FusionPositions.HandOffCameraPosition.position);
        }

        var selectedCards = BattleManager.Instance.CardSelector.GetSelectedCards();
        
        foreach(var card in selectedCards){
            card.SetCardOnHand(false);
        }

        BattleManager.Instance.Fusion.StartFusionRoutine(selectedCards);
    }

    public override void ExitState(){

    }

    public override void Update(){
        
    }
}