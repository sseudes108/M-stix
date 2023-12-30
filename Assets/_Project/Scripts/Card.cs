using System;
using UnityEngine;

public class Card : MonoBehaviour{
    public static Action<Card> OnSelect, OnDiselect;
    private MonsterCard _monsterCard;
    private ArcaneCard _arcaneCard;
    [SerializeField] private bool _selected;

    private Collider _collider;

    private void Awake() {
        _collider = GetComponent<Collider>();
    }

    private void Start() {
        gameObject.TryGetComponent<MonsterCard>(out _monsterCard);
        gameObject.TryGetComponent<ArcaneCard>(out _arcaneCard);
    }

    public CardSO.CardType GetCardType(){
        if(_monsterCard != null){
            return CardSO.CardType.Monster;
        }else{
            return CardSO.CardType.Arcane;
        }
    }

    public void SetArcaneData(CardSO data){
        _arcaneCard.SetData(data);
    }
    
    public void SetMonsterData(CardSO data){
        _monsterCard.SetData(data);
    }

    public MonsterCard GetMonsterInfo(){
        return _monsterCard;
    }
    public ArcaneCard GetArcaneInfo(){
        return _arcaneCard;
    }

    private void OnMouseDown() {
        if(_selected){
            _selected = false;
            transform.position += new Vector3(0, -0.5f, -0.5f);
            OnDiselect?.Invoke(this);
        }else{
            _selected = true;
            transform.position += new Vector3(0, 0.5f, 0.5f);
            OnSelect?.Invoke(this);
        }
    }

    public void EnableCollider(bool disable){
        if(!disable){
            _collider.enabled = false;
        }else{
            _collider.enabled = true;
        }
    }
    
}
