using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelector : MonoBehaviour{
    public static CardSelector Instance {get; private set;}
    public List<Card> SelectedCards => _selectedCards;
    [SerializeField] private List<Card> _selectedCards;

    private void OnEnable() {
        Card.OnAnyCardSelected += Card_OnAnyCardSelected;
    }

    private void OnDisable() {
        Card.OnAnyCardSelected -= Card_OnAnyCardSelected;
    }

    private void Awake() {
        if(Instance != null){Debug.Log("Error! More than one CardSelector instance" + transform + Instance); Destroy(gameObject);}
        Instance = this;
    }
    
    private void Card_OnAnyCardSelected(Card card){
        HandPositions playerHandPositions = card.GetComponentInParent<HandPositions>();

        if(card.IsSelected()){
            AddCardToSelectedList(card);
            playerHandPositions.SetFree();
        }else{
            RemoveCardFromSelectedList(card);
            playerHandPositions.SetOccupied();
        }
    }
    
    public void AddCardToSelectedList(Card card){
        _selectedCards.Add(card);

        card.UpdateNumberInLine(_selectedCards.Count);
    }

    public void RemoveCardFromSelectedList(Card card){
        _selectedCards.Remove(card);

        card.DeactiveNumberInLine();
        UpdateInLineNumberAllCardsInLine();
    }

    public void UpdateInLineNumberAllCardsInLine(){
        for(int i = 0; i <_selectedCards.Count; i++){
            _selectedCards[i].UpdateNumberInLine(i + 1);
        }
    }

    public void AddFusionedCardToTheSelectedList(Card card){
        _selectedCards.Insert(0, card);
        card.UpdateNumberInLine(_selectedCards.Count);
        UpdateInLineNumberAllCardsInLine();
    }

    public void BattleUI_OnSelectionEnd(){
        FusionLogic.Instance.StartFusion();
    }
}