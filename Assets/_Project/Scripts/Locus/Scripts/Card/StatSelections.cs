using System;
using UnityEngine;

public class StatSelections : MonoBehaviour {
    public static Action<Card> OnSelectAnother;
    private MonsterCard _monsterCard;
    private Card _resultCard;
    private bool _animaSelected;
    private bool _monsterModeSelected;
    private bool _faceSelected;
    private bool _isPlayerTurn;

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

    private void CardStatSelectPhase_OnStatSelectStart(Card card, bool IsPlayerTurn){
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
        if(!_resultCard.AnimaSelected){
            Debug.Log("Anima 1 Selected");
            _resultCard.SelectAnima();
            OnSelectAnother?.Invoke(_resultCard);
        }
    }

    public void Option2_Clicked(){
        Debug.Log("Option2_Clicked()");
    }
}
