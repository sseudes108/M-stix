using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mistix{
    public class AIA_CardSelector : AIA_Action{
        public AIA_CardSelector(AIActor actor) : base(actor){}
        
        private List<Card> _selectedList = new();

        public override void StartActionRoutine(){ _actor.StartCoroutine(ActionRoutine()); }

        public override IEnumerator ActionRoutine(){
            _selectedList.Clear();
            // SelectRandomCard(_actor.GetCardsInAIHand());
            _actor.OrganizeCardsOnHand();
            _actor.OrganizeAIMonsterCardsOnField();

            // if(_actor.IsBoardFusion()){
            //     _Actor.ResetBoardFusion();
            // }else{//Não é uma board fusion
            //     StrongestFusionFromHand();
            // }

            // _Actor.FieldChecker.OrganizeAIMonsterCardsOnField(_Actor.CardOrganizer.AIMonstersOnField);

            // if(_Actor.MakeABoardFusion){//É uma boardfusion

            //     _Actor.CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
            //     AddToSelectedList(_Actor.GetFusionedCard());
            //     AddToSelectedList(_Actor.CardOnBoardToFusion);

            //     _Actor.ResetBoardFusion();
            // }else{//Não é uma board fusion
            //     StrongestFusionFromHand();
            // }

            StrongestFusionFromHand();

            yield return new WaitForSeconds(2f);

            _actor.SetSelectedAICards(_selectedList);

            _actor.CardSelectionFinished();

            yield return null;
        }

        private void SelectRandomCard(List<Card> cardsInHand){
            var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
            AddToSelectedList(randomCard);
        }

        private void AddToSelectedList(Card card){ _selectedList.Add(card);}
        private void StrongestFusionFromHand(){
            if(CanMakeAlvl5FromHand()){
                TryMakeALvl5FromHand();
                return;
            }

            if(CanMakeAlvl4FromHand()){
                TryMakeALvl4FromHand();
                return;
            }

            if(CanMakeALvl3FromHand()){
                TryMakeALvl3FromHand();
                return;
            }

            // AddToSelectedList(_HandChecker.Lvl2OnHand[0]);
            AddToSelectedList(_actor.SelectLvl2Card(0));
        }

    #region Level 5
        private bool CanMakeAlvl5FromHand(){
            if(_actor.Lvl4OnHandCount() > 1){ return true; } //Mais de um lvl 4 na mão

            if(_actor.Lvl3OnHandCount() > 1 && _actor.Lvl4OnHandCount() > 0){ return true; } //Mais de um lvl 3 e um lvl 4

            if(_actor.Lvl2OnHandCount() > 1 && _actor.Lvl3OnHandCount() > 0 && _actor.Lvl4OnHandCount() > 0){ return true; } //Mais de um lvl 2, um lvl 3 e um lvl 4

            return false; //Nao é possivel fazer um lvl 5
        }

        private void TryMakeALvl5FromHand(){
            if(_actor.Lvl4OnHandCount() > 1){
                AddToSelectedList(_actor.SelectLvl4Card(0));
                AddToSelectedList(_actor.SelectLvl4Card(1));
                return;
            }

            if(_actor.Lvl3OnHandCount() > 1 && _actor.Lvl4OnHandCount() > 0){
                AddToSelectedList(_actor.SelectLvl3Card(0));
                AddToSelectedList(_actor.SelectLvl3Card(1));

                AddToSelectedList(_actor.SelectLvl4Card(0));
                return;
            }

            if(_actor.Lvl2OnHandCount() > 1 && _actor.Lvl3OnHandCount() > 0 && _actor.Lvl4OnHandCount() > 0){
                AddToSelectedList(_actor.SelectLvl2Card(0));
                AddToSelectedList(_actor.SelectLvl2Card(1));

                AddToSelectedList(_actor.SelectLvl3Card(0));
                AddToSelectedList(_actor.SelectLvl4Card(0));
                return;
            }
        }
    #endregion

    #region Level 4
        private bool CanMakeAlvl4FromHand(){
            if(_actor.Lvl3OnHandCount() > 1){ return true; }

            if(_actor.Lvl2OnHandCount() > 1 && _actor.Lvl3OnHandCount() > 0){ return true; }

            return false;
        }

        private void TryMakeALvl4FromHand(){
            if(_actor.Lvl3OnHandCount() > 1){
                AddToSelectedList(_actor.SelectLvl3Card(0));
                AddToSelectedList(_actor.SelectLvl3Card(1));
                return;
            }

            if(_actor.Lvl2OnHandCount() > 1 && _actor.Lvl3OnHandCount() > 0){
                AddToSelectedList(_actor.SelectLvl2Card(0));
                AddToSelectedList(_actor.SelectLvl2Card(1));
                AddToSelectedList(_actor.SelectLvl3Card(0));
                return;
            }
        }

    #endregion

    #region Level 3
        private bool CanMakeALvl3FromHand(){
            if(_actor.Lvl2OnHandCount() > 1){
                return true;
            }
            return false;
        }

        private void TryMakeALvl3FromHand(){
            AddToSelectedList(_actor.SelectLvl2Card(0));
            AddToSelectedList(_actor.SelectLvl2Card(1));
        }
    #endregion

    }
}