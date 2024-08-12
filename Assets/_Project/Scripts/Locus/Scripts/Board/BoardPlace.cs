using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoardPlaceVisual))]
public class BoardPlace : MonoBehaviour {
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private BoardManagerSO _boardManager;
    // [SerializeField] private FusionManagerSO _fusionManager;
    // [SerializeField] private TurnManagerSO _turnManager;
    [SerializeField] private CardManagerSO _cardManager;
    // [SerializeField] private UIEventHandlerSO _uIManager;

    [field:SerializeField] public EBoardPlace Location { get; private set; }
    [field:SerializeField] public Collider[] Colliders { get; private set; }
    [field:SerializeField] public bool IsPlayerPlace { get; private set; }
    [field:SerializeField] public bool IsMonsterPlace { get; private set; }
    [field:SerializeField] public bool IsFree { get; private set; }
    private bool _canBeSelected;
    private Card _resultCard;
    public Card CardInPlace;
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

#region Unity Methods

    private void Awake() {
        Colliders = GetComponents<Collider>();
        Visual = GetComponent<BoardPlaceVisual>();
    }

    private void Start(){
        IsFree = true;
    }

    private void OnMouseOver(){
        if(this == null) { return; }

        if(CardInPlace == null) { return; }
        CardInPlace.OnMouseOver(); //Update the illustration in UICard

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
                
                //Board Fusion
                StartBoardFusion();

                // _cardManager.Selector.SetCardsToBoardFusion(new List<Card>{CardInPlace, _resultCard});
                // _battleManager.Battle.ChangeState(_battleManager.Battle.Fusion);//Change phase back to fusion
                // CardInPlace = null;
                // IsFree = true;
            break;

            case ActionPhase:
                Debug.Log("Attacked");
            break;
            
            default:
            break;
        }
    }


#endregion

#region Custom Methods

    public void SetCardInPlace(Card card){
        if(card is MonsterCard){
            var monsterCard = card as MonsterCard;
            if(monsterCard.IsInAttackMode){//In attacK

                if(monsterCard.IsFaceDown){// In Attack Face Down

                    Quaternion rotation;

                    if(monsterCard.IsPlayerCard){
                        rotation = Quaternion.Euler(-90, -90, -90);
                    }else{
                        rotation = Quaternion.Euler(-90, -90, 90);
                    }

                    card.MoveCard(transform, rotation);

                }else{ // In Attack Face Up
                    card.MoveCard(transform);
                }
                
            }else{ // In Deffense
                if(monsterCard.IsFaceDown){// In Deffense Face Down

                    Quaternion rotation;

                    if(monsterCard.IsPlayerCard){
                        rotation = Quaternion.Euler(-90, -180, -90);
                    }else{
                        rotation = Quaternion.Euler(-90, -180, 90);
                    }

                    card.MoveCard(transform, rotation);

                }else{ // In Deffense Face Up

                    Quaternion rotation;

                    if(monsterCard.IsPlayerCard){
                        rotation = Quaternion.Euler(90, 90, 0);
                    }else{
                        rotation = Quaternion.Euler(90, 90, 180);
                    }

                    card.MoveCard(transform, rotation);

                }
            }
            // monsterCard.SetCanChangeMode(true);
            // monsterCard.SetCanAttack(true);

        }else{// Arcane Card

        }

        CardInPlace = card;
        card.SetCardOnHand(false);
        card.DeselectCard();
        card.DisableCollider();
        
        _canBeSelected = false;
        IsFree = false;
        _boardManager.BoardPlaceSelected();
    }

    private void StartBoardFusion(){
        _cardManager.Selector.SetCardsToBoardFusion(new List<Card>{CardInPlace, _resultCard});
        _battleManager.Battle.ChangeState(_battleManager.Battle.Fusion);//Change phase back to fusion
        CardInPlace = null;
        IsFree = true;
    }

#endregion

#region Events

    /// <summary>
    /// Allow a card be selected
    /// </summary>
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

    private void BoardManager_OnBoardPlaceSelected(){
        _canBeSelected = false;
    }
    
#endregion

}