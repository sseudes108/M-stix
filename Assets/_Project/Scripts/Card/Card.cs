using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] protected ScriptableObject _cardData;
    [SerializeField] protected Texture2D _ilustration;
    public virtual void SetCardData(ScriptableObject cardData){}
    public virtual void SetUpCardVariables(){}
    public Texture2D Ilustration => _ilustration;

    protected bool _canMove = false;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private void Awake() {
        _canMove = false;
    }

    private void Update() {
        if(_canMove){
            Move();
        }
    }

    private void Move(){
        float moveSpeed = 5f;
        float rotateSpeed = 300.0f;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotateSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _targetPosition) < 0.2f && transform.rotation == _targetRotation){
            _canMove = false;
        }
    }

    public void MoveCard(Vector3 targetPosition, Quaternion targetRotation){
        _canMove = true;
        _targetPosition = targetPosition;
        _targetRotation = targetRotation;
    }
}