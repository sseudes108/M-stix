using System.Collections.Generic;
using System.Diagnostics;

public class AIAgroMonstersFocused : AIArchetype {
    public override void SelectCard(List<CardMonster> lvl1MonstersList,List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList,List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList, List<CardMonster> monstersOnField){

        CardSelection(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList, trapsList, fieldsList, equipsList, monstersOnField);
        UnityEngine.Debug.Log("AIAgroMonstersFocused - MakeStrongestFusionPossible");
    }

#region High Fusion Lvl Monster
    private void CardSelection(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList, List<CardMonster> monstersOnAIField){

        var traps = trapsList.Count;
        var fields = fieldsList.Count;
        var equips = equipsList.Count;

        if(monstersOnAIField == null){
            StrongestMonsterFusion(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
        }else{
            if(monstersOnAIField.Count > 1){
                //Organiza o maior lvl em campo
                monstersOnAIField.Sort((x, y) => y.GetLevel().CompareTo(x.GetLevel()));

                var lvl = monstersOnAIField[0].GetLevel();
                switch (lvl) {
                    case 1:
                        if(CanSummomlvl1(lvl1MonstersList)){
                            UnityEngine.Debug.Log("CanSummonlvl1");
                            Summomlvl1(lvl1MonstersList);
                        }else{
                            StrongestMonsterFusion(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
                        }
                    break;

                    case 2:
                        if(CanSummonlvl2(lvl1MonstersList)){
                            UnityEngine.Debug.Log("CanSummonlvl2");
                            Summomlvl2(lvl1MonstersList);
                        }else{
                            StrongestMonsterFusion(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
                        }
                    break;

                    case 3:
                        if(CanSummonlvl3(lvl1MonstersList, lvl2MonstersList)){
                            UnityEngine.Debug.Log("CanSummonlvl3");
                            Summomlvl3(lvl1MonstersList, lvl2MonstersList);
                        }else{
                            StrongestMonsterFusion(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
                        }
                    break;

                    case 4:
                        if(CanSummonlvl4(lvl2MonstersList, lvl3MonstersList)){
                            UnityEngine.Debug.Log("CanSummonlvl4");
                            Summomlvl4(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
                        }else{
                            StrongestMonsterFusion(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
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
                StrongestMonsterFusion(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
            }
        }
    }

    private void StrongestMonsterFusion(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        if(!Summomlvl4(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList)){
            if(!Summomlvl3(lvl1MonstersList, lvl2MonstersList)){
                if(!Summomlvl2(lvl1MonstersList)){
                    Summomlvl1(lvl1MonstersList);
                }else{
                    UnityEngine.Debug.Log("CanMakelvl2");
                }
            }else{
               UnityEngine.Debug.Log("CanMakelvl3"); 
            }
        }else{
            UnityEngine.Debug.Log("CanMakelvl4");
        }
    }

    private bool CanSummonlvl4(List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        if (lvl3MonstersList.Count > 1){
            return true;
        }
        else if (lvl3MonstersList.Count  == 1){
            if (lvl2MonstersList.Count > 1){
            }else{
                return true;
            }
        }
        return false;
    }
    private bool CanSummonlvl3(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList){
        if (lvl2MonstersList.Count > 1){
            return true;
        }else if (lvl2MonstersList.Count == 1){
            if (lvl1MonstersList.Count > 1){
                return true;
            }
        }
        return false;
    }
    private bool CanSummonlvl2(List<CardMonster> lvl1MonstersList){
        if (lvl1MonstersList.Count > 1){
            return true;
        }else{
            return false;
        }
    }
    private bool CanSummomlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            return true;
        }else{
            return false;
        }
    }

    private bool Summomlvl4(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        if (lvl3MonstersList.Count > 1){
            GetTopLevel3Monsters(lvl3MonstersList);
            return true;
        }
        else if (lvl3MonstersList.Count  == 1){
            //mais de 1 nv 2
            if (lvl2MonstersList.Count > 1){
                //faz um nv 3
                GetTopLevel2Monsters(lvl2MonstersList);
            }else{
                //mais de um nv 1
                if (lvl1MonstersList.Count > 1){
                    //faz um nv 2
                    GetTopLevel1Monsters(lvl1MonstersList);}
                //add o nv 2 e faz um nv 3
                BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);

                //add o nv 3 e faz um nv 4
                BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[0]);
                return true;
            }
        }
        return false;
    }
    private bool Summomlvl3(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList){
        UnityEngine.Debug.Log("CanMakelvl3");
        if (lvl2MonstersList.Count > 1){
            //faz um nv 3
            GetTopLevel2Monsters(lvl2MonstersList);
            return true;
        }else if (lvl2MonstersList.Count == 1){
            //mais de um nv 1
            if (lvl1MonstersList.Count > 1){
                //faz um nv 2
                GetTopLevel1Monsters(lvl1MonstersList);

                //add o nv 2 e faz um nv 3
                BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
                return true;
            }
        }
        return false;
    }
    private bool Summomlvl2(List<CardMonster> lvl1MonstersList){
        if (lvl1MonstersList.Count > 1){
            //faz um nv 2
            GetTopLevel1Monsters(lvl1MonstersList);
            return true;
        }else{
            return false;
        }
    }
    private bool Summomlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[0]);
            return true;
        }else{
            return false;
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
        UnityEngine.Debug.Log("AIAgroMonstersFocused - SelectMonsterMode");

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

            }
            else if (monstersInDefense.Count > 0){
                //Vê qual o monstro com def mais forte do player em campo e virado para cima
                faceUpMonsters.Sort((x, y) => y.GetDefense().CompareTo(x.GetDefense()));
                if (atk >= faceUpMonsters[0].GetDefense()){
                    return 1;
                }else{
                    return 0;
                }
            }
        }
        else if (faceDownMonsters.Count > 0){
            if (atk >= 3000){
                return 0;
            }else{
                return 1;
            }
        }

        //Se nenhum caso for atendido, retorna atk por padrão
        return 0;
    }

}