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

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand, CardsOnField cardsOnField){

        /*
            Limpar lista de cartas selecionadas
            Existem monstros em campo?
                Não -> Faz Invoca o monstro mais forte possivel da mão
                Sim -> 
                    Verificar qual lista de monstros AI em field checker não está vazia
                    é possivel fazer um monstro um nivel mais forte do que o mais forte em campo? (Existe um level 5 em campo. É possivel fazer um nivel 6?)
                        Sim -> Funde o monstro e reinicia o processo
                        Não -> Verifica o proximo level na lista (Seguindo o exemplo, verifica se tem um nv4 em campo e se é possivel fazer um nivel 5)
                            Sim -> Funde o monstro e reinicia o processo
                            Não -> Verifica o proximo level na lista
        */

        _selectedList.Clear();

        if(_actor.MakeABoardFusion){//É uma boardfusion
            _actor.CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();

            AddToSelectedList(_actor.AIManager.GetFusionedCard());
            AddToSelectedList(_actor.CardOnBoardToFusion);

            _actor.ResetBoardFusion();

        }else{//Não é uma board fusion
            if(cardsOnField.MonstersOnAIField.Count == 0){//Não existe nenhum monstro em campo
                StrongestFusionFromHand();//Faz o monstro mais forte possivel da mão
            }else{//Existe ao menos um monstro em campo
                CheckBoardFusion();
            }
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
    
    /// <summary>
    /// cardToFusion is the card on the field the will be used after the fusion from hand
    /// </summary>
    private void BoardFusion(Card cardToFusion){
        _actor.BoardManager.BoardFusion();
        _actor.MakeABoardFusion = true;
        _actor.CardOnBoardToFusion = cardToFusion;
    }

    private void CheckBoardFusion(){
        MonsterCard lastFusionedMonster = null;
        int lastFusionedMonsterLevel = 0;

        if(_actor.AIManager.GetFusionedCard() != null){//Uma fusão foi anteriomente feita
            if(!_actor.AIManager.GetFusionedCard().IsPlayerCard){//A Carta resultado nao é do player, ou seja, feita pela IA
                lastFusionedMonster = _actor.AIManager.GetFusionedCard() as MonsterCard;
                lastFusionedMonsterLevel = lastFusionedMonster.Level;
            }
        }
        
        if(_fieldChecker.Lvl4OnAIField.Count > 0){
            if(CanMakeAlvl4FromHand()){//é possivel fazer outro nivel 4 da mão
                TryMakeALvl4FromHand();
                BoardFusion(_fieldChecker.Lvl4OnAIField[0]);
                return;
            }else if(CanMakeALvl4OnBoard()){//é possivel fazer outro nivel 4 utilizando um nivel 3 em campo
                TryMakeALvl4OnBoard();
                BoardFusion(_fieldChecker.Lvl3OnAIField[0]);
                return;
            }

            if(lastFusionedMonsterLevel == 4){
                AddToSelectedList(lastFusionedMonster);
                BoardFusion(_fieldChecker.Lvl4OnAIField[0]);
                return;
            }
        }

        if(_fieldChecker.Lvl3OnAIField.Count > 0){
            if(CanMakeALvl3FromHand()){//é possivel fazer outro nivel 3 da mão
                TryMakeALvl3FromHand();
                BoardFusion(_fieldChecker.Lvl3OnAIField[0]);
                return;
            }else if(CanMakeALvl3OnBoard()){//é possivel fazer outro nivel 3 utilizando um nivel 2 em campo
                TryMakeALvl3OnBoard();
                BoardFusion(_fieldChecker.Lvl3OnAIField[0]);
                return;
            }
        }

        //Caso nada seja realizado a fusão mais forte da mão é chamada
        StrongestFusionFromHand();
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

    private bool CanMakeALvl4OnBoard(){
        if(_fieldChecker.Lvl3OnAIField.Count > 1 && CanMakeALvl3FromHand()){
            return true;
        }else{
            return false;
        }
    }

    private void TryMakeALvl4OnBoard(){
        AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
        AddToSelectedList(_fieldChecker.Lvl2OnHand[1]);
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

    private bool CanMakeALvl3OnBoard(){
        if(_fieldChecker.Lvl2OnAIField.Count > 0 && _fieldChecker.Lvl2OnHand.Count > 0){
            return true;
        }

        return false;
    }

    private void TryMakeALvl3OnBoard(){
        AddToSelectedList(_fieldChecker.Lvl2OnHand[0]);
        AddToSelectedList(_fieldChecker.Lvl2OnHand[1]);
    }

#endregion

}