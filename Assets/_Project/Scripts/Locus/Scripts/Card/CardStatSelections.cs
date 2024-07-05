using System;
using UnityEngine;

public class CardStatSelections : MonoBehaviour {
    public static Action<Card> OnSelectAnother;
    public static Action OnSelectionsEnd;
    public MonsterCard _monsterCard;
    public Card _resultCard;

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
            var monster = _resultCard as MonsterCard;
            if(monster.FusionedCard){
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima note selected
                    monster.SelectAnima();
                    OnSelectAnother?.Invoke(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    OnSelectionsEnd?.Invoke();
                }
            }else{
                // normal card
                if(!monster.AnimaSelected){ // Anima note selected
                    monster.SelectAnima();
                    OnSelectAnother?.Invoke(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    OnSelectAnother?.Invoke(monster);
                }else if(!monster.FaceSelected){ //Anima selected, Mode seletec and Face not selected
                    monster.SelectFace();
                    OnSelectionsEnd?.Invoke();
                }
            }
        }
    }

    public void Option2_Clicked(){
        if(_resultCard is MonsterCard){
            var monster = _resultCard as MonsterCard;
            if(monster.FusionedCard){
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima note selected
                    monster.SelectAnima();
                    OnSelectAnother?.Invoke(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    OnSelectionsEnd?.Invoke();
                }
            }else{
                // normal card
                if(!monster.AnimaSelected){ // Anima note selected
                    monster.SelectAnima();
                    OnSelectAnother?.Invoke(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    OnSelectAnother?.Invoke(monster);
                }else if(!monster.FaceSelected){ //Anima selected, Mode seletec and Face not selected
                    monster.SelectFace();
                    OnSelectionsEnd?.Invoke();
                }
            }
        }
    }
}
