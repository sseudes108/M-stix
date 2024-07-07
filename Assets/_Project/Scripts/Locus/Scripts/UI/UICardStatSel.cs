using System;
using System.Diagnostics;
using UnityEngine.UIElements;

public class UICardStatSel : UIManager{

    public static Action OnOption1Clicked;
    public static Action OnOption2Clicked;

    private Button _option1, _option2;
    private VisualElement _optionsCanvas;


    private void OnEnable() {
        CardStatSelections.OnSelectAnother += CardStatSelections_OnSelectAnother;
        CardStatSelections.OnSelectionsEnd += CardStatSelections_OnSelectionsEnd;
    }

    private void OnDisable() {
        CardStatSelections.OnSelectAnother -= CardStatSelections_OnSelectAnother;
        CardStatSelections.OnSelectionsEnd -= CardStatSelections_OnSelectionsEnd;
    }


    private void CardStatSelections_OnSelectionsEnd(){
        HideOptions();
    }

    private void CardStatSelections_OnSelectAnother(Card card){
        SetButtonText(card);
    }

    public void FusionEnded(Card card){
        ShowOptions();
        SetButtonText(card);
    }

    public override void Awake() {
        base.Awake();
        SetElements();
    }

    private void Start() {
        HideOptions();
    }

    private void ShowOptions(){
        _optionsCanvas.style.display = DisplayStyle.Flex;
    }

    private void HideOptions(){
        _option1.clicked -= Option1_Clicked;
        _option2.clicked -= Option2_Clicked;
        _optionsCanvas.style.display = DisplayStyle.None;
    }

    private void Option1_Clicked(){
        OnOption1Clicked?.Invoke();
    }

    private void Option2_Clicked(){
        OnOption2Clicked?.Invoke();
    }

    private void SetElements(){
        _option1 = Root.Q<Button>("Option1");
        _option2 = Root.Q<Button>("Option2");
        _optionsCanvas = Root.Q("OptionsCanvas");

        _option1.clicked -= Option1_Clicked;
        _option2.clicked -= Option2_Clicked;

        _option1.clicked += Option1_Clicked;
        _option2.clicked += Option2_Clicked;
    }

    private void SetButtonText(Card card){
        SetElements();
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(!monsterCard.AnimaSelected){
                //Anima
                _option1.text = $"{monsterCard.FirstAnima}";
                _option2.text = $"{monsterCard.SecondAnima}";
            }else if(!monsterCard.ModeSelected){
                //Mode
                _option1.text = $"Attack";
                _option2.text = $"Deffense";
            }else if(!monsterCard.FusionedCard){
                //Face
                _option1.text = $"Face Up";
                _option2.text = $"Face Down";
            }
        }
    }
}
