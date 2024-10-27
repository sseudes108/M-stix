using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISelectCardState : AbstractState{
    public AISelectCardState(StateMachine controller) : base(controller){}

    private List<MonsterCard> _monstersOnAIField = new();
    private List<MonsterCard> _monsterOnPlayerField = new();

    // private List<ArcaneCard> _arcanesOnAIField = new();
    // private List<ArcaneCard> _arcanesOnPlayerField = new();

    private CardsOnField _cardsOnField;
    // public CardsOnField CardsOnField => _cardsOnField;
    private List<Card> _cardsInHand;
    // public List<Card> CardsInHand => _cardsInHand;

    private AI _AI;

    public override void Enter(){
        _AI = StateMachine.AI;
        
        SplitCardsOnBoardByType(); //Verifica quais as cartas em campo
        _AI.StartCoroutine(AIRoutine());
    }

    public override void Exit(){}

    public IEnumerator AIRoutine(){
        yield return new WaitForSeconds(0.5f);
        _AI.Actor.UpdateCardLists(_AI.Manager.CardsInHand, _cardsOnField.MonstersOnAIField);
        _AI.StartCoroutine(_AI.Actor.CardSelector.SelectCardRoutine());
        yield return null;
    }

    public override string ToString(){
        return "Select Card";
    }

    public void SplitCardsOnBoardByType(){
        _monstersOnAIField.Clear();
        _monsterOnPlayerField.Clear();
        // _arcanesOnAIField.Clear();
        // _arcanesOnPlayerField.Clear();

        foreach(var card in _AI.Actor.CardsOnAIField){
            if(card is MonsterCard){
                _monstersOnAIField.Add(card as MonsterCard);
            }else{
                // _arcanesOnAIField.Add(card as ArcaneCard);
            }
        }

        foreach(var card in _AI.Actor.CardsOnPlayerField){
            if(card is MonsterCard){
                _monsterOnPlayerField.Add(card as MonsterCard);
            }else{
                // _arcanesOnPlayerField.Add(card as ArcaneCard);
            }
        }

        _cardsOnField = null;
        _cardsOnField = new(
            _monsterOnPlayerField, 
            _monstersOnAIField
        );
    }
}

public class CardsOnField{
    public List<MonsterCard> MonstersOnPlayerField;
    // public List<MonsterCard> ArcanesOnPlayerField;

    public List<MonsterCard> MonstersOnAIField;
    // public List<MonsterCard> ArcanesOnAIField;
    public CardsOnField(
        List<MonsterCard> monstersOnPlayerField,
        List<MonsterCard> monstersOnAIField
    ){
        MonstersOnPlayerField = monstersOnPlayerField;
        MonstersOnAIField = monstersOnAIField;
    }
}