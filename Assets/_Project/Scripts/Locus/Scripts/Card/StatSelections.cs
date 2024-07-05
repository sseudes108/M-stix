using System;
using UnityEngine;

public class StatSelections : MonoBehaviour {
    public static Action<Card> OnSelectAnother;
    public static Action OnSelectionsEnd;
    private MonsterCard _monsterCard;
    private Card _resultCard;

    private void OnEnable() {
        CardStatSelectPhase.OnStatSelectStart += CardStatSelectPhase_OnStatSelectStart;
        UICardStatSel.OnOption1Clicked += UICardStatSel_OnOption1Clicked;
        UICardStatSel.OnOption2Clicked += UICardStatSel_OnOption2Clicked;
    }

    private void OnDisable() {
        CardStatSelectPhase.OnStatSelectStart -= CardStatSelectPhase_OnStatSelectStart;
        UICardStatSel.OnOption1Clicked -= UICardStatSel_OnOption1Clicked;
        UICardStatSel.OnOption2Clicked -= UICardStatSel_OnOption2Clicked;
    }

    private void UICardStatSel_OnOption1Clicked(){
        Option1_Clicked();
    }
    private void UICardStatSel_OnOption2Clicked(){
        Option2_Clicked();
    }

    private void CardStatSelectPhase_OnStatSelectStart(Card card){
        StartSelection(card);
    }

    public void StartSelection(Card card){
        _resultCard = null;
        _resultCard = card;
        if(_resultCard is MonsterCard){
            _monsterCard = _resultCard as MonsterCard;
        }
    }

    public void Option1_Clicked(){
        if(_resultCard is MonsterCard){
            if(!_monsterCard.AnimaSelected){
                Debug.Log("Anima 1 Selected");
                _monsterCard.SelectAnima();
                OnSelectAnother?.Invoke(_resultCard);
            }else if(!_monsterCard.ModeSelected){
                Debug.Log("Face 1 Selected");
                _monsterCard.SelectMode();
                if(!_monsterCard.FusionedCard){
                    OnSelectAnother?.Invoke(_resultCard);
                }else{
                    Debug.Log("OnSelectionsEnd");
                    OnSelectionsEnd?.Invoke();
                }
            }
        }
    }

    public void Option2_Clicked(){
        Debug.Log("Option2_Clicked()");
        if(_resultCard is MonsterCard){
            if(!_monsterCard.AnimaSelected){
                Debug.Log("Anima 2 Selected");
                _monsterCard.SelectAnima();
                OnSelectAnother?.Invoke(_resultCard);
            }else if(!_monsterCard.ModeSelected){
                Debug.Log("Face 2 Selected");
                _monsterCard.SelectMode();
                if(!_monsterCard.FusionedCard){
                    OnSelectAnother?.Invoke(_resultCard);
                }else{
                    OnSelectionsEnd?.Invoke();
                }
            }
        }
    }
}
