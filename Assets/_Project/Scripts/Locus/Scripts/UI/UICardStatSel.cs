using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UICardStatSel : UIManager{

    public static Action OnOption1Clicked;
    public static Action OnOption2Clicked;

    public Button Option1, Option2;
    public VisualElement OptionsCanvas;


    private void OnEnable() {
        Fusion.OnFusionEnd += Fusion_OnFusionEnd;
        Card.OnStatSelection += Card_OnStatSelection;
        StatSelections.OnSelectAnother += StatSelections_OnSelectAnother;
    }

    private void OnDisable() {
        Fusion.OnFusionEnd -= Fusion_OnFusionEnd;
        Card.OnStatSelection -= Card_OnStatSelection;
        StatSelections.OnSelectAnother -= StatSelections_OnSelectAnother;
    }

    private void StatSelections_OnSelectAnother(Card card){
        SetButtonText(card);
    }

    private void Card_OnStatSelection(Card card){
        Debug.Log($"{card.Name}");
    }

    private void Fusion_OnFusionEnd(Card card){
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
            if(!card.AnimaSelected){
                Option1.text = $"{monsterCard.FirstAnima.ToString()}";
                Option2.text = $"{monsterCard.SecondAnima.ToString()}";
            }else{
                Option1.text = $"Outra Opção 1";
                Option2.text = $"Outra Opção 2";
            }
        }
    }
}
