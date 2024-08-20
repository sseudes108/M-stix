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

    public override void Enter(){
        SplitCardsOnBoardByType(); //Verifica quais as cartas em campo
        StateMachine.AI.StartCoroutine(AIRoutine());
    }

    public override void Exit(){}

    public IEnumerator AIRoutine(){
        yield return new WaitForSeconds(0.5f);
        StateMachine.AI.StartCoroutine(StateMachine.AI.Actor.CardSelector.SelectCardRoutine(StateMachine.AI.Manager.CardsInHand, _cardsOnField));
        yield return null;
    }

    public override string ToString(){
        return "Select Card";
    }

    private void SplitCardsOnBoardByType(){
        foreach(var card in StateMachine.AI.CardsOnAIField){
            if(card is MonsterCard){
                _monstersOnAIField.Add(card as MonsterCard);
            }else{
                // _arcanesOnAIField.Add(card as ArcaneCard);
            }
        }

        foreach(var card in StateMachine.AI.CardsOnPlayerField){
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

        Debug.Log($"MonstersOnAIField - {_cardsOnField.MonstersOnAIField.Count}");
        Debug.Log(_cardsOnField.MonstersOnPlayerField.Count);
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