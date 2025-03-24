using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIA_BoardPlaceSelector : AIA_Action{
        public AIA_BoardPlaceSelector(AIActor actor) : base(actor){}

        private List<BoardPlace> _monsterPlaces;
        private List<BoardPlace> _arcanePlaces;

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

            var card = _actor.GetFusionedCard();//Pegar a carta fusionada
            yield return new WaitForSeconds(1f);//Espera

            if(card is MonsterCard){//Caso seja carta de monstro
                _actor.CheckForBoardFusion(card as MonsterCard);//Verifica se há um monstro no campo com o mesmo lvl da carta recem fusionada
            }else{//Caso seja Carta Arcana

            }

            if(_actor.IsBoardFusion()){//É uma board fusion
                _actor.ReEnterCardSelectionPhase();//Re entrar na fase de seleção de carta
            }else{//Não é uma board fusion
                SelectRandomFreePlace(card);//Setar primeiro place livre
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
}