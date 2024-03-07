using System.Collections;
using UnityEngine;

public class BattlePhaseFusion : BattleAbstract {
   public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Fusion);

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }else{
            var selectedCards = BattleManager.Instance.CardSelector.GetSelectedCards();
            
            foreach(var card in selectedCards){
                card.SetCardOnHand(false);
            }

            //Move hand off camera
            if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
                BattleManager.Instance.PlayerHand.MoveHand(BattleManager.Instance.FusionPositions.HandOffCameraPosition.position);
            }

            BattleManager.Instance.Fusion.StartFusionRoutine(selectedCards);
        }

        // //Move hand off camera
        // if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
        //     BattleManager.Instance.PlayerHand.MoveHand(BattleManager.Instance.FusionPositions.HandOffCameraPosition.position);
        // }

        // var selectedCards = BattleManager.Instance.CardSelector.GetSelectedCards();
        
        // foreach(var card in selectedCards){
        //     card.SetCardOnHand(false);
        // }
        
        // BattleManager.Instance.Fusion.StartFusionRoutine(selectedCards);
    }

    public override void ExitState(){

    }

    public override void Update(){
        
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Debug.Log("Waiting Fusion - Enemy");
        }
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.SelectionsPhase);
    }
}