using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhaseSelections : BattleAbstract{
    Card _resultCard;
    private bool _animaSelected;
    private bool _monsterModeSelected;
    private bool _faceSelected;

    public override void EnterState(){
        BattleManager.Instance.BattleStateManager.SetBattlePhase(EStateMachinePhase.Selections);

        _animaSelected = false;
        _monsterModeSelected = false;
        _faceSelected = false;

        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Wait();
        }else{
            _resultCard = BattleManager.Instance.Fusion.GetResultCard();
            StartSelection();
        }

        // _resultCard = BattleManager.Instance.Fusion.GetResultCard();
        // StartSelection();
    }

    public override void ExitState(){

    }

    public override void Update(){

    }

    public void StartSelection(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(SelectionRoutine());
    }

    private IEnumerator SelectionRoutine(){
        var (button1, button2) = _resultCard.GetOptionButtons();

        if(_resultCard is CardMonster){
            //Anima
            yield return new WaitForSeconds(1f);
            AnimaSelection(button1, button2);
            do{
                yield return null;
            }while(!_animaSelected);
            _resultCard.HideOptions();

            //Mode
            yield return new WaitForSeconds(1f);
            MonsterModeSelection(button1, button2);
            do{
                yield return null;
            }while(!_monsterModeSelected);
            _resultCard.HideOptions();
        }
        
        if(!_resultCard.IsFusioned()){
            yield return new WaitForSeconds(1f);
            FaceSelection(button1, button2);
            do{
                yield return null;
            }while(!_faceSelected);
            _resultCard.HideOptions();
        }

        SelectionFinished();
    }

    //Anima
    private void AnimaSelection(Button button1, Button button2){
        _resultCard.GetComponent<CardMonster>().ShowAnimaOptions();

        button1.onClick.AddListener(FirstAnimaSelected);
        button2.onClick.AddListener(SecondAnimaSelected);
    }
    private void FirstAnimaSelected(){
        if(_animaSelected){return;}
        _resultCard.GetComponent<CardMonster>().SetAnima(_resultCard.GetComponent<CardMonster>().GetAnimas()[0]);
        _resultCard.Shader.SetSelectedAnimaShader(1, _resultCard.GetComponent<CardMonster>().GetAnimas()[0]);
        _animaSelected = true;
    }
    private void SecondAnimaSelected(){
        if(_animaSelected){return;}
        _resultCard.GetComponent<CardMonster>().SetAnima(_resultCard.GetComponent<CardMonster>().GetAnimas()[1]);
        _resultCard.Shader.SetSelectedAnimaShader(2, _resultCard.GetComponent<CardMonster>().GetAnimas()[1]);
        _animaSelected = true;
    }

    //Monster Mode
    private void MonsterModeSelection(Button button1, Button button2){
        _resultCard.GetComponent<CardMonster>().ShowMonsterModeOptions();

        button1.onClick.AddListener(AttackModeSelected);
        button2.onClick.AddListener(DefenseModeSelected);
    }
    private void AttackModeSelected(){
        if(_monsterModeSelected){return;}
        _resultCard.GetComponent<CardMonster>().SetAttackMode(true);
        _monsterModeSelected = true;
    }
    private void DefenseModeSelected(){
        if(_monsterModeSelected){return;}
        _resultCard.GetComponent<CardMonster>().SetAttackMode(false);
        _monsterModeSelected = true;
    }

    //Face
    private void FaceSelection(Button button1, Button button2){        
        _resultCard.ShowFaceOptions();

        button1.onClick.AddListener(FaceUpSelected);
        button2.onClick.AddListener(FaceDownSelected);
    }

    private void FaceUpSelected(){        
        SelectionFinished();
        _faceSelected = true;
    }

    private void FaceDownSelected(){
        _resultCard.SetCardFaceDown();
        _faceSelected = true;
    }

    private void SelectionFinished(){
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
    }



    public void Wait(){
        BattleManager.Instance.BattleStateManager.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine(){
        if(!BattleManager.Instance.TurnManager.IsPlayerTurn()){
            Debug.Log("Waiting Selection - Enemy");
        }
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
    }
}