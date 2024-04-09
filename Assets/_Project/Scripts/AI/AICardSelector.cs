using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : MonoBehaviour {

    private List<CardMonster> _lvl1MonstersList;
    private List<CardMonster> _lvl2MonstersList;
    private List<CardMonster> _lvl3MonstersList;
    private List<CardMonster> faceDownMonsters;
    private List<CardMonster> faceUpMonsters;
    private List<CardArcane> _trapsList;
    private List<CardArcane> _fieldsList;
    private List<CardArcane> _equipsList;
    private List<Card> cardsInHand;

    public void StartCardSelection(){
        StartCoroutine(SelectCardsInEnemyHand());
    }

    private IEnumerator SelectCardsInEnemyHand(){
        AnalyzeMonstersOnField();
        OrganizeCardsFromHand();

        var monsterslvl1 = _lvl1MonstersList.Count;
        var monsterslvl2 = _lvl2MonstersList.Count;
        var monsterslvl3 = _lvl3MonstersList.Count;
        var traps = _trapsList.Count;
        var fields = _fieldsList.Count;
        var equips = _equipsList.Count;

        MakeStrongestFusionPossible(monsterslvl1, monsterslvl2, monsterslvl3);

        yield return new WaitForSeconds(1f);

        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }

    private void AnalyzeMonstersOnField(){
        var oponentTargets = BattleManager.Instance.BoardPlaceManager.GetOcuppiedMonsterPlaces();
        faceDownMonsters = new();
        faceUpMonsters = new();
        foreach (var place in oponentTargets){
            var monster = place.GetCardInThisPlace() as CardMonster;
            if (monster.IsFaceDown()){
                faceDownMonsters.Add(monster);
            }else{
                faceUpMonsters.Add(monster);
            }
        }
    }

    private void MakeStrongestFusionPossible(int monsterslvl1, int monsterslvl2, int monsterslvl3){
        //Strongest monster possible to fusion only from hand
        //mais de 1 nv 3
        //faz 1 nv 4
        if (monsterslvl3 > 1){
            GetTopLevel3Monsters();
        }
        else if (monsterslvl3 == 1){
            //mais de 1 nv 2
            if (monsterslvl2 > 1){
                //faz um nv 3
                GetTopLevel2Monsters();
            }else{
                //mais de um nv 1
                if (monsterslvl1 > 1){
                    //faz um nv 2
                    GetTopLevel1Monsters();
                }
                //add o nv 2 e faz um nv 3
                BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl2MonstersList[0]);
            }
            //add o nv 3 e faz um nv 4
            BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl3MonstersList[0]);

            //mais de um nv 2
        }else if (monsterslvl2 > 1){
            //faz um nv 3
            GetTopLevel2Monsters();
        }
        else if (monsterslvl2 == 1){
            //mais de um nv 1
            if (monsterslvl1 > 1){
                //faz um nv 2
                GetTopLevel1Monsters();
            }
            //add o nv 2 e faz um nv 3
            BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl2MonstersList[0]);

            //mais de um nv 1
        }else if (monsterslvl1 > 1){
            //faz um nv 2
            GetTopLevel1Monsters();
        }else{
            //Ordena os nv1 por ordem de atq e seleciona o mais forte
            _lvl1MonstersList.Sort((x, y) => y.GetAttack().CompareTo(x.GetAttack()));
            BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl1MonstersList[0]);
        }
    }

    private void OrganizeCardsFromHand(){
        cardsInHand = BattleManager.Instance.EnemyHand.GetCardsInHand();

        _lvl1MonstersList = new();
        _lvl2MonstersList = new();
        _lvl3MonstersList = new();
        _trapsList = new();
        _fieldsList = new();
        _equipsList = new();

        foreach (var card in cardsInHand){
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

    private void GetTopLevel3Monsters(){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl3MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl3MonstersList[1]);
    }
    private void GetTopLevel2Monsters(){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl2MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl2MonstersList[1]);
    }
    private void GetTopLevel1Monsters(){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl1MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(_lvl1MonstersList[1]);
    }

    public (List<CardMonster>, List<CardMonster>) GetTargetMonstersOnField(){
        return (faceDownMonsters, faceUpMonsters);
    }
}