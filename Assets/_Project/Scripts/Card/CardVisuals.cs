using System.Collections.Generic;
using UnityEngine;

public class CardVisuals : MonoBehaviour {
    public void ResetBorderColors(List<Card> cards){
        foreach(var card in cards){
            card.Shader.ResetBoarderColor();
        }
    }
    
    public void DissolveCard(List<Card> cards, Color newColor){
        foreach(var card in cards){
            card.Shader.DissolveCard(newColor);
        }
    }
    public void DissolveCard(Card card, Color newColor){
        card.Shader.DissolveCard(newColor);
    }

    public void SolidifyCard(Card card, Color newColor){
        card.Shader.SolidifyCard(newColor);
    }

    public void MakeCardInvisible(Card card){
        card.Shader.MakeCardCardInvisible();
    }
}