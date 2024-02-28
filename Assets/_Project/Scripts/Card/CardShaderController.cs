using Mistix;
using Mistix.Enums;
using TMPro;
using UnityEngine;

public class CardShaderController : MonoBehaviour{
    private Card _card;
    private Renderer _renderer;

    [SerializeField] private Texture2D _allBlackMask;

    [Header("Monster")]
    [SerializeField] private TextMeshPro _atk;
    [SerializeField] private TextMeshPro _def, _lvl;

    [Header("Arcane")]
    [SerializeField] private TextMeshPro _name;
    [SerializeField] private TextMeshPro _effect;

    private void Awake() {
        _card = GetComponent<Card>();
        _renderer = GetComponent<Renderer>();
    }

    private void Start() {
        SetIlustrationAndFrame();
        SetTexts();
    }

#region Card Visual Set Up
    private void SetIlustrationAndFrame(){
        var faceMat = new Material(_renderer.sharedMaterials[0]);

        

        //Frame
        if(IsMonsterCard()){
        }else{
            var arcaneCard = _card as ArcaneCard;
            if(arcaneCard.GetArcaneType() == EArcaneType.Magic){
                faceMat.SetColor("_Color",Color.blue);
            }else{
                faceMat.SetColor("_Color",Color.red);
            }
        }

        faceMat.SetTexture("_Ilustration", _card.GetCardIlustration());
        _renderer.materials = new[] { faceMat, _renderer.sharedMaterials[1] };

        CheckFoil();
    }

    private void SetTexts(){
        if(IsMonsterCard()){
            var monsterCard = _card as MonsterCard;

            _atk.text = monsterCard.GetMonsterAtk().ToString();
            _def.text = monsterCard.GetMonsterDef().ToString();
            _lvl.text = monsterCard.GetMonsterLevel().ToString();

        }else{
            var arcaneCard = _card as ArcaneCard;

            _name.text = arcaneCard.GetCardName();
            _effect.text = arcaneCard.GetCardEffectText();
        }
    }

#endregion

private void CheckFoil(){
    if(IsMonsterCard()){
        var levelMin = 2;
        var monsterCard = _card as MonsterCard;
        var monsterLevel = monsterCard.GetMonsterLevel();

        if(monsterLevel <= levelMin){
            Debug.Log("T");
            var faceMat = new Material(_renderer.sharedMaterials[0]);

            Debug.Log(faceMat.GetFloat("_Foil_Effect_Velocity"));
            faceMat.SetFloat("_Foil_Effect_Velocity", 0);
            Debug.Log(faceMat.GetFloat("_Foil_Effect_Velocity"));

            _renderer.materials = new[] { faceMat, _renderer.sharedMaterials[1] };
        }

    }else{
        //Adicionar verificação de carta arcana especial
    }

    // var faceMat = new Material(_renderer.sharedMaterials[0]);
    // faceMat.SetFloat("_Foil_Effect_Velocity", 0);
    // _renderer.materials = new[] { faceMat, _renderer.sharedMaterials[1] };
}

    private bool IsMonsterCard(){
        return _card.GetCardType() == ECardType.Monster;
    }
}
