using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour{
    public static CardSelector Instance {get; private set;}
    public List<Card> SelectedCards => _selectedCards;
    [SerializeField] private List<Card> _selectedCards;

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one CardSelector instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }

    public void AddFusionedCardToTheSelectedList(Card card){
        _selectedCards.Insert(0, card);
    }
    
    public void AddCardToSelectedList(Card card){
        _selectedCards.Add(card);
        PlayerHandPositions playerHandPositions = card.GetComponentInParent<PlayerHandPositions>();
        playerHandPositions?.SetPositionFree();
        card.transform.position += new Vector3(0f, 0.5f, 0.5f);
    }

    public void RemoveCardFromSelectedList(Card card){
        _selectedCards.Remove(card);
        PlayerHandPositions playerHandPositions = card.GetComponentInParent<PlayerHandPositions>();
        playerHandPositions?.SetPositionFree();
        card.transform.position += new Vector3(0f, -0.5f, -0.5f);
    }
}