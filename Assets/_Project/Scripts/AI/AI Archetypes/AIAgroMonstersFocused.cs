using System.Collections.Generic;
using UnityEngine;
public class AIAgroMonstersFocused : AIArchetype {
    
    public override void SelectCard(List<CardMonster> monstersOnField){
        // var traps = trapsList.Count;
        // var fields = fieldsList.Count;
        // var equips = equipsList.Count;

        BattleManager.Instance.AILib.StrongestMonsterFusion(Lvl1MonstersList, Lvl2MonstersList, Lvl3MonstersList);
    }

    public override int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card CardToPutOnBoard){
        CardMonster monsterOnBoardToFusion = null;
        CardMonster monsterToPutOnBoard = CardToPutOnBoard as CardMonster;
        List<BoardCardMonsterPlace> monstersOnBoard = new();

        BattleManager.Instance.AIManager.CardSelector.AnalyzeMonstersOnField();
        if(MonstersOnAIField.Count > 1){
            BattleManager.Instance.AILib.CheckBoardFusion(MonstersOnAIField, monsterToPutOnBoard);
        }

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
            BattleManager.Instance.AILib.BoardFusionSetUp(false, 0);
        }else{
            //Corrigir - Infinite loop if there's no free place in board
            do{
                positionInBoard = Random.Range(0, monsterBoardPlaces.Count);
            }while(monsterBoardPlaces[positionInBoard].GetComponentInChildren<BoardCardMonsterPlace>().GetCardInThisPlace() != null);
        }

        return positionInBoard;
    }

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
}