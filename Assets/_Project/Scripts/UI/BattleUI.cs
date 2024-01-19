using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour{
    public static BattleUI Instance{get; private set;}
    [SerializeField] private Button _endSelectionButon;

    [Header("Card Info")]
    //Card type = Arcane/Monster
        //sub type =  magic, trap, monsters types
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardAtk, _cardDef, _cardLvl, _cardType_, _cardSubType;

    [Header("Battle Info")]
    [SerializeField] private TextMeshProUGUI _playerHP;
    [SerializeField] private TextMeshProUGUI  _playerDeck, _EnemyHP, _enemyDeck;

    private void OnEnable() {
        FusionLogic.Instance.OnFusionStarted += FusionLogic_OnFusionStarted;
        FusionLogic.Instance.OnFusionEnded += FusionLogic_OnFusionEnded;
        Hand.OnAnyCardDraw += PlayerHand_OnAnyCardDraw;
    }

    private void OnDisable() {
        FusionLogic.Instance.OnFusionStarted -= FusionLogic_OnFusionStarted;
        FusionLogic.Instance.OnFusionEnded -= FusionLogic_OnFusionEnded;
    }

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one BattleUI instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }

    public void UpdateMonsterCardUIInfo(string cardName, int cardAtk, int cardDef, int cardLvl){
        _cardName.text = "Name: " + cardName;
        _cardAtk.text = "Atk: " + cardAtk.ToString();
        _cardDef.text = "Def: " + cardDef.ToString();
        _cardLvl.text = "Lvl: " + cardLvl.ToString();
    }

    public void UpdateArcaneCardUIInfo(string cardName){
        _cardName.text = "Name: " + cardName;
    }

    public void UpdateDeckInfo(Hand playerHand){
        _playerDeck.text = "Deck: " + playerHand.GetCountDeckInUse().ToString();
    }

    private void PlayerHand_OnAnyCardDraw(Hand playerHand){
        UpdateDeckInfo(playerHand);
    }

    private void FusionLogic_OnFusionStarted(){
        _endSelectionButon.gameObject.SetActive(false);
    }

    private void FusionLogic_OnFusionEnded(){
        _endSelectionButon.gameObject.SetActive(true);
    }
}
