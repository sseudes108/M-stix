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

        if(monstersOnAIField.Count == 0){
            StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
        }else{
            if(monstersOnAIField.Count > 1){
                //Organiza o maior lvl em campo
                monstersOnAIField.Sort((x, y) => y.GetLevel().CompareTo(x.GetLevel()));

                var lvl = monstersOnAIField[0].GetLevel();
                switch (lvl) {
                    case 1:
                        if(CanSummomlvl1(Lvl1MonstersList)){
                            Debug.Log("CanSummonlvl1");
                            Summomlvl1(Lvl1MonstersList);
                            BoardFusion = true;
                            BoardFusionLvl = lvl;
                        }else{
                            StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                        }
                    break;

                    case 2:
                        if(CanSummonlvl2(Lvl1MonstersList)){
                            Debug.Log("CanSummonlvl2");
                            Summomlvl2(Lvl1MonstersList);
                            BoardFusion = true;
                            BoardFusionLvl = lvl;
                        }else{
                            if(Lvl2MonstersList.Count > 0){
                                BattleManager.Instance.CardSelector.AddCardToSelectedList(Lvl2MonstersList[0]);
                            }else{
                                StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                            }
                        }
                    break;

                    case 3:
                        if(CanSummonlvl3(Lvl1MonstersList, Lvl2MonstersList)){
                            Debug.Log("CanSummonlvl3");
                            Summonlvl3(Lvl1MonstersList, Lvl2MonstersList);
                            BoardFusion = true;
                            BoardFusionLvl = lvl;
                        }else{
                            if(Lvl3MonstersList.Count > 0){
                                BattleManager.Instance.CardSelector.AddCardToSelectedList(Lvl3MonstersList[0]);
                            }else{
                                StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
                            }
                        }
                    break;

                    case 4:
                        if(CanSummonlvl4(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList)){
                            Debug.Log("CanSummonlvl4");
                            Summomlvl4(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
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
    }

#region Summom Fusion Monsters
    private void StrongestMonsterFusion(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        Debug.Log("StrongestMonsterFusion");

        if(CanSummonlvl4(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList)){
            Summomlvl4(lvl1MonstersList, lvl2MonstersList, lvl3MonstersList);
            return;
        }
        if(CanSummonlvl3(lvl2MonstersList, lvl3MonstersList)){
            Summonlvl3(lvl1MonstersList, lvl2MonstersList);
            return;
        }
        if(CanSummonlvl2(lvl2MonstersList)){
            Summomlvl2(lvl1MonstersList);
            return;
        }
        if(CanSummomlvl1(lvl1MonstersList)){
            Summomlvl1(lvl1MonstersList);
            return;
        }

        // If got here no monsters to summom
        Debug.LogError("No monsters to Summom");
    }

    private bool CanSummonlvl4(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        if (lvl3MonstersList.Count > 1){
            return true;

        }else if (lvl3MonstersList.Count == 1){
            if (CanSummonlvl3(lvl1MonstersList, lvl2MonstersList)){
                return true;
            }else{
                if(lvl2MonstersList.Count == 4){
                    return true;
                }
            }
        }
        return false;
    }
    private bool CanSummonlvl3(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList){
        if (lvl2MonstersList.Count > 1){
            return true;
        }else{
            if(lvl2MonstersList.Count == 1){
                if(CanSummonlvl2(lvl1MonstersList)){
                    return true;
                }
            }else{
                if(lvl1MonstersList.Count == 4){
                    return true;
                }
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

    private void Summomlvl4(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList, List<CardMonster> lvl3MonstersList){
        Debug.Log("Summom Lvl 4");

        if (lvl3MonstersList.Count > 1){
            GetTopLevel3Monsters(lvl3MonstersList, false);
            //Se tiver mais de um nv 3 na mão. OK

        }else if (lvl3MonstersList.Count == 1){
            //mais de 1 nv 2
            if (CanSummonlvl3(lvl1MonstersList, lvl2MonstersList)){
                //faz um nv 3
                GetTopLevel2Monsters(lvl2MonstersList, false);
                //Se tiver um nv 3 na  mão e se pode fazer outro. OK
            }else{
                if(lvl2MonstersList.Count == 4){
                    GetTopLevel2Monsters(lvl2MonstersList, true);
                }
            }
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[0]);
        }
    }
    private void Summonlvl3(List<CardMonster> lvl1MonstersList, List<CardMonster> lvl2MonstersList){
        Debug.Log("Summom Lvl 3");
        if (CanSummonlvl3(lvl1MonstersList, lvl2MonstersList)){
            //faz um nv 3
            GetTopLevel2Monsters(lvl2MonstersList, false);
            //OK
        }else{
            if(lvl2MonstersList.Count == 1){
                if(CanSummonlvl2(lvl1MonstersList)){
                    GetTopLevel1Monsters(lvl1MonstersList, false);
                    // BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
                    //Ok
                }
            }else{
                if(lvl1MonstersList.Count == 4){
                    GetTopLevel1Monsters(lvl1MonstersList, true);
                }
            }
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
        }
    }
    private void Summomlvl2(List<CardMonster> lvl1MonstersList){
        if (lvl1MonstersList.Count > 1){
            //faz um nv 2
            GetTopLevel1Monsters(lvl1MonstersList, false);
        }
    }
    private void Summomlvl1(List<CardMonster> lvl1MonstersList){
        if(lvl1MonstersList.Count > 0){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[0]);
        }
    }

    private void GetTopLevel3Monsters(List<CardMonster> lvl3MonstersList, bool fourCardsFusion){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[1]);
        if(fourCardsFusion){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[2]);
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl3MonstersList[3]);
        }
    }
    private void GetTopLevel2Monsters(List<CardMonster> lvl2MonstersList, bool fourCardsFusion){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[1]);

        if(fourCardsFusion){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[2]);
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl2MonstersList[3]);
        }
    }
    private void GetTopLevel1Monsters(List<CardMonster> lvl1MonstersList, bool fourCardsFusion){
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[0]);
        BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[1]);

        if(fourCardsFusion){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[2]);
            BattleManager.Instance.CardSelector.AddCardToSelectedList(lvl1MonstersList[3]);
        }
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
        var pos = 0;
        BoardCardMonsterPlace place = null;

        if(BoardFusion){
            switch (BoardFusionLvl) {
                case 1:
                    place = Lvl1MonstersList[0].GetComponentInParent<BoardCardMonsterPlace>();
                    pos = 1;
                break;

                case 2:
                    place = Lvl2MonstersList[0].GetComponentInParent<BoardCardMonsterPlace>();
                    pos = 1;
                break;

                case 3:
                    place = Lvl3MonstersList[0].GetComponentInParent<BoardCardMonsterPlace>();
                    pos = 1;
                break;

                case 4:
                    place = Lvl4MonstersList[0].GetComponentInParent<BoardCardMonsterPlace>();
                    pos = 1;
                break;

                case 5:
                    pos = 1;
                break;

                case 6:
                    pos = 1;
                break;

                case 7:
                    pos = 1;
                break;
            }
        }else{
            pos = Random.Range(0, monsterBoardPlaces.Count);
        }

        if(place != null){
            var cardInPlace = place.GetCardInThisPlace();
        }

        return pos;
    }
}