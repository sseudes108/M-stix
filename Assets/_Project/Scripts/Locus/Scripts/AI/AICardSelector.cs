using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardSelector : AIAction {
    public AICardSelector(AIActorSO actor){
        _actor = actor;
    }
    
    private List<Card> _selectedList = new();
    public List<Card> SelectedList => _selectedList;

    private List<MonsterCard> _lvl2OnHand = new(), _lvl3OnHand = new(), _lvl4OnHand = new();

    private List<MonsterCard> _lvl2OnAIField = new(), _lvl3OnAIField = new(), _lvl4OnAIField = new(), _lvl5OnAIField = new(), _lvl6OnAIField = new(), _lvl7OnAIField = new();

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand, CardsOnField cardsOnField){
        OrganizeCardsInHand(cardsInHand); //Organiza os lvls dos monstros na mão

        /*
            if(boardfusion){
                add fusioned card to selected list;
                add card on board to selected list;
            }else{
                if(cardsOnField.MonstersOnAIField.Count == 0){
                    StrongestFusionInHand();
                }else{
                    StrongestFusionInBoard(cardsOnField.MonstersOnAIField);
                }
            }
        */

        if(_actor.MakeABoardFusion){
            // add fusioned card to selected list;
            AddToSelectedList(_actor.AIManager.GetFusionedCard());
            // add card on board to selected list;
            AddToSelectedList(_actor.CardOnBoardToFusion);

            _actor.ResetBoardFusion();
        }else{
            if(cardsOnField.MonstersOnAIField.Count == 0){
                StrongestFusionInHand();
            }else{
                StrongestFusionInBoard(cardsOnField.MonstersOnAIField);
            }
        }

        // if(cardsOnField.MonstersOnAIField.Count == 0){
        //     StrongestFusionInHand();
        // }else{
        //     StrongestFusionInBoard(cardsOnField.MonstersOnAIField);
        // }

        yield return new WaitForSeconds(Random.Range(3f, 5f));
        _actor.CardSelectionFinished();
        yield return null;
    }

    private void StrongestFusionInHand(){
        // if(CanMakeALvl5()){
        //     TryMakeALvl5();
        //     return; 
        // }

        // if(CanMakeALvl4()){
        //     TryMakeALvl4();
        //     return;
        // }
        
        if(CanMakeALvl3()){
            TryMakeALvl3();
            return;
        }

        AddToSelectedList(_lvl2OnHand[0]);
    }

    private void StrongestFusionInBoard(List<MonsterCard> monstersOnAIField){
        OrganizeAIMonsterCardsOnField(monstersOnAIField);

        //lvl8
        //lvl7
        //lvl6

        if(_lvl4OnAIField.Count > 0){ //lvl5
            if(CanMakeALvl5()){
                TryMakeALvl5();
                BoardFusion(_lvl4OnAIField[0]);
                return;
            }
        }

        if(_lvl3OnAIField.Count > 0){ //lvl4
            if(CanMakeALvl4()){
                TryMakeALvl4();
                BoardFusion(_lvl3OnAIField[0]);
                return;
            }
        }

        //lvl3

        StrongestFusionInHand();
    }

#region Level 8
    // private bool CanMakeLvl8(){
    //     if(_lvl7OnAIField.Count > 0 && _lvl6OnAIField.Count > 0){
    //         if(CanMakeALvl7()){
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // private void TryMakeLvl8(){
    //     BoardFusion(_lvl6OnAIField[0]);
    //     // if(_lvl7OnAIField.Count > 0 && _lvl6OnAIField.Count > 0){// Tem um 7 e um 6 no campo
    //     //     if(CanMakeALvl7()){ // make the lvl 7 using the lvl 6 on field
    //     //         BoardFusion(_lvl6OnAIField[0]);
    //     //         //Initiate board fusion with the new lvl 7
    //     //     }
    //     // }
    // }
#endregion

#region Level 7
    // private bool CanMakeALvl7(){
    //     if(_lvl6OnAIField.Count > 0 && _lvl5OnAIField.Count > 0){ // tem um 6 no campo e um 5
    //         if(CanMakeALvl5()){ //Add all cards to select list to make a lvl 5
    //             BoardFusion(_lvl5OnAIField[0]);
    //             //Initiate board fusion with the lvl 5 on field and then, with the lvl 6 to make a lvl 7
    //         }
    //     }
    // }

    // private void TryMakeLvl7(){
    //     if(_lvl6OnAIField.Count > 0 && _lvl5OnAIField.Count > 0){ // tem um 6 no campo e um 5
    //         if(CanMakeALvl5()){ //Add all cards to select list to make a lvl 5
    //             BoardFusion(_lvl5OnAIField[0]);
    //             //Initiate board fusion with the lvl 5 on field and then, with the lvl 6 to make a lvl 7
    //         }
    //     }
    // }
#endregion

