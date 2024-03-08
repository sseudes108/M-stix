using System.Collections;
using UnityEngine;

public class AIBoardPlaceSelector : MonoBehaviour {

    public void StartBoardPlaceSelection(Card resultCard){
        StartCoroutine(BoardPlaceSelectionRoutine(resultCard));
    }

    private IEnumerator BoardPlaceSelectionRoutine(Card resultCard){
        var monsterPlaces = BattleManager.Instance.EnemyBoardPlaces.MonsterPlaces;
        var arcanePlaces = BattleManager.Instance.EnemyBoardPlaces.ArcanePlaces;
        var randomIndex = Random.Range(0,5);

        BoardCardPlacement boarderPlace;
        if(resultCard is CardMonster){
            boarderPlace = monsterPlaces[randomIndex].GetComponent<BoardCardMonsterPlace>();
        }else{
            boarderPlace = arcanePlaces[randomIndex].GetComponent<BoardCardArcanePlace>();
        }

        yield return new WaitForSeconds(1f);
        
        if(boarderPlace.IsFree()){
            boarderPlace.SetCardInPlace(resultCard);
        }else{
            boarderPlace.BoardFusion(resultCard);
        }
    }
}