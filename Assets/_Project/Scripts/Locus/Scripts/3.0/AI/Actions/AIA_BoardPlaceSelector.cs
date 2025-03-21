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
            _monsterPlaces = monsterPlaces;
            _arcanePlaces = arcanePlaces; 
        }

        public override void StartActionRoutine(){
            _actor.StartCoroutine(ActionRoutine());
        }

        public override IEnumerator ActionRoutine(){
            /*
            Limpar board place a ser usado
            Verificar tipo de carta
                Caso Monstro:
                    Verifica se há um monstro no campo com o mesmo lvl da carta recem fusionada
                        Caso haja:
                            Setar configuração de board fusion e carta no campo para ser usada na fusão
                        Caso nao:
                            Continuar
                Caso Arcana:
                    ~~~~~~
            Verificar se é uma board fusion
                Caso sim:
                    Re entrar na fase de seleção de carta
                Caso não:
                    Setar primeiro place livre
            Encerrar ação de seleção de place
            */       

            yield return null;


            // _actor.ResetBoardFusion();
            
            // var card = _actor.GetFusionedCard();
            // yield return new WaitForSeconds(2f);

            // if(card is MonsterCard){
            //     _actor.CheckForBoardFusion(card as MonsterCard); //Verifica que há monstros no campo que tenham o mesmo lvl do mostro feito agora
            // }else{
            //     //Arcane
            // }

            // if(_actor.IsBoardFusion()){
            //     _actor.ReEnterCardSelectionPhase();
            // }else{
            //     SelectRandomFreePlace(card);
            // }

            // yield return null;

            // if(_actor.IsBoardFusion()){
            //     _boardPlace = null;
            //     _boardPlace = _actor.GetCardOnBoardToFusion().GetBoardPlace();
            //     _actor.ReEnterCardSelectionPhase();
            // }else{
            //     SelectRandomFreePlace(card);
            // }

            // yield return null;

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
            // SelectRandomFreePlace(card);
            // yield return null;
        }

        // private void BoardFusionPlace(BoardPlace place, Card card){
        //     place.SetCardInPlace(card);
        // }

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