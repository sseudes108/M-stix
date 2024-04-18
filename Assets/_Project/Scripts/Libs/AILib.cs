using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AILib: MonoBehaviour{

    #region Summom Fusion Monsters
    public void StrongestMonsterFusion(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        Debug.Log("StrongestMonsterFusion");

        if(CanSummonlvl4(lvl1MonstersList.Count, lvl2MonstersList.Count, lvl3MonstersList.Count)){
            Summonlvl4(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
            return;
        }

        if(CanSummonlvl3(lvl1MonstersList.Count, lvl2MonstersList.Count)){
            Summonlvl3(lvl1MonstersList, lvl2MonstersList);
            return;
        }

        if(CanSummonlvl2(lvl1MonstersList.Count)){
            if(lvl3MonstersList.Count == 0){
                Summonlvl2(lvl1MonstersList);
            }else{
                CheckIsOnField(lvl3MonstersList);
            }
            return;
        }

        if(CanSummonlvl1(lvl1MonstersList)){
            if(lvl3MonstersList.Count == 0 && lvl2MonstersList.Count == 0){
                Summonlvl1(lvl1MonstersList);
            }else{
                if(lvl3MonstersList.Count > 0){
                    CheckIsOnField(lvl3MonstersList);
                }else if(lvl2MonstersList.Count > 0){
                    CheckIsOnField(lvl2MonstersList);
                }
            }
            return;
        }

        // If got here, no monsters to summom
        Debug.LogError("No monsters to Summom");
    }
    public bool CanSummonlvl8(){
        Debug.Log("Can Summon lvl 8");
        return true;
    }
    public bool CanSummonlvl7(){
        Debug.Log("Can Summon lvl 7");
        return true;
    }
    public bool CanSummonlvl6(){
        Debug.Log("Can Summon lvl 6");
        return true;
    }
    public bool CanSummonlvl5(){
        Debug.Log("Can Summon lvl 5");
        return true;
    }
    

    public bool CanSummonlvl4(int lvl1MonstersList, int lvl2MonstersList, int lvl3MonstersList){
        if(lvl3MonstersList > 1){
            return true;
        }
        if(lvl3MonstersList == 1){
            if(lvl2MonstersList >= 2){
                return true;
            }else if(lvl2MonstersList == 1 && lvl1MonstersList > 1){
                return true;
            }
        }
        return false;
    }
    public bool CanSummonlvl3(int lvl1MonstersList, int lvl2MonstersList){
        if(lvl2MonstersList > 1){
            return true;
        }
        if(lvl2MonstersList == 1 && lvl1MonstersList >= 2){
            return true;
        }
        return false;
    }
    public bool CanSummonlvl2(int lvl1MonstersList){
        if (lvl1MonstersList > 1){
            return true;
        }else{
            return false;
        }
    }
    public bool CanSummonlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            return true;
        }else{
            return false;
        }
    }

    public void Summonlvl8(){

    }
    public void Summonlvl7(){

    }
    public void Summonlvl6(){

    }
    public void Summonlvl5(){
        
    }

    public void Summonlvl4(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        Debug.Log("Summom Lvl 4");

        if(lvl3MonstersList.Count > 1){
            //2 lvl3 = 1 lvl4
            GetTopLevel3Monsters(lvl3MonstersList);
        }
        
        //Se Houver um nv3 na mão
        if(lvl3MonstersList.Count == 1){
            if(lvl2MonstersList.Count >= 2){
                //2 lvl2 = 1 lvl3
                GetTopLevel2Monsters(lvl2MonstersList);

            }else if(lvl2MonstersList.Count == 1 && lvl1MonstersList.Count > 1){
                //2 lvl 1 = 1 lvl2
                GetTopLevel1Monsters(lvl1MonstersList);

                //add o lvl 2 para formar o 4
                CheckIsOnField(lvl2MonstersList);
            }
            //Add lvl 3 para formar o 4
            CheckIsOnField(lvl3MonstersList);
        }
    }

    public void Summonlvl3(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList){
        Debug.Log("Summom Lvl 3");

        if(lvl2MonstersList.Count > 1){
            GetTopLevel2Monsters(lvl2MonstersList);
        }
        if(lvl2MonstersList.Count == 1 && lvl1MonstersList.Count >= 2){
            GetTopLevel1Monsters(lvl1MonstersList);
            CheckIsOnField(lvl2MonstersList);
        }
    }
    public void Summonlvl2(List<CardMonster> lvl1MonstersList){
        Debug.Log("Summom Lvl 2");
        GetTopLevel1Monsters(lvl1MonstersList);
    }
    public void Summonlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            CheckIsOnField(lvl1MonstersList);
        }
    }

    public void GetTopLevel3Monsters(List<CardMonster> lvl3MonstersList){
        GetTopLevelInHandFromList(lvl3MonstersList);
    }
    public void GetTopLevel2Monsters(List<CardMonster> lvl2MonstersList){
        GetTopLevelInHandFromList(lvl2MonstersList);
    }
    public void GetTopLevel1Monsters(List<CardMonster> lvl1MonstersList){
        GetTopLevelInHandFromList(lvl1MonstersList);
    }

    private void CheckIsOnField(List<CardMonster> monsterList){
        foreach(var monster in monsterList){
            if(!monster.IsOnField()){
                BattleManager.Instance.CardSelector.AddCardToSelectedList(monster);
                break;
            }
        }
    }

    private void GetTopLevelInHandFromList(List<CardMonster> monsterList){
        List<CardMonster> topMonstersOnHand = new();
        foreach(var monster in monsterList){
            if(!monster.IsOnField()){
                topMonstersOnHand.Add(monster);
            }
        }

        BattleManager.Instance.CardSelector.AddCardToSelectedList(topMonstersOnHand[0]);
        
        if(topMonstersOnHand.Count > 1){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(topMonstersOnHand[1]);
        }
    }

#endregion

    public void CheckBoardFusion(List<CardMonster> MonstersOnAIField, Card cardToFusion){
        var monsterLvl = cardToFusion.GetComponent<CardMonster>().GetLevel();
        foreach(var monster in MonstersOnAIField){
            if(monster.GetLevel() == monsterLvl){
                BoardFusionSetUp(true, monsterLvl);
                break;
            }
        }
    }

    public void BoardFusionSetUp(bool boardFusion, int boardFusionLvl){
        BattleManager.Instance.AIManager.CurrentArchetype.BoardFusion = boardFusion;
        BattleManager.Instance.AIManager.CurrentArchetype.BoardFusionLvl = boardFusionLvl;
        Testing.Instance.UpdateBoardFusionLvl(boardFusionLvl);
    }

}