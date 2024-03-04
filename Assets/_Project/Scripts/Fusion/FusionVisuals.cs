using System.Collections.Generic;
using UnityEngine;

public class FusionVisuals : MonoBehaviour {
    public void ResetBorderColors(List<Card> cards){
        foreach(var card in cards){
            card.Shader.ResetBoarderColor();
        }
    }
    
    public void DissolveCard(List<Card> cards){
        foreach(var card in cards){
            card.Shader.DissolveCard();
        }
    }
    public void DissolveCard(Card card){
        card.Shader.DissolveCard();
    }

    public void SolidifyCard(Card card){
        card.Shader.SolidifyCard();
    }

    public void MakeCardInvisible(Card card){
        card.Shader.MakeCardCardInvisible();
    }
}