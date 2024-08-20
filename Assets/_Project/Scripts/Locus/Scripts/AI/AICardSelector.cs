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

        if(cardsOnField.MonstersOnAIField.Count == 0){
            StrongestFusionInHand();
        }else{
            OrganizeAIMonsterCardsOnField(cardsOnField.MonstersOnAIField);

            if(_lvl7OnAIField.Count > 0){
                //Can make another lvl7 to make a 8?
            }

            if(_lvl6OnAIField.Count > 0){
                //Can make another lvl6 to make a 7?
            }

            if(_lvl5OnAIField.Count > 0){
                //Can make another lvl5 to make a 6?
            }

            if(_lvl4OnAIField.Count > 0){
                //Can make another lvl4 to make a 5?
            }

            if(_lvl3OnAIField.Count > 0){
                //Can make another lvl3 to make a 4?
            }

            if(_lvl2OnAIField.Count > 0){
                //Can make another lvl2 to make a 3?
            }
        }

        // SelectRandomCard(cardsInHand);
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        _actor.CardSelectionFinished();
        yield return null;
    }

    private void StrongestFusionInHand(){
        if(CanMakeALvl5()){
            return;
        }

        if(CanMakeALvl4()){
            return;
        }

        if(CanMakeALvl3()){
            return;
        }

        AddToSelectedList(_lvl2OnHand[0]);

        // if (_lvl4OnHand.Count >= 2){

        //     AddToSelectedList(_lvl4OnHand[0]);
        //     AddToSelectedList(_lvl4OnHand[1]);
        //     //Fusion 2 lvl 4 to form a lvl 5

        // }else if (_lvl3OnHand.Count >= 2){

        //     //Fusion 2 lvl 3 to form a lvl 4
        //     AddToSelectedList(_lvl3OnHand[0]);
        //     AddToSelectedList(_lvl3OnHand[1]);

        //     //Add a lvl 4 to make a lvl 5
        //     if (_lvl4OnHand.Count == 1){
        //         AddToSelectedList(_lvl4OnHand[0]);
        //     }
        // }else if (_lvl2OnHand.Count >= 2){

        //     //Fusion 2 lvl 2 to form a lvl 3
        //     AddToSelectedList(_lvl2OnHand[0]);
        //     AddToSelectedList(_lvl2OnHand[1]);

        //     //Add a lvl 3 to make a lvl 4
        //     if (_lvl3OnHand.Count == 1)
        //     {
        //         AddToSelectedList(_lvl3OnHand[0]);
        //     }
        // }
    }

    private bool CanMakeALvl8(){
        if(_lvl7OnAIField.Count > 0 && _lvl6OnAIField.Count > 0){// Tem um 7 e um 6 no campo
            if(CanMakeALvl7()){ // make the lvl 7 using the lvl 6 on field
                //Initiate board fusion with the new lvl 7
            }
        }

        return false;
    }

    private bool CanMakeALvl7(){
        if(_lvl6OnAIField.Count > 0 && _lvl5OnAIField.Count > 0){ // tem um 6 no campo e um 5
            if(CanMakeALvl5()){ //Add all cards to select list to make a lvl 5
                //Initiate board fusion with the lvl 5 on field and then, with the lvl 6 to make a lvl 7
                return true;
            }
        }

        return false;
    }

    private bool CanMakeALvl6(){
        if(_lvl5OnAIField.Count > 0 && CanMakeALvl5()){// Tem um nv5 no campo e é possivel fazer outro da mão
            //Initiate Board fusion with the lvl 5 on field and the new one made from hand
        }
        return false;
    }

    private bool CanMakeALvl5(){
        if(_lvl4OnHand.Count > 1){
            AddToSelectedList(_lvl4OnHand[0]);
            AddToSelectedList(_lvl4OnHand[1]);
            return true;
        }

        if(_lvl3OnHand.Count >= 2 && _lvl4OnHand.Count == 1){
            AddToSelectedList(_lvl3OnHand[0]);
            AddToSelectedList(_lvl3OnHand[1]);
            AddToSelectedList(_lvl4OnHand[0]);
            return true;
        }

        return false;
    }

    private bool CanMakeALvl4(){
        if(_lvl3OnHand.Count > 1){
            AddToSelectedList(_lvl3OnHand[0]);
            AddToSelectedList(_lvl3OnHand[1]);
            return true;
        }

        if(_lvl2OnHand.Count >= 2 && _lvl3OnHand.Count == 1){
            AddToSelectedList(_lvl2OnHand[0]);
            AddToSelectedList(_lvl2OnHand[1]);
            AddToSelectedList(_lvl3OnHand[0]);
            return true;
        }

        return false;
    }

    private bool CanMakeALvl3(){
        if(_lvl2OnHand.Count > 1){
            AddToSelectedList(_lvl2OnHand[0]);
            AddToSelectedList(_lvl2OnHand[1]);
            return true;
        }

        return false;
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
        _lvl2OnAIField.Clear();
        _lvl3OnAIField.Clear();
        _lvl4OnAIField.Clear();
        _lvl5OnAIField.Clear();
        _lvl6OnAIField.Clear();
        _lvl7OnAIField.Clear();

        foreach (var card in monstersOnAIField){
            if (card.Level == 2){
                _lvl2OnAIField.Add(card);
            }

            if (card.Level == 3){
                _lvl3OnAIField.Add(card);
            }

            if (card.Level == 4){
                _lvl4OnAIField.Add(card);
            }

            if (card.Level == 5){
                _lvl5OnAIField.Add(card);
            }

            if (card.Level == 6){
                _lvl6OnAIField.Add(card);
            }

            if (card.Level == 7){
                _lvl7OnAIField.Add(card);
            }
        }
    }

    private void SelectRandomCard(List<Card> cardsInHand){
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }

    private void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }
}