using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : MonoBehaviour {

    //AI Hand
    private List<CardMonster> _lvl1MonstersList;
    private List<CardMonster> _lvl2MonstersList;
    private List<CardMonster> _lvl3MonstersList;
    private List<CardMonster> _lvl4MonstersList;
    private List<CardMonster> _lvl5MonstersList;
    private List<CardMonster> _lvl6MonstersList;
    private List<CardMonster> _lvl7MonstersList;
    private List<CardArcane> _trapsList;
    private List<CardArcane> _fieldsList;
    private List<CardArcane> _equipsList;

    //monsters On Field
    private List<CardMonster> _AIMonstersOnField;
    private List<Card> _faceUpAIMonsters;
    private List<Card> _faceDownAIMonsters;

    private List<Card> _faceUpPlayerMonsters;
    private List<Card> _faceDownPlayerMonsters;

    private List<Card> _cardsInHand;
    public void StartCardSelection(){
        StartCoroutine(SelectCardsInEnemyHand());
    }

    private IEnumerator SelectCardsInEnemyHand(){
        OrganizeCardsFromHand();
        AnalyzeMonstersOnField();
        
        BattleManager.Instance.AIManager.CurrentArchetype.SelectCard(_AIMonstersOnField);

        yield return new WaitForSeconds(2f);
        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }

    public void AnalyzeMonstersOnField(){
        var (playerMonstersPlaces, aiMonsterPlaces) = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlacesAI();

        //Ai monsters on field
        _AIMonstersOnField = new();
        _faceUpAIMonsters = new();
        _faceDownAIMonsters = new();

        _lvl4MonstersList = new();
        _lvl5MonstersList = new();
        _lvl6MonstersList = new();
        _lvl7MonstersList = new();

        foreach(var card in aiMonsterPlaces){
            var monster = card.GetCardInThisPlace() as CardMonster;
            var monsterLvl = monster.GetLevel();

            //Face
            if(!monster.IsFaceDown()){
                _faceUpAIMonsters.Add(monster);
            }else{
                _faceDownAIMonsters.Add(monster);
            }

            //Organize Levels
            switch(monsterLvl){
                case 1:
                    _lvl1MonstersList.Add(monster);
                break;

                case 2:
                    _lvl2MonstersList.Add(monster);
                break;

                case 3:
                    _lvl3MonstersList.Add(monster);
                break;

                case 4:
                    _lvl4MonstersList.Add(monster);
                break;

                case 5:
                    _lvl5MonstersList.Add(monster);
                break;

                case 6:
                    _lvl6MonstersList.Add(monster);
                break;

                case 7:
                    _lvl7MonstersList.Add(monster);
                break;
            }

            _AIMonstersOnField.Add(monster);
        }

        //Player monsters on field
        _faceUpPlayerMonsters = new();
        _faceDownPlayerMonsters = new();
        foreach(var place in playerMonstersPlaces){
            var monster = place.GetCardInThisPlace();
            if(monster != null){
                if(!monster.IsFaceDown()){
                    _faceUpPlayerMonsters.Add(monster);
                }else{
                    _faceDownPlayerMonsters.Add(monster);
                }
            }
        }

        SetMonstersList();
    }

    private void OrganizeCardsFromHand(){
        _cardsInHand = BattleManager.Instance.EnemyHand.GetCardsInHand();

        _lvl1MonstersList = new();
        _lvl2MonstersList = new();
        _lvl3MonstersList = new();
        
        _trapsList = new();
        _fieldsList = new();
        _equipsList = new();

        foreach (var card in _cardsInHand){
            if (card is CardMonster){
                var monster = card as CardMonster;
                switch (monster.GetLevel()){
                    case 1:
                        _lvl1MonstersList.Add(monster);
                        break;
                    case 2:
                        _lvl2MonstersList.Add(monster);
                        break;
                    case 3:
                        _lvl3MonstersList.Add(monster);
                        break;
                }
            }else{
                var arcane = card as CardArcane;
                switch (arcane.GetArcaneType()){
                    case EArcaneType.Field:
                        _equipsList.Add(arcane);
                        break;
                    case EArcaneType.Equip:
                        _equipsList.Add(arcane);
                        break;
                    case EArcaneType.Trap:
                        _trapsList.Add(arcane);
                        break;
                }
            }
        }
    }

    private void SetMonstersList(){
        BattleManager.Instance.AIManager.CurrentArchetype.SetMonstersList(_AIMonstersOnField, _lvl1MonstersList, _lvl2MonstersList, _lvl3MonstersList, _lvl4MonstersList, _lvl5MonstersList, _lvl6MonstersList, _lvl7MonstersList);

        //DEBUG//
        UpdateDebugLists();
        //DEBUG//
    }

    private void UpdateDebugLists(){
        Testing.Instance.UpdateLists(
            _lvl1MonstersList, 
            _lvl2MonstersList, 
            _lvl3MonstersList, 
            _lvl4MonstersList, 
            _lvl5MonstersList, 
            _lvl6MonstersList, 
            _lvl7MonstersList, 
            _trapsList, 
            _fieldsList,
            _equipsList,
            _AIMonstersOnField,
            _faceUpAIMonsters,
            _faceDownAIMonsters,
            _faceUpPlayerMonsters,
            _faceDownPlayerMonsters
        );
    }
}