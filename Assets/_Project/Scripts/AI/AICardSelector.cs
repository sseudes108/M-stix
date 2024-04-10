using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : MonoBehaviour {

    private List<CardMonster> _lvl1MonstersList;
    private List<CardMonster> _lvl2MonstersList;
    private List<CardMonster> _lvl3MonstersList;
    private List<CardMonster> _faceDownP1Monsters;
    private List<CardMonster> _faceUpP1Monsters;
    private List<CardMonster> _AIMonsters;
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

        BattleManager.Instance.AIManager.CurrentArchetype.SelectCard(_lvl1MonstersList, _lvl2MonstersList, _lvl3MonstersList, _trapsList, _fieldsList, _equipsList, _AIMonsters);

        yield return new WaitForSeconds(1f);

        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }

    private void AnalyzeMonstersOnField(){
        var (p1MonstersPlaces, p2MonstersPlaces) = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlacesAI();
        
        _faceDownP1Monsters = new();
        _faceUpP1Monsters = new();
        foreach (var place in p1MonstersPlaces){
            var monster = place.GetCardInThisPlace() as CardMonster;
            if (monster.IsFaceDown()){
                _faceDownP1Monsters.Add(monster);
            }else{
                _faceUpP1Monsters.Add(monster);
            }
        }

        _AIMonsters = new();
        foreach (var place in p2MonstersPlaces){
            var monster = place.GetCardInThisPlace() as CardMonster;
            _AIMonsters.Add(monster);
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
        return (_faceDownP1Monsters, _faceUpP1Monsters);
    }
    public List<CardMonster> GetAIMonstersOnField(){
        return _AIMonsters;
    }
}