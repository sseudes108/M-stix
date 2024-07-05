using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UICardStatSel : UIManager{

    public static Action OnOption1Clicked;
    public static Action OnOption2Clicked;

    public Button Option1, Option2;
    public VisualElement OptionsCanvas;


    private void OnEnable() {
        // Fusion.OnFusionEnd += Fusion_OnFusionEnd;
        // Card.OnStatSelection += Card_OnStatSelection;
        // CardStatSelectPhase.OnStatSelectStart += CardStatSelectPhase_OnStatSelectStart;
        StatSelections.OnSelectAnother += StatSelections_OnSelectAnother;
        StatSelections.OnSelectionsEnd += StatSelections_OnSelectionsEnd;
    }

    private void OnDisable() {
        // Fusion.OnFusionEnd -= Fusion_OnFusionEnd;
        // Card.OnStatSelection -= Card_OnStatSelection;
        // CardStatSelectPhase.OnStatSelectStart -= CardStatSelectPhase_OnStatSelectStart;
        StatSelections.OnSelectAnother -= StatSelections_OnSelectAnother;
        StatSelections.OnSelectionsEnd -= StatSelections_OnSelectionsEnd;
    }

    // private void CardStatSelectPhase_OnStatSelectStart(Card card){
    //     Debug.Log("UICardStatSel - CardStatSelectPhase_OnStatSelectStart");
    //     ShowOptions();
    //     SetButtonText(card);
    // }

    private void StatSelections_OnSelectionsEnd(){
        Debug.Log("StatSelections - StatSelections_OnSelectionsEnd");
        HideOptions();
    }

    private void StatSelections_OnSelectAnother(Card card){
        Debug.Log("StatSelections - StatSelections_OnSelectAnother");
        SetButtonText(card);
    }

    // private void Card_OnStatSelection(Card card){
    //     Debug.Log($"{card.Name}");
    // }

    // private void Fusion_OnFusionEnd(Card card){
    //     Debug.Log("UICardStatSel - Fusion_OnFusionEnd");
    //     if(card != null){
    //         ShowOptions();
    //         SetButtonText(card);
    //     }
    // }

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
