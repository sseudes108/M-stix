using System.Collections.Generic;

public class AIAgroMonstersFocused : AIArchetype {
    public override void SelectCard(List<CardMonster> lvl1MonstersList,List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList,List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList, List<CardMonster> monstersOnField){

        MakeStrongestFusionPossible(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList, trapsList, fieldsList, equipsList, monstersOnField);
        UnityEngine.Debug.Log("AIAgroMonstersFocused - MakeStrongestFusionPossible");
    }

#region High Fusion Lvl Monster
    private void MakeStrongestFusionPossible(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList, List<CardArcane> trapsList, List<CardArcane> fieldsList, List<CardArcane> equipsList, List<CardMonster> monstersOnField){

        var monsterslvl1 = lvl1MonstersList.Count;
        var monsterslvl2 = lvl2MonstersList.Count;
        var monsterslvl3 = lvl3MonstersList.Count;
        var traps = trapsList.Count;
        var fields = fieldsList.Count;
        var equips = equipsList.Count;

        if(monstersOnField.Count == 0){
            
        }else{

        }

        //Strongest monster possible to fusion only from hand
        //mais de 1 nv 3
        //faz 1 nv 4
        if (monsterslvl3 > 1){
            GetTopLevel3Monsters(lvl3MonstersList);
        }
        else if (monsterslvl3 == 1){
            //mais de 1 nv 2
            if (monsterslvl2 > 1){
                //faz um nv 3
                GetTopLevel2Monsters(lvl2MonstersList);
            }else{
                //mais de um nv 1
                if (monsterslvl1 > 1){
                    //faz um nv 2
                    GetTopLevel1Monsters(lvl1MonstersList);
                }
                //add o nv 2 e faz um nv 3
                BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
            }
            //add o nv 3 e faz um nv 4
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[0]);

            //mais de um nv 2
        }else if (monsterslvl2 > 1){
            //faz um nv 3
            GetTopLevel2Monsters(lvl2MonstersList);
        }
        else if (monsterslvl2 == 1){
            //mais de um nv 1
            if (monsterslvl1 > 1){
                //faz um nv 2
                GetTopLevel1Monsters(lvl1MonstersList);
            }
            //add o nv 2 e faz um nv 3
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);

            //mais de um nv 1
        }else if (monsterslvl1 > 1){
            //faz um nv 2
            GetTopLevel1Monsters(lvl1MonstersList);
        }else{
            //Ordena os nv1 por ordem de atq e seleciona o mais forte
            lvl1MonstersList.Sort((x, y) => y.GetAttack().CompareTo(x.GetAttack()));
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