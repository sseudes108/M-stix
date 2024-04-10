using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : MonoBehaviour {

    private List<CardMonster> _lvl1MonstersList;
    private List<CardMonster> _lvl2MonstersList;
    private List<CardMonster> _lvl3MonstersList;
    private List<CardMonster> _faceDownMonsters;
    private List<CardMonster> _faceUpMonsters;
    private List<CardArcane> _trapsList;
    private List<CardArcane> _fieldsList;
    private List<CardArcane> _equipsList;
    private List<Card> _cardsInHand;

    public void StartCardSelection(){
        StartCoroutine(SelectCardsInEnemyHand());
    }

    private IEnumerator SelectCardsInEnemyHand(){
        AnalyzeMonstersOnField();
        OrganizeCardsFromHand();

        BattleManager.Instance.AIManager.CurrentArchetype.SelectCard(_lvl1MonstersList, _lvl2MonstersList, _lvl3MonstersList, _trapsList, _fieldsList, _equipsList);

        yield return new WaitForSeconds(1f);

        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }

    private void AnalyzeMonstersOnField(){
        var oponentTargets = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces();
        _faceDownMonsters = new();
        _faceUpMonsters = new();
        foreach (var place in oponentTargets){
            var monster = place.GetCardInThisPlace() as CardMonster;
            if (monster.IsFaceDown()){
                _faceDownMonsters.Add(monster);
            }else{
                _faceUpMonsters.Add(monster);
            }
        }
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

    public (List<CardMonster>, List<CardMonster>) GetTargetMonstersOnField(){
        return (_faceDownMonsters, _faceUpMonsters);
    }
}