using System.Collections.Generic;
using UnityEngine;

public class AICardOrganizer : MonoBehaviour {
    public List<Card> CardsInAIHand { get; private set; } = new(){};
    public List<Card> AIMonstersInHand { get; private set; } = new();
    public List<Card> AIArcanesInHand { get; private set; } = new();
    public List<MonsterCard> AIMonstersOnField { get; private set; } = new();
    public List<ArcaneCard> AIArcanesOnField { get; private set; } = new();

    public List<MonsterCard> PlayerMonstersOnField { get; private set; } = new();
    public List<ArcaneCard> PlayerArcanesOnField { get; private set; } = new();

    // public List<MonsterCard> PlayerMonsterOnFieldByLevel { get; private set; } = new();
    // public List<MonsterCard> PlayerMonsterOnFieldByAttack { get; private set; } = new();
    // public List<MonsterCard> PlayerMonsterOnFieldByDeffense { get; private set; } = new();

    public void OnDisable() { CardsInAIHand.Clear(); }

    public void SetAICardsOnField(List<Card> aiCardOnField){
        ClearAILists();
        foreach(var card in aiCardOnField){
            if(card is MonsterCard){
                AIMonstersOnField.Add(card as MonsterCard);
            }else{
                AIArcanesOnField.Add(card as ArcaneCard);
            }
        }
    }

    public void SetPlayerCardsOnField(List<Card> playerMonstersOnField){
        ClearPlayerLists();
        foreach(var card in playerMonstersOnField){
            if(card is MonsterCard){
                if(!PlayerMonstersOnField.Contains(card as MonsterCard)){
                    PlayerMonstersOnField.Add(card as MonsterCard);
                }
            }else{
                if(!PlayerArcanesOnField.Contains(card as ArcaneCard)){
                    PlayerArcanesOnField.Add(card as ArcaneCard);
                }
            }
        }
    }
    
    public void SetCardsInAIHand(List<Card> cardsInHand) {
        AIMonstersInHand.Clear();
        AIArcanesInHand.Clear();

        foreach(var card in cardsInHand) {
            if(card is MonsterCard){
                AIMonstersInHand.Add(card as MonsterCard);
            }else{
                AIArcanesInHand.Add(card as ArcaneCard);
            }
        }

        CardsInAIHand = cardsInHand; 
    }

    private void ClearAILists(){
        AIMonstersOnField.Clear();
        AIArcanesOnField.Clear();
    }

    private void ClearPlayerLists(){
        PlayerMonstersOnField.Clear();
        PlayerArcanesOnField.Clear();
    }
}