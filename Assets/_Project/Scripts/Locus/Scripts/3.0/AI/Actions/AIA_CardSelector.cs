using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIA_CardSelector{
        public AIA_CardSelector(AIActor actor){
            _actor = actor;
        }

        private AIActor _actor;
        private List<Card> _selectedList = new();

        public void StartCardSelectionRoutine(){
            _actor.StartCoroutine(CardSelectionRoutine());
        }

        public IEnumerator CardSelectionRoutine(){
            _selectedList.Clear();
            SelectRandomCard(_actor.GetCardsInAIHand());

            // _Actor.FieldChecker.OrganizeAIMonsterCardsOnField(_Actor.CardOrganizer.AIMonstersOnField);

            // if(_Actor.MakeABoardFusion){//É uma boardfusion

            //     _Actor.CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
            //     AddToSelectedList(_Actor.GetFusionedCard());
            //     AddToSelectedList(_Actor.CardOnBoardToFusion);

            //     _Actor.ResetBoardFusion();
            // }else{//Não é uma board fusion
            //     StrongestFusionFromHand();
            // }

            // yield return new WaitForSeconds(2f);
            // _Actor.CardSelectionFinished();

            Debug.Log($"Selected - { _selectedList[0].Name}");
            yield return null;
        }

        private void SelectRandomCard(List<Card> cardsInHand){
            var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
            AddToSelectedList(randomCard);
        }
        private void AddToSelectedList(Card card) { _selectedList.Add(card);}
    }
}