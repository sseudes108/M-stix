using System.Collections.Generic;
using UnityEngine;

public class AI : StateMachine {
    [field:SerializeField] public AIManagerSO Manager {get; private set;}
    [field:SerializeField] public AIActorSO Actor {get; private set;}

    public AbstractState CurrentState;

    //States
    public AISelectCardState CardSelect {get; private set;}
    public AICardStatSelState CardStatSelect {get; private set;}
    public AIBoardPlaceSelState BoardPlaceSelect {get; private set;}

    public AI(){
        CardSelect??= new(this);
        CardStatSelect??= new(this);
        BoardPlaceSelect??= new(this);
    }

    private void Start(){
        Manager.SetAI(this);
        Actor.SetAIMAnager(Manager);
        Actor.ResetBoardFusion();

        // Actor.FieldChecker.SubscribeEvents();
    }

    // private void OnDisable() {
    //     Actor.FieldChecker.UnsubscribeEvents();
    // }

    public void ChangeState(AbstractState newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
        
        TesterUI.Instance.UpdateAIStateText(CurrentState.ToString());
    }

    public void SetAIOnBoardLists(List<Card> aICardsOnField, List<Card> playerCardsOnField){
        Actor.SetAICardLists(aICardsOnField, playerCardsOnField);
    }

    // public void SetAICardsOnField(List<Card> aICardsOnField){
    //     Actor. = aICardsOnField;
    // }

    public void SplitCardsOnBoardByType(){
        CardSelect.SplitCardsOnBoardByType();
    }

    // public void OrganizeCardsOnHandAndField(List<Card> cardsInHand, List<MonsterCard> monstersOnAIField){
    //     CardSelect.SplitCardsOnBoardByType();

    //     Actor.FieldChecker.OrganizeCardsOnHand(cardsInHand);

    //     Actor.FieldChecker.OrganizeAIMonsterCardsOnField(monstersOnAIField);
    // }
}