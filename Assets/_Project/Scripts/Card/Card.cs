using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Card : MonoBehaviour {
    public Texture2D Ilustration => _ilustration;
    public CardShaderController Shader => _shader;

    [SerializeField] protected ScriptableObject _cardData;    
    [SerializeField] protected Texture2D _ilustration;
    //Need to be serialized. Dont know why.

    //Shader
    private CardShaderController _shader;
    private Color _selectedColor = new(191, 162, 57);

    //Physics
    protected Collider _collider;
    protected bool _isSelected = false;

    //Movement
    private Movement _movement;

    private void Awake() {
        SetUpComponents();
    }

    private void SetUpComponents(){
        _collider = GetComponentInChildren<Collider>();
        _shader = GetComponentInChildren<CardShaderController>();
        _movement = GetComponent<Movement>();
    }

    public virtual void SetCardData(ScriptableObject cardData){}
    public virtual void SetUpCardVariables(){}
    
    public virtual ECardType GetCardType(){return ECardType.Err;}

    public string GetCardName() => _cardData.name;

    public void DisableCardCollider() {_collider.enabled = false;}
    public void EnableCollider() {_collider.enabled = true;}

    public void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
        _movement.SetTargetPosition(targetPosition, targetRotation, 5);
    }

    protected void OnMouseDown() {
        Vector3 newPos = new();

        if(!_isSelected){
            BattleManager.Instance.CardSelector.AddCardToSelectedList(this);
            newPos = new (0,+0.3f,0);
            _shader.SetBoarderColor(_selectedColor);

            _isSelected = true;
            
        }else{
            BattleManager.Instance.CardSelector.RemoveCardFromSelectedList(this);
            newPos = new (0,-0.3f,0);
            _shader.ResetBoarderColor();
    
            _isSelected = false;
        }

        transform.position += newPos;
    }
}