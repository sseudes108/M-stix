using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FusionAfterSelections : MonoBehaviour {
    private CardMonster _monsterCard;
    private Card _resultCard;
    private bool _animaSelected;
    private bool _monsterModeSelected;
    private bool _faceSelected;

    public void StartSelection(Card resultCard){
        _resultCard = resultCard;
        BattleManager.Instance.BattleStateManager.StartCoroutine(SelectionRoutine());
    }
    
    private IEnumerator SelectionRoutine(){
        // var (button1, button2) = _resultCard.GetOptionButtons();

        if(BattleManager.Instance.TurnManager.IsPlayerTurn()){
            if(_resultCard is CardMonster){
                _monsterCard = _resultCard as CardMonster;

                //Anima
                yield return new WaitForSeconds(1f);
                AnimaSelection(_resultCard);
                do{
                    yield return null;
                }while(!_animaSelected);
                _resultCard.HideOptions();

                //Mode
                yield return new WaitForSeconds(1f);
                MonsterModeSelection(_resultCard);
                do{
                    yield return null;
                }while(!_monsterModeSelected);
                _resultCard.HideOptions();
            }

            if(!_resultCard.IsFusioned()){
                yield return new WaitForSeconds(1f);
                FaceSelection(_resultCard);
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
                var anima = BattleManager.Instance.AIManager.AfterFusionSelector.AnimaSelection();
                if(anima == 0){
                    Debug.Log("1 - Anima 1");
                    FirstAnimaSelected();
                }else{
                    Debug.Log("2 - Anima 2");
                    SecondAnimaSelected();
                }
                yield return new WaitForSeconds(0.3f);

                var monsterMode = BattleManager.Instance.AIManager.AfterFusionSelector.MonsterModeSelection();
                if(monsterMode == 0){
                    Debug.Log("1 - Attack Mode");
                    AttackModeSelected();
                }else{
                    Debug.Log("2 - Defense Mode");
                    DefenseModeSelected();
                }
                yield return new WaitForSeconds(0.3f);

                var face = BattleManager.Instance.AIManager.AfterFusionSelector.FaceSelection();
                if(face == 0){
                    Debug.Log("1 - FaceUp Mode");
                    FaceUpSelected();
                }else{
                    Debug.Log("2 - FaceDown Mode");
                    FaceDownSelected();
                }
                yield return new WaitForSeconds(0.3f);
            }
        }

        SelectionFinished();
    }

    //Anima
    private void AnimaSelection(Card _resultCard){
        var (button1, button2) = _resultCard.GetOptionButtons();
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
    private void MonsterModeSelection(Card _resultCard){
        var (button1, button2) = _resultCard.GetOptionButtons();
        _monsterModeSelected = false;
        _monsterCard.ShowMonsterModeOptions();

        button1.onClick.AddListener(AttackModeSelected);
        button2.onClick.AddListener(DefenseModeSelected);
    }
    private void AttackModeSelected(){
        _monsterCard.SetAttackMode(true);
        _monsterModeSelected = true;
    }
    private void DefenseModeSelected(){
        _monsterCard.SetAttackMode(false);
        _monsterModeSelected = true;
    }

    //Face
    private void FaceSelection(Card _resultCard){
        var (button1, button2) = _resultCard.GetOptionButtons();
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