using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Essa classe é chamada diversas vezes por turno. É chamada pela state machine de batalha e novamente pelo boardplace ao verificar a possibilidade de realizar uma fusao com um monstro em campo, onde é ativada a board fusion, setando a nova carta e ajustando as opções na carta em campo (boardplace, cardtofusion)
    Ela verifica se a opção de board fusion esta ativa, seleciona devidamente as cartas e termina a selecão iniciando novamente a fusão, que sequencialmente vai cair no board ourtra vez. O processo é repetido até nao ser mais possivel fundir uma carta de nivel maior usando as do campo e a fundida por ultimo.
*/

public class AICardSelector : AIAction{
    public AICardSelector(AIActorSO actor){
        _actor = actor;
        _fieldChecker = _actor.FieldChecker;
    }

    public List<Card> SelectedList { get; private set; } = new();
    
    private AIFieldChecker _fieldChecker;


    public IEnumerator SelectCardRoutine(){
        SelectedList.Clear();

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
        SelectedList.Add(card);
    }
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

        AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
    }

#region Level5
    private bool CanMakeAlvl5FromHand(){
        if(_fieldChecker.Lvl4OnHand.Count > 1){
            return true;
        }

        if(_fieldChecker.Lvl3OnHand.Count > 1 && _fieldChecker.Lvl4OnHand.Count > 0){
            return true;
        }

        if(_fieldChecker.Lvl2OnHand.Count > 1 && _fieldChecker.Lvl3OnHand.Count > 0 && _fieldChecker.Lvl4OnHand.Count > 0){
            return true;
        }

        return false;
    }

    private void TryMakeALvl5FromHand(){
        if(_fieldChecker.Lvl4OnHand.Count > 1){
            AddToSelectedList(_fieldChecker.Lvl4OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl4OnHand[1]);
            return;
        }

        if(_fieldChecker.Lvl3OnHand.Count > 1 && _fieldChecker.Lvl4OnHand.Count > 0){
            AddToSelectedList(_fieldChecker.Lvl3OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl3OnHand[1]);

            AddToSelectedList(_fieldChecker.Lvl4OnHand[0]);
            return;
        }

        if(_fieldChecker.Lvl2OnHand.Count > 1 && _fieldChecker.Lvl3OnHand.Count > 0 && _fieldChecker.Lvl4OnHand.Count > 0){
            AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl2OnHand[1]);

            AddToSelectedList(_fieldChecker.Lvl3OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl4OnHand[0]);
            return;
        }
    }
#endregion

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
            return;
        }

        if(_fieldChecker.Lvl2OnHand.Count > 1 && _fieldChecker.Lvl3OnHand.Count > 0){
            AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
            AddToSelectedList(_fieldChecker.Lvl2OnHand[1]);
            AddToSelectedList(_fieldChecker.Lvl3OnHand[0]);
            return;
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