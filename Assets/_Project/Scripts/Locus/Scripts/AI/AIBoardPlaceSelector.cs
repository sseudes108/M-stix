using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardPlaceSelector : AIAction {
    public AIBoardPlaceSelector(AIActorSO actor){_actor = actor;}

    private List<BoardPlace> _monsterPlaces;
    private List<BoardPlace> _arcanePlaces;

    public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
        _monsterPlaces = monsterPlaces;
        _arcanePlaces = arcanePlaces;
    }

    public IEnumerator BoardSelectionRoutine(Card cardToPlace){
        if(_actor.MakeABoardFusion){
            BoardPlace boardPlace = _actor.CardOnBoardToFusion.GetBoardPlace();
            boardPlace.SetCardInPlace(cardToPlace);

            _actor.ResetBoardFusion();
            yield return null;

        }else{
            yield return new WaitForSeconds(2f);
            SelectFirstFreePlace(cardToPlace);
            // SelectRandomFreePlace(cardToPlace);
            yield return null;
        }
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