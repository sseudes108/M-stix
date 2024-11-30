using System.Collections.Generic;
using UnityEngine;

public class AICardOrganizer : MonoBehaviour {
    [field:SerializeField] public AIActor Actor { get; private set; }
    [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }
    

    public List<Card> AIMonstersInHand { get; private set; } = new();
    public List<Card> AIArcanesInHand { get; private set; } = new();
    public List<MonsterCard> AIMonstersOnField { get; private set; } = new();
    public List<ArcaneCard> AIArcanesOnField { get; private set; } = new();
    // public List<MonsterCard> AIMonstersOnFieldThatCanAttack { get; private set; } = new();

    public List<MonsterCard> PlayerMonstersOnField { get; private set; } = new();
    public List<ArcaneCard> PlayerArcanesOnField { get; private set; } = new();

    public MonsterCard AttackingMonster { get; private set; }

    public List<Card> CardsInHand { get; private set; } = new(){};

    private void Awake() { Actor = GetComponent<AIActor>(); }
    
    public void OnDisable() { CardsInHand.Clear(); }

    public void SetAICardsOnField(List<Card> aiCardOnField){
        Debug.Log("SetAICardsOnField");
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
        Debug.Log("SetPlayerCardsOnField");
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
    
    public void SetCardsInHand(List<Card> cardsInHand) { CardsInHand = cardsInHand; }

    private void ClearAILists(){
        AIMonstersOnField.Clear();
        AIArcanesOnField.Clear();
    }

    private void ClearPlayerLists(){
        PlayerMonstersOnField.Clear();
        PlayerArcanesOnField.Clear();
    }
}