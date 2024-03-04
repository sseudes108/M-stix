using UnityEngine;

public class Card : MonoBehaviour {
    public Texture2D Ilustration => _ilustration;
    public CardShaderController Shader => _shader;

    // -- //
    [HideInInspector] [SerializeField] protected ScriptableObject _cardData;    
    [HideInInspector] [SerializeField] protected Texture2D _ilustration;
    //Need to be serialized. Dont know why.//

    //Canvas and Model
    [SerializeField] protected Transform _cardModel, _statsCanvas;

    //Shader
    private CardShaderController _shader;
    private Color _selectedColor = new(191, 162, 57);

    //Physics
    protected Collider _collider;
    protected bool _isSelected = false;

    //Movement
    private Movement _movement;

    //Card owner and place
    [SerializeField] private bool _isPlayerCard = false;
    [SerializeField] private bool _isOnHand = false;
    [SerializeField] private bool _isOnField = false;
    [SerializeField] private bool _isFaceDown = false;

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

    public void DisableStatCanvas(){
        _statsCanvas.gameObject.SetActive(false);
    }
    public void DisableModelVisual(){
        _cardModel.gameObject.SetActive(false);
    }
    public void EnableStatCanvas(){
        _statsCanvas.gameObject.SetActive(true);
        _cardModel.gameObject.SetActive(true);
    }

    public void DisableCollider(){_collider.enabled = false;}
    public void EnableCollider(){_collider.enabled = true;}

    public void MoveCard(Transform targetTransform){
        transform.SetParent(targetTransform);
        MoveCard(targetTransform.position, targetTransform.rotation);
    }

    private void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
        float moveSpeed = 5.0f;
        _movement.SetTargetPosition(targetPosition, targetRotation, moveSpeed);
    }

    private void OnMouseDown() {
        if(_isPlayerCard && _isOnHand){
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

    private void OnMouseOver() {
        if(_isPlayerCard || _isOnField && !_isFaceDown){
            BattleManager.Instance.UIBattleManager.UICardPlaceHolder.ChangeIllustration(Ilustration);
        }
    }

    public void SetPlayerCard(){_isPlayerCard = true;}
    public bool IsPlayerCard(){return _isPlayerCard;}

    public void SetCardOnHand(){_isOnHand = true;}
    public bool IsOnHand(){return _isOnHand;}

    public void SetCardOnField(){_isOnField = true;}
    public bool IsOnField(){return _isOnField;}

    public void SetCardFaceDown(){_isFaceDown = true;}
    public bool IsFaceDown(){return _isFaceDown;}

    public void DestroyCard(){
        Destroy(gameObject);
    }
}