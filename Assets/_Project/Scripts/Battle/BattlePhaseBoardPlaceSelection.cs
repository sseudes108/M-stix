using System.Collections;
using UnityEngine;

public class BattlePhaseBoardPlaceSelection : BattleAbstract{
    Card _resultCard;
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);

        _resultCard = BattleManager.Instance.Fusion.GetResultCard();

        BattleManager.Instance.BoardPlaceManager.DisableOnBoardCardColliders();

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }else{
            //Board material color change
            BattleManager.Instance.BoardPlaceVisuals.BoarderSelectionPhaseHighlight(_resultCard, 3f);

            //Move result card to board place selection
            BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(_resultCard);
        }
    }

    public override void ExitState(){
        BattleManager.Instance.BoardPlaceManager.EnableOnBoardCardColliders();
        
        //Board material reset color
        BattleManager.Instance.BoardPlaceVisuals.ResetPlaceHighlightColor(1.5f);
        
        BattleManager.Instance.CardSelector.ClearSelectedlist();
    }

    public override void Update(){

    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Debug.Log("Waiting Selection - Enemy");
        }
        yield return new WaitForSeconds(_waitTime);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.ActionPhase);
    }
}