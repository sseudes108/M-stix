using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class AIAgroMonstersFocused : AIArchetype {
    
    public override void SelectCard(List<CardMonster> monstersOnField){
        CardSelection(monstersOnField);
    }

    private void CardSelection(List<CardMonster> monstersOnAIField){
        // var traps = trapsList.Count;
        // var fields = fieldsList.Count;
        // var equips = equipsList.Count;

        if(monstersOnAIField.Count > 1){
            CheckBoardFusion(monstersOnAIField);
        }else{
            StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
        }
    }

    private void CheckBoardFusion(List<CardMonster> monstersOnAIField){
        if (monstersOnAIField.Count > 1){
            //Organiza o maior lvl em campo
            monstersOnAIField.Sort((x, y) => y.GetLevel().CompareTo(x.GetLevel()));

            BoardFusion = false;
            var lvl = monstersOnAIField[0].GetLevel();

            switch (lvl){
                case 1:
                    if (CanSummonlvl1(Lvl1MonstersList)){
                        Debug.Log("CanSummonlvl1");
                        Summonlvl1(Lvl1MonstersList);
                        BoardFusion = true;
                        BoardFusionLvl = lvl;
                    }else{
                        StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                    }
                    break;

                case 2:
                    if (CanSummonlvl2(Lvl1MonstersList.Count)){
                        Debug.Log("CanSummonlvl2");
                        Summonlvl2(Lvl1MonstersList);
                        BoardFusion = true;
                        BoardFusionLvl = lvl;
                    }else{
                        if (Lvl2MonstersList.Count > 0){
                            BattleManager.Instance.CardSelector.AddCardToSelectedList(Lvl2MonstersList[0]);
                        }else
                        {
                            StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                        }
                    }
                    break;

                case 3:
                    if (CanSummonlvl3(Lvl1MonstersList.Count, Lvl2MonstersList.Count)){
                        Debug.Log("CanSummonlvl3");
                        Summonlvl3(Lvl1MonstersList, Lvl2MonstersList);
                        BoardFusion = true;
                        BoardFusionLvl = lvl;
                    }else{
                        if (Lvl3MonstersList.Count > 0){
                            BattleManager.Instance.CardSelector.AddCardToSelectedList(Lvl3MonstersList[0]);
                        }else{
                            StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                        }
                    }
                    break;

                case 4:
                    if (CanSummonlvl4(Lvl1MonstersList.Count, Lvl2MonstersList.Count, Lvl3MonstersList.Count)){
                        Debug.Log("CanSummonlvl4");
                        Summonlvl4(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                        BoardFusion = true;
                        BoardFusionLvl = lvl;
                    }else{
                        StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                    }
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;
            }
        }else{
            StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
        }
    }

    #region Summom Fusion Monsters
    private void StrongestMonsterFusion(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
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
            Summonlvl2(lvl1MonstersList);
            return;
        }
        if(CanSummonlvl1(lvl1MonstersList)){
            Summonlvl1(lvl1MonstersList);
            return;
        }

        // If got here, no monsters to summom
        Debug.LogError("No monsters to Summom");
    }

    private bool CanSummonlvl4(int lvl1MonstersList, int lvl2MonstersList, int lvl3MonstersList){
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
    private bool CanSummonlvl3(int lvl1MonstersList, int lvl2MonstersList){
        if(lvl2MonstersList > 1){
            return true;
        }
        if(lvl2MonstersList == 1 && lvl1MonstersList >= 2){
            return true;
        }
        return false;
    }
    private bool CanSummonlvl2(int lvl1MonstersList){
        if (lvl1MonstersList > 1){
            return true;
        }else{
            return false;
        }
    }
    private bool CanSummonlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            return true;
        }else{
            return false;
        }
    }

    private void Summonlvl4(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
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
                BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
            }
            //Add lvl 3 para formar o 4
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[0]);
        }
    }
    private void Summonlvl3(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList){
        Debug.Log("Summom Lvl 3");

        if(lvl2MonstersList.Count > 1){
            GetTopLevel2Monsters(lvl2MonstersList);
        }
        if(lvl2MonstersList.Count == 1 && lvl1MonstersList.Count >= 2){
            GetTopLevel1Monsters(lvl1MonstersList);
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
        }
    }
    private void Summonlvl2(List<CardMonster> lvl1MonstersList){
        Debug.Log("Summom Lvl 2");
        GetTopLevel1Monsters(lvl1MonstersList);
    }
    private void Summonlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[0]);
        }
    }

    private void GetTopLevel3Monsters(List<CardMonster> lvl3MonstersList){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[1]);
    }
    private void GetTopLevel2Monsters(List<CardMonster> lvl2MonstersList){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[1]);
    }
    private void GetTopLevel1Monsters(List<CardMonster> lvl1MonstersList){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[1]);
    }

#endregion

    public override int SelectMonsterMode(int atk, List<CardMonster> faceDownMonsters, List<CardMonster> faceUpMonsters, List<CardMonster> monstersInDefense, List<CardMonster> monstersInAttack){
        Debug.Log("AIAgroMonstersFocused - SelectMonsterMode");

        //Se houver monstros virados para cima
        if (faceUpMonsters.Count > 0){
            if (monstersInAttack.Count > 0){
                //Vê qual o monstro mais forte do player em campo e virado para cima
                faceUpMonsters.Sort((x, y) => y.GetAttack().CompareTo(x.GetAttack()));
                if (atk >= faceUpMonsters[0].GetAttack()){
                    return 0;
                }else{
                    return 1;
                }

            }else if (monstersInDefense.Count > 0){
                //Vê qual o monstro com def mais forte do player em campo e virado para cima
                faceUpMonsters.Sort((x, y) => y.GetDefense().CompareTo(x.GetDefense()));
                if (atk >= faceUpMonsters[0].GetDefense()){
                    return 1;
                }else{
                    return 0;
                }
            }

        }else if (faceDownMonsters.Count > 0){
            if (atk >= 3000){
                return 0;
            }else{
                return 1;
            }
        }

        //Se nenhum caso for atendido, retorna atk por padrão
        return 0;
    }

    public override int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces){
        CardMonster monsterOnBoardToFusion = null;
        List<BoardCardMonsterPlace> monstersOnBoard = new();

        int positionInBoard;
        if (BoardFusion){
            foreach(var boardPlace in monsterBoardPlaces){
                var place = boardPlace.GetComponentInChildren<BoardCardMonsterPlace>();
                var monster = place.GetCardInThisPlace() as CardMonster;
                if(monster != null && monster.GetLevel() == BoardFusionLvl) {
                    monsterOnBoardToFusion = monster;
                }
                monstersOnBoard.Add(place);
            }
            positionInBoard = monstersOnBoard.IndexOf(monsterOnBoardToFusion.GetComponentInParent<BoardCardMonsterPlace>());
            BoardFusion = false;
        }else{
            do{
                positionInBoard = Random.Range(0, monsterBoardPlaces.Count);
            }while(monsterBoardPlaces[positionInBoard].GetComponentInChildren<BoardCardMonsterPlace>().GetCardInThisPlace() != null);
        }
        return positionInBoard;
    }
}