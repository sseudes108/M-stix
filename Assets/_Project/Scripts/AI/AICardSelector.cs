using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : MonoBehaviour {

    List<CardMonster> lvl1Monsters;
    List<CardMonster> lvl2Monsters;
    List<CardMonster> lvl3Monsters;

    List<Card> cardsInHand;

    public void StartCardSelection(){
        StartCoroutine(SelectCardsInEnemyHand());
    }

    private IEnumerator SelectCardsInEnemyHand(){
        cardsInHand = BattleManager.Instance.EnemyHand.GetCardsInHand();


        foreach(var card in cardsInHand){
            if(card is CardMonster){
               var monster = card as CardMonster;
               switch(monster.GetLevel()){
                    case 1:
                        lvl1Monsters.Add(monster);
                    break;
                    case 2:
                        lvl2Monsters.Add(monster);
                    break;
                    case 3:
                        lvl3Monsters.Add(monster);
                    break;
                }
            }
        }

        var monsterslvl1 = lvl1Monsters.Count;
        var monsterslvl2 = lvl2Monsters.Count;
        var monsterslvl3 = lvl3Monsters.Count;

        //Strongest monster possible to fusion only from hand
            //mais de 1 nv 3
            //faz 1 nv 4
        if(monsterslvl3 > 1){
            GetTopLevel3Monsters();
        }else if(monsterslvl3 == 1){
            //mais de 1 nv 2
            if(monsterslvl2 > 1){
                //faz um nv 3
                GetTopLevel2Monsters();
            }else{
                //mais de um nv 1
                if(monsterslvl1 > 1){
                    //faz um nv 2
                    GetTopLevel1Monsters();
                }
                //add o nv 2 e faz um nv 3
                BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2Monsters[0]);
            }
            //add o nv 3 e faz um nv 4
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3Monsters[0]);

            //mais de um nv 2
        }else if(monsterslvl2 > 1){
            //faz um nv 3
            GetTopLevel2Monsters();
        }else if (monsterslvl2 == 1){
            //mais de um nv 1
            if(monsterslvl1 > 1){
                //faz um nv 2
                GetTopLevel1Monsters();
            }
            //add o nv 2 e faz um nv 3
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2Monsters[0]);

            //mais de um nv 1
        }else if(monsterslvl1 > 1){
            //faz um nv 2
            GetTopLevel1Monsters();
        }else{
            //Ordena os nv1 por ordem de atq e seleciona o mais forte
            lvl1Monsters.Sort((x,y) => y.GetAttack().CompareTo(x.GetAttack()));
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1Monsters[0]);
        }

        yield return new WaitForSeconds(1f);

        BattleManager.Instance.BattleStateManager.BattlePhaseCardSelection.EndSelection();
    }

    private void GetTopLevel3Monsters(){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3Monsters[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3Monsters[1]);
    }
    private void GetTopLevel2Monsters(){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2Monsters[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2Monsters[1]);
    }
    private void GetTopLevel1Monsters(){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1Monsters[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1Monsters[1]);
    }
}