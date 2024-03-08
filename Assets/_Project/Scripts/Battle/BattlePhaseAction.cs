using System.Collections;
using UnityEngine;

public class BattlePhaseAction : BattleAbstract {
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Action);

        var lastCardPlaced = BattleManager.Instance.BoardPlaceManager.GetLastPlacedCard();
        if(lastCardPlaced is CardArcane){
            var arcaneCard = lastCardPlaced as CardArcane;
            if(!arcaneCard.IsFaceDown()){
                var effectType = arcaneCard.GetArcaneType();
                BattleManager.Instance.CardEffect.StartEffectRoutine(arcaneCard, effectType);
            }
        }

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }else{
            //
        }
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
            // Debug.Log("Waiting Action - Enemy");
        }
        yield return new WaitForSeconds(_waitTime);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.EndPhase);
    }
}