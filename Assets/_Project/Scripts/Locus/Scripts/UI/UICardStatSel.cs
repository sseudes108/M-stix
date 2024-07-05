using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UICardStatSel : UIManager{

    public static Action OnOption1Clicked;
    public static Action OnOption2Clicked;

    public Button Option1, Option2;
    public VisualElement OptionsCanvas;


    private void OnEnable() {
        CardStatSelections.OnSelectAnother += CardStatSelections_OnSelectAnother;
        CardStatSelections.OnSelectionsEnd += CardStatSelections_OnSelectionsEnd;
    }

    private void OnDisable() {
        CardStatSelections.OnSelectAnother -= CardStatSelections_OnSelectAnother;
        CardStatSelections.OnSelectionsEnd -= CardStatSelections_OnSelectionsEnd;
    }


    private void CardStatSelections_OnSelectionsEnd(){
        Debug.Log("StatSelections - StatSelections_OnSelectionsEnd");
        HideOptions();
    }

    private void CardStatSelections_OnSelectAnother(Card card){
        // Debug.Log("StatSelections - StatSelections_OnSelectAnother");
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
        OptionsCanvas.style.display = DisplayStyle.Flex;
    }

    private void HideOptions(){
        Option1.clicked -= Option1_Clicked;
        Option2.clicked -= Option2_Clicked;
        OptionsCanvas.style.display = DisplayStyle.None;
    }

    private void Option1_Clicked(){
        OnOption1Clicked?.Invoke();
    }

    private void Option2_Clicked(){
        OnOption2Clicked?.Invoke();
    }

    private void SetElements(){
        Option1 = Root.Q<Button>("Option1");
        Option2 = Root.Q<Button>("Option2");
        OptionsCanvas = Root.Q("OptionsCanvas");

        Option1.clicked -= Option1_Clicked;
        Option2.clicked -= Option2_Clicked;

        Option1.clicked += Option1_Clicked;
        Option2.clicked += Option2_Clicked;
    }

    private void SetButtonText(Card card){
        SetElements();
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(!monsterCard.AnimaSelected){
                //Anima
                Option1.text = $"{monsterCard.FirstAnima}";
                Option2.text = $"{monsterCard.SecondAnima}";
            }else if(!monsterCard.ModeSelected){
                //Mode
                Option1.text = $"Attack";
                Option2.text = $"Deffense";
            }else if(!monsterCard.FusionedCard){
                //Face
                Option1.text = $"Face Up";
                Option2.text = $"Face Down";
            }
        }
    }
}
