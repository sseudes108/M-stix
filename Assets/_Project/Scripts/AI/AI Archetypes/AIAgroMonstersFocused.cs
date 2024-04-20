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

        if(CardsList.PlayerMonstersFaceUp.Count > 0){
            CardsList.PlayerMonstersFaceUp.Sort((x,y) => y.GetAttack().CompareTo(x.GetAttack()));
        }

        //Se o ataq for menor que o tres mais fortes que o do player
        if(atk >= CardsList.PlayerMonstersFaceUp[0].GetAttack()
            || atk >= CardsList.PlayerMonstersFaceUp[1].GetAttack()
            || atk >= CardsList.PlayerMonstersFaceUp[2].GetAttack()
        ){
            return 0;
        }else{
            return 1;
        }

        // //retorno atk padrão
        // return 0;
    }

    public override int SelectCardFace(Card monster){
        return 1;
    }

    public override void CheckAttack(){
        var cardsOnField = BattleManager.Instance.AILib.GetCardsOnField();

        if(cardsOnField.PlayerFaceUpMonsters.Count > 0 && cardsOnField.AIFaceUpMonsters.Count > 0){
            Debug.Log("Chegou até aqui");
        }
    }
}