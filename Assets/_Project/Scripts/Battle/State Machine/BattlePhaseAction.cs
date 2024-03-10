using System.Collections;
using UnityEngine;

public class BattlePhaseAction : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Action);
        BattleManager.Instance.UIBattleManager.EndPhaseButton();
        
        var lastCardPlaced = BattleManager.Instance.BoardPlaceManager.GetLastPlacedCard();
        if(lastCardPlaced is CardArcane){
            var arcaneCard = lastCardPlaced as CardArcane;
            if(!lastCardPlaced.IsFaceDown()){
                BattleManager.Instance.CardEffect.ActiveCardEffect(arcaneCard);
            }
        }

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }else{
            //
        }
    }

    public override void ExitState(){
        // BattleManager.Instance.BoardPlaceManager.EnableOnBoardCardColliders();
    }

    public override void Update(){
        
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            // Debug.Log("Waiting Action - Enemy");
        }
        yield return new WaitForSeconds(_waitTime);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.EndPhase);
    }
}