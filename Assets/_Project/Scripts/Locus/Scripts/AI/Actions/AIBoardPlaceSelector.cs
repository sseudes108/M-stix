using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardPlaceSelector : AIAction {
    public AIBoardPlaceSelector(AIActorSO actor){_actor = actor;}

    private List<BoardPlace> _monsterPlaces;
    private List<BoardPlace> _arcanePlaces;

    private BoardPlace _boardPlace;

    public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
        _monsterPlaces = monsterPlaces;
        _arcanePlaces = arcanePlaces;
    }

    public IEnumerator BoardSelectionRoutine(Card cardToPlace){
        yield return new WaitForSeconds(2f);

        if(_actor.MakeABoardFusion){
            _boardPlace = null;
            _boardPlace = _actor.CardOnBoardToFusion.GetBoardPlace();
            
            _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.CardSelect);
        }else{
            if(_boardPlace != null){ //If was a board fusion
                _boardPlace.SetCardInPlace(cardToPlace); 
            }else{
                SelectFirstFreePlace(cardToPlace);
                yield return null;
            }
        }


        // if(_actor.MakeABoardFusion){
        //     _boardPlace = _actor.CardOnBoardToFusion.GetBoardPlace();
        //     _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.CardSelect);
        //     yield break;
        // }

        // yield return new WaitForSeconds(2f);
        // if(_boardPlace != null){ //If was a board fusion
        //     _boardPlace.SetCardInPlace(cardToPlace); 
        // }else{
        //     SelectFirstFreePlace(cardToPlace);
        //     yield return null;
        // }
    }

    private void SelectFirstFreePlace(Card cardToPlace){
        if(cardToPlace is MonsterCard){
            foreach(var place in _monsterPlaces){
                if(place.IsFree){
                    place.SetCardInPlace(cardToPlace);
                    break;
                }
            }
        }
    }

    private void SelectRandomFreePlace(Card cardToPlace){
        List<BoardPlace> freePlaces = new();
        if(cardToPlace is MonsterCard){
            foreach(var place in _monsterPlaces){
                if(place.IsFree){
                    freePlaces.Add(place);
                }
            }
            freePlaces[Random.Range(0, freePlaces.Count)].SetCardInPlace(cardToPlace);
        }
    }
}