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

    //On Field
    // private List<CardMonster> _faceDownP1Monsters;
    // private List<CardMonster> _faceUpP1Monsters;
    private List<CardMonster> _AIMonstersOnField;
    private List<Card> _cardsInHand;

    public void StartCardSelection(){
        StartCoroutine(SelectCardsInEnemyHand());
    }

    private IEnumerator SelectCardsInEnemyHand(){
        OrganizeCardsFromHand();
        AnalyzeMonstersOnField();
        
        SetMonstersList();

        BattleManager.Instance.AIManager.CurrentArchetype.SelectCard(_AIMonstersOnField);

        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }

    private void AnalyzeMonstersOnField(){
        var (playerMonstersPlaces, aiMonsterPlaces) = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlacesAI();

        //Ai monsters on field
        _AIMonstersOnField = new();
        List<Card> faceUpAIMonsters = new();
        List<Card> faceDownAIMonsters = new();

        foreach(var card in aiMonsterPlaces){
            var monster = card.GetCardInThisPlace() as CardMonster;
            if(!monster.IsFaceDown()){
                faceUpAIMonsters.Add(monster);
            }else{
                faceDownAIMonsters.Add(monster);
            }
            _AIMonstersOnField.Add(monster);
        }

        //Player monsters on field
        List<Card> faceUpPlayerMonsters = new();
        List<Card> faceDownPlayerMonsters = new();
        foreach(var place in playerMonstersPlaces){
            var monster = place.GetCardInThisPlace();
            if(monster != null){
                if(!monster.IsFaceDown()){
                    faceUpPlayerMonsters.Add(monster);
                }else{
                    faceDownPlayerMonsters.Add(monster);
                }
            }
        }
    }

    private void OrganizeCardsFromHand(){
        _cardsInHand = BattleManager.Instance.EnemyHand.GetCardsInHand();

        _lvl1MonstersList = new();
        _lvl2MonstersList = new();
        _lvl3MonstersList = new();
        _lvl4MonstersList = new();
        _lvl5MonstersList = new();
        _lvl6MonstersList = new();
        _lvl7MonstersList = new();
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
        BattleManager.Instance.AIManager.CurrentArchetype.SetMonstersList(_lvl1MonstersList, _lvl2MonstersList, _lvl3MonstersList, _lvl4MonstersList, _lvl5MonstersList, _lvl6MonstersList, _lvl7MonstersList);
    }
}