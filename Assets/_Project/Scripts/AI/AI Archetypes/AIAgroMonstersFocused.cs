using System.Collections.Generic;
using UnityEngine;
public class AIAgroMonstersFocused : AIArchetype {
    
    public override void SelectCard(){
        if(CardsList.MonstersOnAIField.Count > 2){
            BattleManager.Instance.AILib.CheckBoardForLowLevelFusion(CardsList);
        }else{
            BattleManager.Instance.AILib.StrongestMonsterFusion(CardsList);
        }
    }

    public override int SelectMonsterPlaceOnBoard(List<Transform> monsterBoardPlaces, Card CardToPutOnBoard){
        CardMonster monsterOnBoardToFusion = null;
        CardMonster monsterToPutOnBoard = CardToPutOnBoard as CardMonster;
        List<BoardCardMonsterPlace> monstersOnBoard = new();

        BattleManager.Instance.AIManager.CardSelector.AnalyzeMonstersOnField();

        if(CardsList.MonstersOnAIField.Count > 1){
            BattleManager.Instance.AILib.CheckBoardFusion(CardsList.MonstersOnAIField, monsterToPutOnBoard);
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

    public override int SelectMonsterMode(CardMonster monster){
        //0 = atk 1 = def
        int atk = monster.GetAttack();

        if(CardsList.FaceUpPlayerMonsters.Count > 0){
            CardsList.FaceUpPlayerMonsters.Sort((x,y) => y.GetAttack().CompareTo(x.GetAttack()));
        }

        if(atk >= CardsList.FaceUpPlayerMonsters[0].GetAttack()){
            return 0;
        }else{
            return 1;
        }

        // //retorno atk padrão
        // return 0;
    }
}