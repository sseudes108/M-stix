using UnityEngine;
using Mistix;
using Mistix.Enums;

public class LocusCard : MonoBehaviour{
    [SerializeField] private Card _card;

    [SerializeField] private Material _cardBackMaterial;

    [Header("Materials")]
    [SerializeField] private Material _monsterFaceMaterial;
    [SerializeField] private Material _arcaneFaceMaterial;

    [Header("Arcane Textures")]
    [SerializeField] private Texture2D _arcaneTrapFaceTexture;
    [SerializeField] private Texture2D _arcaneMagicFaceTexture;

    private Renderer _renderer;
    public Material _faceMat;
    public Material _backMat;

    Material[] materials;
    private Material _faceCardMaterial;

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();
        _card = GetComponent<Card>();
    }

    private void Start() {
        if(_card.GetCardType() == Mistix.Enums.ECardType.Monster){
            _faceCardMaterial = _monsterFaceMaterial;
        }else{
            var arcaneCard = _card as ArcaneCard;
            _faceCardMaterial = _arcaneFaceMaterial;
            if(arcaneCard.GetArcaneType() == EArcaneType.Magic){
                _faceCardMaterial.SetTexture("_Frame",_arcaneMagicFaceTexture);
            }else{
                _faceCardMaterial.SetTexture("_Frame",_arcaneTrapFaceTexture);
            }
        }

        materials = _renderer.sharedMaterials;
        materials[0] = _faceCardMaterial;
        materials[1] = _cardBackMaterial;
        _renderer.sharedMaterials = materials;

        var mat = _renderer.sharedMaterials;
        _faceMat = new Material(mat[0]);
        _backMat = new Material(mat[1]);

        _faceMat.SetTexture("_Ilustration", _card.GetCardIlustration());
    }
}

