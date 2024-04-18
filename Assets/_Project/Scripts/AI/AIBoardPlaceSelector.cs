using System.Collections;
using UnityEngine;

public class AIBoardPlaceSelector : MonoBehaviour {

    public void StartBoardPlaceSelection(Card resultCard){
        StartCoroutine(BoardPlaceSelectionRoutine(resultCard));
    }

    private IEnumerator BoardPlaceSelectionRoutine(Card resultCard){
        var monsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
        var arcanePlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlaces;
        BoardCardPlace boarderPlace;

        if(resultCard is CardMonster){
            boarderPlace = monsterPlaces[BattleManager.Instance.AIManager.CurrentArchetype.SelectMonsterPlaceOnBoard(monsterPlaces, resultCard)].GetComponent<BoardCardMonsterPlace>();
        }else{
            //Random Placement if its not board fusion
            boarderPlace = arcanePlaces[Random.Range(0,5)].GetComponent<BoardCardArcanePlace>();
        }
        //

        yield return new WaitForSeconds(1f);
        
        if(boarderPlace.IsFree()){
            boarderPlace.SetCardInPlace(resultCard);
        }else{
            boarderPlace.BoardFusion(resultCard);
        }

        //Atualiza os montros em campo no debug
        BattleManager.Instance.AIManager.CardSelector.AnalyzeMonstersOnField();
        BattleManager.Instance.AIManager.CardSelector.OrganizeCardsFromHand();
    }
}