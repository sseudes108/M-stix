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

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand, CardsOnField cardsOnField){
        OrganizeCardsInHand(cardsInHand); //Organiza os lvls dos monstros na mão

        if(cardsOnField.MonstersOnAIField.Count == 0){
            Debug.Log("cardsOnField.MonstersOnAIField.Count == 0");
            if(_lvl4OnHand.Count >= 2){
                Debug.Log("_lvl4OnHand.Count >= 2");
                AddToSelectedList(_lvl4OnHand[0]);
                AddToSelectedList(_lvl4OnHand[1]);
                //Fusion 2 lvl 4 to form a lvl 5

            }else if(_lvl3OnHand.Count >= 2){
                Debug.Log("_lvl3OnHand.Count >= 2");
                //Fusion 2 lvl 3 to form a lvl 4
                AddToSelectedList(_lvl3OnHand[0]);
                AddToSelectedList(_lvl3OnHand[1]);

                //Add a lvl 4 to make a lvl 5
                if(_lvl4OnHand.Count == 1){
                    AddToSelectedList(_lvl4OnHand[0]);
                }

            }else if(_lvl2OnHand.Count >= 2){
                Debug.Log("_lvl2OnHand.Count >= 2");
                //Fusion 2 lvl 2 to form a lvl 3
                AddToSelectedList(_lvl2OnHand[0]);
                AddToSelectedList(_lvl2OnHand[1]);

                //Add a lvl 4 to make a lvl 5
                if(_lvl3OnHand.Count == 1){
                    AddToSelectedList(_lvl3OnHand[0]);
                }
            }
        }

        // SelectRandomCard(cardsInHand);
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        _actor.CardSelectionFinished();
        yield return null;
    }

    private void OrganizeCardsInHand(List<Card> cardsInHand)    {
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

        Debug.Log(_lvl2OnHand.Count);
        Debug.Log(_lvl3OnHand.Count);
        Debug.Log(_lvl4OnHand.Count);
    }

    private void SelectRandomCard(List<Card> cardsInHand){
        var randomCard = cardsInHand[Random.Range(0, cardsInHand.Count)];
        AddToSelectedList(randomCard);
    }

    private void AddToSelectedList(Card card){
        _selectedList.Add(card);
    }
}