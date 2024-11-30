using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardPlaceSelector : AIAction {
    public AIBoardPlaceSelector(AI ai, AIActor actor){
        _AI = ai;
        _Actor = actor;
    }

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
            _Actor.Fusioner.CheckForBoardMonsterFusion(cardToPlace as MonsterCard);
        }else{
            //Arcane Options
        }

        if(_AI.Actor.MakeABoardFusion){
            _boardPlace = null;
            _boardPlace = _Actor.CardOnBoardToFusion.GetBoardPlace();
            
            _AI.ChangeState(_AI.CardSelect);
        }else{
            // SelectFirstFreePlace(cardToPlace);
            SelectRandomFreePlace(cardToPlace);
            _AI.Actor.BoardPlaceSelected();
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

// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AIBoardPlaceSelector : AIAction {
//     public AIBoardPlaceSelector(AIActorSO actor, AIManagerSO manager){
//         _actor = actor;
//         _manager = manager;
//         _cardOrganizer = _manager.AI.CardOrganizer;
//     }

//     private List<BoardPlace> _monsterPlaces;
//     private List<BoardPlace> _arcanePlaces;

//     private BoardPlace _boardPlace;

//     public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
//         _monsterPlaces = monsterPlaces;
//         _arcanePlaces = arcanePlaces;
//     }

//     public IEnumerator BoardSelectionRoutine(Card cardToPlace){
//         yield return new WaitForSeconds(2f);

//         if(cardToPlace is MonsterCard){
//             // _actor.Fusioner.CheckForBoardMonsterFusion(cardToPlace as MonsterCard);
//             _AI.Actor.Fusioner.CheckForBoardMonsterFusion(cardToPlace as MonsterCard);

//         }else{
//             //Arcane Options
//         }

//         // if(_actor.MakeABoardFusion){
//         //     _boardPlace = null;
//         //     _boardPlace = _actor.CardOnBoardToFusion.GetBoardPlace();
            
//         //     _actor.AIManager.AI.ChangeState(_actor.AIManager.AI.CardSelect);
//         // }else{
//         //     // SelectFirstFreePlace(cardToPlace);
//         //     SelectRandomFreePlace(cardToPlace);
//         //     _actor.BoardPlaceSelected();
//         // }

//         // if(_actor.MakeABoardFusion){
//         //     _boardPlace = null;
//         //     _boardPlace = _cardOrganizer.CardOnBoardToFusion.GetBoardPlace();
            
//         //     _manager.AI.ChangeState(_manager.AI.CardSelect);
//         // }else{
//         //     // SelectFirstFreePlace(cardToPlace);
//         //     SelectRandomFreePlace(cardToPlace);
//         //     _actor.BoardPlaceSelected();
//         // }

//         if(_AI.Actor.MakeABoardFusion){
//             _boardPlace = null;
//             _boardPlace = _cardOrganizer.CardOnBoardToFusion.GetBoardPlace();
            
//             _manager.AI.ChangeState(_manager.AI.CardSelect);
//         }else{
//             // SelectFirstFreePlace(cardToPlace);
//             SelectRandomFreePlace(cardToPlace);
//             _AI.Actor.BoardPlaceSelected();
//         }
        
//         yield return null;
//     }

//     private void SelectFirstFreePlace(Card cardToPlace){
//         if(cardToPlace is MonsterCard){
//             foreach(var place in _monsterPlaces){
//                 if(place.IsFree){
//                     place.SetCardInPlace(cardToPlace);
//                     break;
//                 }
//             }
//         }
//     }

//     private void SelectRandomFreePlace(Card cardToPlace){
//         List<BoardPlace> freePlaces = new();
//         if(cardToPlace is MonsterCard){
//             foreach(var place in _monsterPlaces){
//                 if(place.IsFree){
//                     freePlaces.Add(place);
//                 }
//             }
//             freePlaces[Random.Range(0, freePlaces.Count)].SetCardInPlace(cardToPlace);
//         }
//     }

// }