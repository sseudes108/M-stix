using System.Collections;
using UnityEngine;

public class BattlePhaseStart : BattleAbstract{
    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Start);
        
        if(BattleManager.Instance.TurnManager.GetTurn() == 1){
            StartBattle();
        }else{
            Wait();
        }
    }

    public override void Update(){}
    

    public override void ExitState(){
        
    }
    public void StartBattle(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(InitializationRoutine());
    }

    private IEnumerator InitializationRoutine(){
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BoardPlaceVisuals.LightUpAllPlaces();
        
        yield return new WaitForSeconds(0.8f);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.DrawPhase);
    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            BattleManager.Instance.PlayerHand.MoveHand(BattleManager.Instance.FusionPositions.HandDefaultPosition.position);
        }else{
            // Debug.Log("Waiting Start - Enemy");
        }

        yield return new WaitForSeconds(_waitTime);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.DrawPhase);
    }
}
