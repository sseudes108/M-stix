using System.Collections;
using UnityEngine;

public class BattlePhaseFaceSelection : BattleAbstract{
    private Card _resultCard;

    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.FaceSelection);
        _resultCard = BattleManager.Instance.Fusion.GetResultCard();
        Wait();
    }

    public override void ExitState(){

    }

    public override void Update(){

    }

    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        Debug.Log("Waiting Face Select");
        yield return new WaitForSeconds(0.2f);

        var (button1, button2) = _resultCard.GetOptionButtons();
        _resultCard.ShowFaceOptions();

        button2.onClick.AddListener(FaceUpSelected);
        button2.onClick.AddListener(FaceDownSelected);
    }

    private void FaceUpSelected(){
        Debug.Log("Face Up Selected");
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
    }

    private void FaceDownSelected(){
        Debug.Log("Face Down Selected");
        _resultCard.SetCardFaceDown();
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
    }
}