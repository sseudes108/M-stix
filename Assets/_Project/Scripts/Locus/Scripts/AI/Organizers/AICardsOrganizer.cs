using System.Collections.Generic;
using UnityEngine;

public class AICardOrganizer : MonoBehaviour {
    [field:SerializeField] public AIActor Actor { get; private set; }
    [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }

    public List<Card> AIMonstersInHand { get; private set; } = new();
    public List<Card> AIArcanesInHand { get; private set; } = new();
    public List<MonsterCard> AIMonstersOnField { get; private set; } = new();
    public List<ArcaneCard> AIArcanesOnField { get; private set; } = new();
    public List<MonsterCard> AIMonstersOnFieldThatCanAttack { get; private set; } = new();

    public List<MonsterCard> PlayerMonstersOnField { get; private set; } = new();
    public List<ArcaneCard> PlayerArcanesOnField { get; private set; } = new();

    public Card CardOnBoardToFusion { get; private set; }
    public MonsterCard AttackingMonster { get; private set; }

    public List<Card> CardsInHand = new(){};

    private void Awake() { Actor = GetComponent<AIActor>(); }
    
    public void OnDisable() { CardsInHand.Clear(); }

    public void SetCardsAIInHand(List<Card> aiCardsInHand){
        foreach(var card in aiCardsInHand){
            if(card is MonsterCard){
                AIMonstersInHand.Add(card as MonsterCard);
            }else{
                AIArcanesInHand.Add(card as ArcaneCard);
            }
        }
    }

    public void SetAICardsOnField(List<Card> aiCardOnField){
        foreach(var card in aiCardOnField){
            if(card is MonsterCard){
                AIMonstersOnField.Add(card as MonsterCard);
            }else{
                AIArcanesOnField.Add(card as ArcaneCard);
            }
        }
    }

    public void SetPlayerCardsOnField(List<Card> playerMonstersOnField){
        foreach(var card in playerMonstersOnField){
            if(card is MonsterCard){
                PlayerMonstersOnField.Add(card as MonsterCard);
            }else{
                PlayerArcanesOnField.Add(card as ArcaneCard);
            }
        }
    }
    
    public void SetCardsInHand(List<Card> cardsInHand) { CardsInHand = cardsInHand; }

    public void SetBoardFusion(Card cardToFusion){
        BoardManager.BoardFusion();
        
        Actor.IsBoardFusion(true);
        CardOnBoardToFusion = cardToFusion;
    }

    public void ResetBoardFusion(){
        Actor.IsBoardFusion(false);

        if(CardOnBoardToFusion != null){
            CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
            CardOnBoardToFusion = null;
        }
    }

}
