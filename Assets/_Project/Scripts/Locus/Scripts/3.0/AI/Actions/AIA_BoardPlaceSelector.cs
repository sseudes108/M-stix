using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIA_BoardPlaceSelector : AIA_Action{
        public AIA_BoardPlaceSelector(AIActor actor) : base(actor){}

        private List<BoardPlace> _monsterPlaces;
        private List<BoardPlace> _arcanePlaces;
        private BoardPlace _boardPlace;

        public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
            // Debug.Log("Set AI Board Places");
            _monsterPlaces = monsterPlaces;
            _arcanePlaces = arcanePlaces;
        }

        public override void StartActionRoutine(){
            _actor.StartCoroutine(ActionRoutine());
        }

        public override IEnumerator ActionRoutine(){
            var card = _actor.GetFusionedCard();
            yield return new WaitForSeconds(2f);

            // if(card is MonsterCard){
            //     _Actor.Fusioner.CheckForBoardMonsterFusion(card as MonsterCard);
            // }else{
            //     //Arcane Options
            // }

            // if(_AI.Actor.MakeABoardFusion){
            //     _boardPlace = null;
            //     _boardPlace = _Actor.CardOnBoardToFusion.GetBoardPlace();
                
            //     _AI.ChangeState(_AI.CardSelect);
            // }else{
            //     // SelectFirstFreePlace(cardToPlace);
            //     SelectRandomFreePlace(cardToPlace);
            //     _AI.Actor.BoardPlaceSelected();
            // }
            
            // SelectFirstFreePlace(card);
            SelectRandomFreePlace(card);
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
}