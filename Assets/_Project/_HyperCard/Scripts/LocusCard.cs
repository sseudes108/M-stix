using UnityEngine;
using Mistix;
using Mistix.Enums;

public class LocusCard : MonoBehaviour{
    private Card _card;
    private Renderer _renderer;

    private void Awake() {
        _card = GetComponent<Card>();
        _renderer = GetComponent<Renderer>();
    }

    private void Start() {
        SetIlustrationAndFrame();
    }

    private void SetIlustrationAndFrame(){
        var faceMat = new Material(_renderer.sharedMaterials[0]);
        
        //Frame
        if(_card.GetCardType() == ECardType.Monster){
            // _faceCardMaterial = _monsterCardMaterial;
        }else{
            // _faceCardMaterial = _arcaneCardMaterial;
            var arcaneCard = _card as ArcaneCard;
            if(arcaneCard.GetArcaneType() == EArcaneType.Magic){
                faceMat.SetColor("_Color",Color.blue);
            }else{
                faceMat.SetColor("_Color",Color.red);
            }
        }

        faceMat.SetTexture("_Ilustration", _card.GetCardIlustration());
        _renderer.materials = new[] { faceMat, _renderer.sharedMaterials[1] };
    }
}

