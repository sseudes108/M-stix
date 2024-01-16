using System;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour{
    public static BattleUI Instance{get; private set;}

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one BattleUI instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }

    //Card type = Arcane/Monster
        //sub type =  magic, trap, monsters types
    [SerializeField] private TextMeshProUGUI _cardName, _cardAtk, _cardDef, _cardLvl, _cardType_, _cardSubType;

    [SerializeField] private TextMeshProUGUI _playerHP, _playerDeck, _EnemyHP, _enemyDeck;

    public void UpdateMonsterCardUIInfo(string cardName, int cardAtk, int cardDef, int cardLvl){
        _cardName.text = "Name: " + cardName;
        _cardAtk.text = "Atk: " + cardAtk.ToString();
        _cardDef.text = "Def: " + cardDef.ToString();
        _cardLvl.text = "Lvl: " + cardLvl.ToString();
    }

    public void UpdateArcaneCardUIInfo(string cardName){
        _cardName.text = "Name: " + cardName;
    }
}