#region Level 6
    private bool CanMakeALvl6(){
        if(_lvl5OnAIField.Count > 0 && CanMakeALvl5()){// Tem um nv5 no campo e é possivel fazer outro da mão
            //Initiate Board fusion with the lvl 5 on field and the new one made from hand
        }
        return false;
    }
#endregion

#region Level 5
    private bool CanMakeALvl5(){
        if(_lvl4OnHand.Count > 1){
            // AddToSelectedList(_lvl4OnHand[0]);
            // AddToSelectedList(_lvl4OnHand[1]);
            return true;
        }

        if(_lvl3OnHand.Count >= 2 && _lvl4OnHand.Count == 1){
            // AddToSelectedList(_lvl3OnHand[0]);
            // AddToSelectedList(_lvl3OnHand[1]);
            // AddToSelectedList(_lvl4OnHand[0]);
            return true;
        }

        return false;
    }

    private void TryMakeALvl5(){
        if(_lvl4OnHand.Count > 1){
            AddToSelectedList(_lvl4OnHand[0]);
            AddToSelectedList(_lvl4OnHand[1]);
            return;
        }

        if(_lvl3OnHand.Count >= 2 && _lvl4OnAIField.Count == 1){
            AddToSelectedList(_lvl3OnHand[0]);
            AddToSelectedList(_lvl3OnHand[1]);
            AddToSelectedList(_lvl4OnAIField[0]);
            return;
        }
    }

#endregion

#region Level 4
    private bool CanMakeALvl4(){
        if(_lvl3OnHand.Count > 1){
            return true;
        }

        if(_lvl2OnHand.Count >= 2 && _lvl3OnAIField.Count == 1){
            return true;
        }

        return false;
    }

    private void TryMakeALvl4(){
        if(_lvl3OnHand.Count > 1){
            AddToSelectedList(_lvl3OnHand[0]);
            AddToSelectedList(_lvl3OnHand[1]);
            return;
        }

        if(_lvl2OnHand.Count >= 2 && _lvl3OnAIField.Count == 1){
            AddToSelectedList(_lvl2OnHand[0]);
            AddToSelectedList(_lvl2OnHand[1]);
            // AddToSelectedList(_lvl3OnAIField[0]);
            return;
        }
    }
#endregion

#region Level 3
    private bool CanMakeALvl3(){
        if(_lvl2OnHand.Count > 1){
            return true;
        }

        if(_lvl2OnAIField.Count > 0 && _lvl2OnHand.Count > 0){
            return true;
        }

        return false;
    }

    private void TryMakeALvl3(){
        if(_lvl2OnHand.Count > 1){
            AddToSelectedList(_lvl2OnHand[0]);
            AddToSelectedList(_lvl2OnHand[1]);
            return;
        }

        if(_lvl2OnHand.Count > 0 && _lvl2OnAIField.Count > 0){
            AddToSelectedList(_lvl2OnHand[0]);
            AddToSelectedList(_lvl2OnAIField[1]);
            return;
        }
    }
#endregion

    private void BoardFusion(Card cardToFusion){
        Debug.Log($"BoardFusion(Card {cardToFusion})");
        _actor.MakeABoardFusion = true;
        _actor.CardOnBoardToFusion = cardToFusion;
    }

    private void OrganizeCardsInHand(List<Card> cardsInHand){
        _lvl2OnHand.Clear();
        _lvl3OnHand.Clear();
        _lvl4OnHand.Clear();

        foreach (var card in cardsInHand){
            if (card is MonsterCard){
                if ((card as MonsterCard).Level == 2){
                    _lvl2OnHand.Add(card as MonsterCard);
                }

                if ((card as MonsterCard).Level == 3){
                    _lvl3OnHand.Add(card as MonsterCard);
                }

                if ((card as MonsterCard).Level == 4){
                    _lvl4OnHand.Add(card as MonsterCard);
                }
            }
        }
    }
    private void OrganizeAIMonsterCardsOnField(List<MonsterCard> monstersOnAIField){
        ClearAICardListsOnField();
        
        foreach (var card in monstersOnAIField){
            int lvl = card.Level;

            switch (lvl){
                case 2:
                    _lvl2OnAIField.Add(card);
                break;

                case 3:
                    _lvl3OnAIField.Add(card);
                break;

                case 4:
                    _lvl4OnAIField.Add(card);
                break;

                case 5:
                    _lvl5OnAIField.Add(card);
                break;

                case 6:
                    _lvl6OnAIField.Add(card);
                break;

                case 7:
                    _lvl7OnAIField.Add(card);
                break;
            }
        }
    }

    private void ClearAICardListsOnField(){
        _lvl2OnAIField.Clear();
        _lvl3OnAIField.Clear();
        _lvl4OnAIField.Clear();
        _lvl5OnAIField.Clear();
        _lvl6OnAIField.Clear();
        _lvl7OnAIField.Clear();
    }

    private void SelectRandomCard(List<Card> cardsInHand){
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }

    private void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }
}