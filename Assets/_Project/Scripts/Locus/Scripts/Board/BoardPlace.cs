using UnityEngine;

[RequireComponent(typeof(BoardPlaceVisual))]
public class BoardPlace : MonoBehaviour {
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private BoardManagerSO _boardManager;
    [SerializeField] private UIEventHandlerSO _uIManager;

    [field:SerializeField] public EBoardPlace Location { get; private set; }
    [field:SerializeField] public Collider[] Colliders { get; private set; }
    [field:SerializeField] public bool IsPlayerPlace { get; private set; }
    [field:SerializeField] public bool IsMonsterPlace { get; private set; }
    [field:SerializeField] public bool IsFree { get; private set; }
    private bool _canBeSelected;
    private Card _resultCard;
    public Card Card;
    private bool _isOptShowing;

    public BoardPlaceVisual Visual;

    private void OnEnable() {
        _battleManager.OnBoardPlaceSelectionStart.AddListener(BattleManager_OnBoardPlaceSelectionStart);
        _boardManager.OnBoardPlaceSelected.AddListener(BoardManager_OnBoardPlaceSelected);
    }
    
    private void OnDisable() {
        _battleManager.OnBoardPlaceSelectionStart.RemoveListener(BattleManager_OnBoardPlaceSelectionStart);
        _boardManager.OnBoardPlaceSelected.RemoveListener(BoardManager_OnBoardPlaceSelected);
    }

    private void BoardManager_OnBoardPlaceSelected(){
        _canBeSelected = false;
    }

    private void Awake() {
        Colliders = GetComponents<Collider>();
        Visual = GetComponent<BoardPlaceVisual>();
    }

    private void Start(){
        IsFree = true;
    }

    /// <summary>
    /// Allow a card be selected
    /// </summary>
    /// <param name="card"></param>
    /// <param name="isPlayerTurn"></param>
    private void BattleManager_OnBoardPlaceSelectionStart(Card card, bool isPlayerTurn){
        if(IsPlayerPlace && isPlayerTurn){
            _resultCard = card;
            if(IsMonsterPlace && _resultCard is MonsterCard){
                _canBeSelected = true;
            }else if(!IsMonsterPlace && _resultCard is ArcaneCard){
                _canBeSelected = true;
            }
        }
    }

    private void OnMouseOver(){
        if(this == null) { return; }
        if(Card == null) { return; }
        if(_battleManager.CurrentPhase != _battleManager.Battle.Action) { return; }
        if(_isOptShowing) { return; }
        _boardManager.ShowOptions(this);
        _isOptShowing = true;
    }

    private void OnMouseExit(){
        _boardManager.HideOptions();
        _isOptShowing = false;
    }

    private void OnMouseDown() {
        if(!_canBeSelected) { return; }

        switch (_battleManager.CurrentPhase){
            case BoardPlaceSelectionPhase:
                if(IsFree){
                    SetCardInPlace(_resultCard);
                    return;
                }
                //Fusion Logic with the monster in This Place
            break;

            case ActionPhase:
                Debug.Log("Attacked");
            break;
            
            default:
            break;
        }

    }

    public void SetCardInPlace(Card card){
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(monsterCard.IsInAttackMode){//In attacK
                if(monsterCard.IsFaceDown){// In Attack Face Down
                    Quaternion rotation = Quaternion.Euler(-90, -90, -90);
                    card.MoveCard(transform, rotation);
                }else{ // In Attack Face Up
                    card.MoveCard(transform);
                }
            }else{ // In Deffense
                if(monsterCard.IsFaceDown){// In Deffense Face Down
                    Quaternion rotation = Quaternion.Euler(-90, -180, -90);
                    card.MoveCard(transform, rotation);
                }else{ // In Deffense Face Up
                    Quaternion rotation = Quaternion.Euler(90, 90, 0);
                    card.MoveCard(transform, rotation);
                }
            }
            // monsterCard.SetCanChangeMode(true);
            // monsterCard.SetCanAttack(true);

        }else{// Arcane Card

        }

        Card = card;
        card.SetCardOnHand(false);
        card.DeselectCard();
        card.DisableCollider();
        
        _canBeSelected = false;
        IsFree = false;
        _boardManager.BoardPlaceSelected();
    }
}