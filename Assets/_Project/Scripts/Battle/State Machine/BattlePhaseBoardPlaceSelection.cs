using System.Collections;
using UnityEngine;

public class BattlePhaseBoardPlaceSelection : BattleAbstract{
    Card _resultCard;
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.BoardPlaceSelection);

        _resultCard = BattleManager.Instance.FusionManager.GetResultCard();

        BattleManager.Instance.BoardPlaceManager.DisableOnBoardCardColliders();

        //Board material color change
        BattleManager.Instance.BoardPlaceVisuals.BoarderSelectionPhaseHighlight(_resultCard, 3f);

        //Move result card to board place selection
        BattleManager.Instance.FusionPositions.MoveCardToBoardPlaceSelectionPos(_resultCard);

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.AIManager.BoardPlaceSelector.StartBoardPlaceSelection(_resultCard);
        }
    }

    public override void ExitState(){
        // BattleManager.Instance.BoardPlaceManager.EnableOnBoardCardColliders();
        
        //Board material reset color
        BattleManager.Instance.BoardPlaceVisuals.ResetPlaceHighlightColor(1.5f);
        BattleManager.Instance.CardSelector.ClearSelectedlist();
    }

    public override void Update(){

    }
}