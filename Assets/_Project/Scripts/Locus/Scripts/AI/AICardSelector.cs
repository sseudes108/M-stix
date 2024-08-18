using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICardSelector : AIAction {
    public AICardSelector(AIActorSO actor){
        _actor = actor;
    }
    
    private List<Card> _selectedList = new();
    public List<Card> SelectedList => _selectedList;

    // public List<int> _monsterLvlsOnField;

    public Dictionary<int, MonsterCard> _monsterLvlsOnField = new();
    public Dictionary<int, MonsterCard> _monsterLvlsOnHand = new();
    public List<MonsterCard> _matchingLevelMonsters = new();

    public IEnumerator SelectCardRoutine(List<Card> cardsInHand, CardsOnField cardsOnField){
        // SelectRandomCard(cardsInHand);

        if(cardsOnField.MonstersOnAIField.Count != 0){
            foreach(var monster in cardsOnField.MonstersOnAIField){
                _monsterLvlsOnField.Add(monster.Level, monster);
            }
        }

        foreach(var card in cardsInHand){
            if(card is MonsterCard){
                _monsterLvlsOnHand.Add((card as MonsterCard).Level, card as MonsterCard);
            }
        }

        foreach(var monsterOnField in _monsterLvlsOnField){
            var levelOnField = monsterOnField.Key;

            if(_monsterLvlsOnHand.ContainsKey(levelOnField)){
                _matchingLevelMonsters.Add(_monsterLvlsOnHand[levelOnField]);
            }
        }

        _matchingLevelMonsters.Sort((x,y) => y.Level.CompareTo(x.Level));

        yield return new WaitForSeconds(Random.Range(3f, 5f));
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
}