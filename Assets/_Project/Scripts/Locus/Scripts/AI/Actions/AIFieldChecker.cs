using System.Collections.Generic;
using UnityEngine;

public class AIFieldChecker : AIAction {
    public AIFieldChecker(AIActorSO actor){
        _actor = actor;
    }

    //AI HAND MONSTERS
    public List<MonsterCard> Lvl2OnHand {get; private set;} = new();
    public List<MonsterCard> Lvl3OnHand {get; private set;} = new();
    public List<MonsterCard> Lvl4OnHand {get; private set;} = new();

    //AI FIELD MONSTERS
    public List<MonsterCard> Lvl2OnAIField {get; private set;} = new();
    public List<MonsterCard> Lvl3OnAIField {get; private set;} = new();
    public List<MonsterCard> Lvl4OnAIField {get; private set;} = new();
    public List<MonsterCard> Lvl5OnAIField {get; private set;} = new();
    public List<MonsterCard> Lvl6OnAIField {get; private set;} = new();
    public List<MonsterCard> Lvl7OnAIField {get; private set;} = new();


    //PLAYER FIELD MONSTERS
    public List<MonsterCard> Lvl2OnPlayerField {get; private set;} = new();
    public List<MonsterCard> Lvl3OnPlayerField {get; private set;} = new();
    public List<MonsterCard> Lvl4OnPlayerField {get; private set;} = new();
    public List<MonsterCard> Lvl5OnPlayerField {get; private set;} = new();
    public List<MonsterCard> Lvl6OnPlayerField {get; private set;} = new();
    public List<MonsterCard> Lvl7OnPlayerField {get; private set;} = new();

    public void OrganizeCardsOnHand(List<Card> cardsInHand){
        ClearHandLists();

        foreach (var card in cardsInHand){
            if (card is MonsterCard){
                int lvl = (card as MonsterCard).Level;

                if (lvl == 2){
                    Lvl2OnHand.Add(card as MonsterCard);
                }

                if (lvl == 3){
                    Lvl3OnHand.Add(card as MonsterCard);
                }

                if (lvl == 4){
                    Lvl4OnHand.Add(card as MonsterCard);
                }
            }
        }
    }

    public void OrganizeAIMonsterCardsOnField(List<MonsterCard> monstersOnAIField){
        ClearAIListsOnField();

        foreach(var card in monstersOnAIField){
            int lvl = card.Level;

            switch (lvl){
                case 2:
                    Lvl2OnAIField.Add(card);
                break;

                case 3:
                    Lvl3OnAIField.Add(card);
                break;

                case 4:
                    Lvl4OnAIField.Add(card);
                break;

                case 5:
                    Lvl5OnAIField.Add(card);
                break;

                case 6:
                    Lvl6OnAIField.Add(card);
                break;

                case 7:
                    Lvl7OnAIField.Add(card);
                break;
            }
        }
    }

    private void ClearAIListsOnField(){
        Lvl2OnAIField.Clear();
        Lvl3OnAIField.Clear();
        Lvl4OnAIField.Clear();
        Lvl5OnAIField.Clear();
        Lvl6OnAIField.Clear();
        Lvl7OnAIField.Clear();

        Lvl2OnPlayerField.Clear();
        Lvl3OnPlayerField.Clear();
        Lvl4OnPlayerField.Clear();
        Lvl5OnPlayerField.Clear();
        Lvl6OnPlayerField.Clear();
        Lvl7OnPlayerField.Clear();
    }

    private void ClearHandLists(){
        Lvl2OnHand.Clear();
        Lvl3OnHand.Clear();
        Lvl4OnHand.Clear();
    }
}