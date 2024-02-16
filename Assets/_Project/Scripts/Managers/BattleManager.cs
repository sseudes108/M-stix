using System.Collections.Generic;
using Mistix;
using Mistix.FusionLogic;
using UnityEngine;

public class BattleManager : MonoBehaviour{
    public static BattleManager Instance;

    private TurnSystem _turnSystem;
    private Fusion _fusion;
    private CardManager _cardManager;

    private void Awake() {
        if(Instance != null){
            Errors.InstanceError(this);
        }
        Instance = this;

        /**-------**/
        _turnSystem = GetComponentInChildren<TurnSystem>();
        _fusion = GetComponentInChildren<Fusion>();
        _cardManager = GetComponentInChildren<CardManager>();
    }

    public void StartFusion(){
        List<Card> selectedCards = new();

        if(_turnSystem.IsPlayerTurn()){
            selectedCards = _cardManager.CardSelector.GetSelectedPlayerCardList();
        }else{
            selectedCards = _cardManager.CardSelector.GetSelectedEnemyCardList();
        }

        _fusion.StartFusionRoutine(selectedCards);
    }

    public void EndTurn(){
        _turnSystem.ChangeTurn();
    }
    
    public TurnSystem TurnSystem => _turnSystem;
    public Fusion Fusion => _fusion;
    public CardCreator CardCreator => _cardManager.CardCreator;
    public CardSelector CardSelector => _cardManager.CardSelector;
    public CardsDatabase CardsDatabase => _cardManager.CardsDatabase;
    public CardSpawnLocations CardSpawnLocations => _cardManager.CardSpawnLocations;
}