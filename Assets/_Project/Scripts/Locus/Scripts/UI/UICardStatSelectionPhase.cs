using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardStatSelectionPhase : MonoBehaviour{
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private FusionManagerSO _fusionManager;
    [SerializeField] private CardStatEventHandlerSO _cardStatSelManager;
    [SerializeField] private TurnManagerSO _turnManager;

    private GameObject _statSelButtonsContainer;
    private Button _statButton1, _statButton2;
    private TextMeshProUGUI _statText1, _statText2;
    
    private void OnEnable() {
        _cardStatSelManager.OnSelectAnother.AddListener(CardStatSelManager_OnSelectAnother);
        _cardStatSelManager.OnSelectionsEnd.AddListener(CardStatSelManager_OnSelectionsEnd);

        _fusionManager.OnFusionStart.AddListener(FusionManager_OnFusionStart);
        _fusionManager.OnFusionEnd.AddListener(FusionManager_OnFusionEnd);
    }

    private void OnDisable() {
        _cardStatSelManager.OnSelectAnother.RemoveListener(CardStatSelManager_OnSelectAnother);
        _cardStatSelManager.OnSelectionsEnd.RemoveListener(CardStatSelManager_OnSelectionsEnd);
        
        _fusionManager.OnFusionStart.RemoveListener(FusionManager_OnFusionStart);
        _fusionManager.OnFusionEnd.RemoveListener(FusionManager_OnFusionEnd);
    }

    private void Awake() {
        _statSelButtonsContainer = transform.Find("Canvas/CardStatButtons").gameObject;
        _statButton1 = _statSelButtonsContainer.transform.Find("StatButton1").GetComponent<Button>();
        _statButton2 = _statSelButtonsContainer.transform.Find("StatButton2").GetComponent<Button>();
        _statText1 = _statButton1.GetComponentInChildren<TextMeshProUGUI>();
        _statText2 = _statButton2.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CardStatSelManager_OnSelectionsEnd(){
        HideOptions();
    }

    private void CardStatSelManager_OnSelectAnother(Card card){
        SetButtonText(card);
    }

    private void FusionManager_OnFusionStart(List<Card> cards, bool isPlayerTurn){
        if(!_turnManager.IsPlayerTurn) { return; }
        HideOptions();
    }

    private void FusionManager_OnFusionEnd(Card card){
        if(!_turnManager.IsPlayerTurn) { return; }
        ShowOptions();
        SetButtonText(card);
    }

    public void ShowOptions(){
        _statSelButtonsContainer.SetActive(true);
        _statButton1.onClick.AddListener(Option1_Clicked);
        _statButton2.onClick.AddListener(Option2_Clicked);
    }

    public void HideOptions(){
        _statSelButtonsContainer.SetActive(false);
        _statButton1.onClick.RemoveListener(Option1_Clicked);
        _statButton2.onClick.RemoveListener(Option2_Clicked);
    }

    private void Option1_Clicked(){
        _cardStatSelManager.Option1Clicked();
    }

    private void Option2_Clicked(){
        _cardStatSelManager.Option2Clicked();
    }

    private void SetButtonText(Card card){
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(!monsterCard.AnimaSelected){
                //Anima
                _statText1.text = $"{monsterCard.FirstAnima}";
                _statText2.text = $"{monsterCard.SecondAnima}";
            }else if(!monsterCard.ModeSelected){
                //Mode
                _statText1.text = $"Attack";
                _statText2.text = $"Deffense";
            }else if(!monsterCard.FusionedCard){
                //Face
                _statText1.text = $"Face Up";
                _statText2.text = $"Face Down";
            }
        }
    }
}