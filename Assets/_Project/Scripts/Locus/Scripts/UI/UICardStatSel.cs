using UnityEngine;
using UnityEngine.UIElements;

public class UICardStatSel : UIManager{
    public BattleManagerSO BattleManager;
    public FusionManager FusionManager;
    public CardStatEventHandlerSO CardStatSelManager;

    private Button _option1, _option2;
    private VisualElement _optionsCanvas;

    Card ResultCard;

    private void OnEnable() {
        CardStatSelManager.OnSelectAnother.AddListener(CardStatSelManager_OnSelectAnother);
        CardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);
        FusionManager.OnFusionEnd.AddListener(FusionManager_OnFusionEnd);
    }

    private void OnDisable() {     
        CardStatSelManager.OnSelectAnother.RemoveListener(CardStatSelManager_OnSelectAnother);
        CardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
        FusionManager.OnFusionEnd.RemoveListener(FusionManager_OnFusionEnd);
    }


    private void CardStatSelManager_OnSelectionsEnd(){
        HideOptions();
    }

    private void CardStatSelManager_OnSelectAnother(Card card){
        SetButtonText(card);
    }

    public void FusionManager_OnFusionEnd(Card card){
        ShowOptions();
        ResultCard = card;
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
        CardStatSelManager.Option1Clicked(ResultCard);
    }

    private void Option2_Clicked(){
        CardStatSelManager.Option2Clicked(ResultCard);
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
