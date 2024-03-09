using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AfterFusionSelections : MonoBehaviour {
    private CardMonster _monsterCard;
    private Card _resultCard;
    private bool _animaSelected;
    private bool _monsterModeSelected;
    private bool _faceSelected;

    public void StartSelection(){
        //reset result Card
        _resultCard = null;
        _resultCard = BattleManager.Instance.FusionManager.GetResultCard();
        if(_resultCard is CardMonster){
            _monsterCard = _resultCard as CardMonster;
        }

        BattleManager.Instance.BattleStateManager.StartCoroutine(SelectionRoutine());
    }
    
    private IEnumerator SelectionRoutine(){
        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){

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
            }else{
                //make fusioned card always side up
                _resultCard.SetCardFaceUp();
            }

        }else{

            if(_resultCard is CardMonster){
                //Anima            
                if(BattleManager.Instance.AIManager.AfterFusionSelector.AnimaSelection() == 0){
                    FirstAnimaSelected();
                }else{
                    SecondAnimaSelected();
                }

                //Monster Mode
                if(BattleManager.Instance.AIManager.AfterFusionSelector.MonsterModeSelection() == 0){
                    AttackModeSelected();
                }else{
                    DefenseModeSelected();
                }
            }

            //Face
                //Not Fusioned Card
            if(!_resultCard.IsFusioned()){
                if(BattleManager.Instance.AIManager.AfterFusionSelector.FaceSelection() == 0){
                    FaceUpSelected();
                }else{

                    FaceDownSelected();
                }
            }else{
                _resultCard.SetCardFaceUp();
            }
        }
        
        SelectionFinished();
    }

    //Anima
    private void AnimaSelection(Button button1, Button button2){
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        _animaSelected = false;
        _monsterCard.ShowAnimaOptions();

        button1.onClick.AddListener(FirstAnimaSelected);
        button2.onClick.AddListener(SecondAnimaSelected);
    }

    private void FirstAnimaSelected(){
        var anima = _monsterCard.GetAnimas()[0];
        _monsterCard.SetAnima(anima);
        _resultCard.Shader.SetSelectedAnimaShader(1,anima);
        _animaSelected = true;
    }
    
    private void SecondAnimaSelected(){
        var anima = _monsterCard.GetAnimas()[1];
        _monsterCard.SetAnima(anima);
        _resultCard.Shader.SetSelectedAnimaShader(2, anima);
        _animaSelected = true;
    }

    //Monster Mode
    private void MonsterModeSelection(Button button1, Button button2){
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        _monsterModeSelected = false;
        _monsterCard.ShowMonsterModeOptions();

        button1.onClick.AddListener(AttackModeSelected);
        button2.onClick.AddListener(DefenseModeSelected);
    }

    private void AttackModeSelected(){
        _monsterCard.SetAttackMode();
        _monsterModeSelected = true;
    }
    private void DefenseModeSelected(){
        _monsterCard.SetDefenseMode();
        _monsterModeSelected = true;
    }

    //Face
    private void FaceSelection(Button button1, Button button2){
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        _faceSelected = false;       
        _resultCard.ShowFaceOptions();

        button1.onClick.AddListener(FaceUpSelected);
        button2.onClick.AddListener(FaceDownSelected);
    }

    private void FaceUpSelected(){
        _faceSelected = true;
    }

    private void FaceDownSelected(){
        _resultCard.SetCardFaceDown();
        _faceSelected = true;
    }

    private void SelectionFinished(){
        BattleManager.Instance.BattleStateManager.ChangeState(BattleManager.Instance.BoardPlaceSelectionPhase);
    }
}