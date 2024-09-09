using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIManagerSO", menuName = "Mistix/Manager/AI", order = 0)]
public class AIManagerSO : ScriptableObject {

    public List<Card> CardsInHand = new(){};

    private Card _fusionedCard;

    public List<BoardPlace> MonsterPlaces { get; private set; }
    public List<BoardPlace> ArcanePlaces { get; private set; }

    public AI AI { get; private set; }
    [field:SerializeField] public AIActorSO Actor { get; private set; }

    public void OnDisable(){
        CardsInHand.Clear();
    }

    public void SetAI(AI ai) { 
        AI = ai;
    }

    public void SetCardsInHand(List<Card> cardsInHand){
        CardsInHand = cardsInHand;
    }

    public void SetFusionedCard(Card fusionedCard){
        _fusionedCard = null;
        _fusionedCard = fusionedCard;
    }

    public Card GetFusionedCard(){
        return _fusionedCard;
    }
}