using UnityEngine;
using UnityEngine.UIElements;

public class UICardStatSel : UIManager{
    public BattleManagerSO BattleManager;
    public CardStatEventHandlerSO CardStatSelManager;

    private Button _option1, _option2;
    private VisualElement _optionsCanvas;


    private void OnEnable() {
        CardStatSelManager.OnSelectAnother.AddListener(CardStatSelManager_OnSelectAnother);
        CardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);
    }

    private void OnDisable() {     
        CardStatSelManager.OnSelectAnother.RemoveListener(CardStatSelManager_OnSelectAnother);
        CardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
    }


    private void CardStatSelManager_OnSelectionsEnd(){
        HideOptions();
    }

    private void CardStatSelManager_OnSelectAnother(Card card){
        SetButtonText(card);
    }

    public void FusionEnded(Card card){
        Debug.Log($"UICardStateSel - FusionEnded(Card {card}) <color=red>2</color=red>");
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
        Debug.Log($"UICardStateSel - ShowOptions() <color=red>3</color=red>");
        _optionsCanvas.style.display = DisplayStyle.Flex;
    }

    private void HideOptions(){
        _option1.clicked -= Option1_Clicked;
        _option2.clicked -= Option2_Clicked;
        _optionsCanvas.style.display = DisplayStyle.None;
    }

    private void Option1_Clicked(){
        CardStatSelManager.Option1Clicked();
    }

    private void Option2_Clicked(){
        CardStatSelManager.Option2Clicked();
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
