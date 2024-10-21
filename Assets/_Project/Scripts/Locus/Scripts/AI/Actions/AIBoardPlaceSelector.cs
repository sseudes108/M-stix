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
        
        if(cardToPlace is MonsterCard){
            var lvl = (cardToPlace as MonsterCard).Level;

            switch(lvl){
                case 4:
                    if(_actor.FieldChecker.Lvl4OnAIField.Count > 0){
                        _actor.BoardFusion(_actor.FieldChecker.Lvl4OnAIField[0]);
                    }
                break;

                case 3:
                    if(_actor.FieldChecker.Lvl3OnAIField.Count > 0){
                        _actor.BoardFusion(_actor.FieldChecker.Lvl3OnAIField[0]);
                    }
                break;
            }
        }
        
        if(_actor.MakeABoardFusion){
            _boardPlace = null;
            _boardPlace = _actor.CardOnBoardToFusion.GetBoardPlace();
            
            _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.CardSelect);
        }else{
            SelectFirstFreePlace(cardToPlace);
        }
        
        yield return null;
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