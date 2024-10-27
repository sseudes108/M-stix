using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : AIAction{
    public AICardSelector(AIActorSO actor){
        _actor = actor;
        _fieldChecker = _actor.FieldChecker;
    }
    
    private List<Card> _selectedList = new();
    public List<Card> SelectedList => _selectedList;

    private AIFieldChecker _fieldChecker;

    public IEnumerator SelectCardRoutine(){
        _selectedList.Clear();

        if(_actor.MakeABoardFusion){//É uma boardfusion
            _actor.CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();

            AddToSelectedList(_actor.AIManager.GetFusionedCard());
            AddToSelectedList(_actor.CardOnBoardToFusion);

            _actor.Fusioner.ResetBoardFusion();
        }else{//Não é uma board fusion
            StrongestFusionFromHand();
        }

        yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        _actor.CardSelectionFinished();
        yield return null;
    }

    private void SelectRandomCard(List<Card> cardsInHand){
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }
    private void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }
    private void StrongestFusionFromHand(){
        if(CanMakeAlvl4FromHand()){
            TryMakeALvl4FromHand();
            return;
        }

        if(CanMakeALvl3FromHand()){
            TryMakeALvl3FromHand();
            return;
        }

        AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
    }
    
#region Level 4
    private bool CanMakeAlvl4FromHand(){
        if(_fieldChecker.Lvl3OnHand.Count > 1){
            return true;
        }

        if(_fieldChecker.Lvl2OnHand.Count > 1 && _fieldChecker.Lvl3OnHand.Count > 0){
            return true;
        }

        return false;
    }

    private void TryMakeALvl4FromHand(){
        if(_fieldChecker.Lvl3OnHand.Count > 1){
            AddToSelectedList(_fieldChecker.Lvl3OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl3OnHand[1]);
        }

        if(_fieldChecker.Lvl2OnHand.Count > 1 && _fieldChecker.Lvl3OnHand.Count > 0){
            AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl2OnHand[1]);
            AddToSelectedList(_fieldChecker.Lvl3OnHand[0]);
        }
    }

#endregion

#region Level 3
    private bool CanMakeALvl3FromHand(){
        if(_fieldChecker.Lvl2OnHand.Count > 1){
            return true;
        }
        return false;
    }

    private void TryMakeALvl3FromHand(){
        AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
        AddToSelectedList(_fieldChecker.Lvl2OnHand[1]);
    }
#endregion

}