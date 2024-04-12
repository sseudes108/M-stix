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

        //Random Placement
        var randomIndex = Random.Range(0,5);
        

        if(resultCard is CardMonster){
            boarderPlace = monsterPlaces[BattleManager.Instance.AIManager.CurrentArchetype.SelectMonsterPlaceOnBoard(monsterPlaces)].GetComponent<BoardCardMonsterPlace>();
            // boarderPlace = monsterPlaces[randomIndex].GetComponent<BoardCardMonsterPlace>();
        }else{
            boarderPlace = arcanePlaces[randomIndex].GetComponent<BoardCardArcanePlace>();
        }
        //

        yield return new WaitForSeconds(1f);
        
        if(boarderPlace.IsFree()){
            boarderPlace.SetCardInPlace(resultCard);
        }else{
            boarderPlace.BoardFusion(resultCard);
        }
    }
}