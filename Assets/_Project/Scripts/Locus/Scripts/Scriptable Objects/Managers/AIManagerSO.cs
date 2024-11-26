using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIManagerSO", menuName = "Mistix/Manager/AI", order = 0)]
public class AIManagerSO : ScriptableObject {

    public AI AI { get; private set; }
    public void SetAI(AI ai) { AI = ai; }

    public AIActor Actor { get; private set; }
    public void SetActor(AIActor actor) { Actor = actor; }

    // public AI AI { get; private set; }
    // public AICardOrganizer CardOrganizer { get; private set; }

    // public List<Card> CardsInHand = new(){};

    // private Card _fusionedCard;
    
    // [field:SerializeField] public AIActorSO Actor { get; private set; }

    // public void OnDisable() { CardsInHand.Clear(); }

    // public void SetAI(AI ai) { AI = ai; }
    // public void SetCardOrganizer(AICardOrganizer cardOrganizer) { CardOrganizer = cardOrganizer; }

    // public void SetCardsInHand(List<Card> cardsInHand) { CardsInHand = cardsInHand; }

    // public void SetFusionedCard(Card fusionedCard){
    //     _fusionedCard = null;
    //     _fusionedCard = fusionedCard;
    // }

    // public Card GetFusionedCard() { return _fusionedCard; }
}