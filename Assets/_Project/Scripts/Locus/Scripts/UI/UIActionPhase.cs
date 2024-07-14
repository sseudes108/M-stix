using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIActionPhase : UIManager {
    [SerializeField] private BoardPlaceEventHandlerSO BoardManager;
    [SerializeField] private BattleManagerSO BattleManager;
    [SerializeField] private UIEventHandlerSO UIManager;
    
    private VisualElement _monsterFarLeftCard;
    private VisualElement _monsterLeftCard;
    private VisualElement _monsterCenterCard;
    private VisualElement _monsterRightCard;
    private VisualElement _monsterFarRightCard;
    public VisualElement _actionCanvas;
    public VisualElement _monsterCards;
    public VisualElement _arcaneCards;

    public List<VisualElement> _playerMonsterCardsElements = new();

    private Button _button1, _button2;

    private Card _card;

    private void OnEnable() {
        BattleManager.OnActionPhaseStart.AddListener(BattleManager_OnActionPhaseStart);
        BattleManager.OnActionPhaseTwoStart.AddListener(BattleManager_OnActionPhaseTwoStart);
        BoardManager.OnShowOptions.AddListener(BoardManager_OnShowOptions);
        BoardManager.OnHideOptions.AddListener(BoardManager_OnHideOptions);
    }

    private void OnDisable() {
        BattleManager.OnActionPhaseStart.RemoveListener(BattleManager_OnActionPhaseStart);
        BattleManager.OnActionPhaseTwoStart.RemoveListener(BattleManager_OnActionPhaseTwoStart);
        BoardManager.OnShowOptions.RemoveListener(BoardManager_OnShowOptions);
        BoardManager.OnHideOptions.RemoveListener(BoardManager_OnHideOptions);

        if(_button1 != null){
            _button1.clicked -= Option1Clicked;
            _button1.clicked -= AttackButtonClicked;
        }

        if(_button2 != null){
            _button2.clicked -= Option2Clicked;
        }
    }

    private void BattleManager_OnActionPhaseTwoStart(){
        ShowCanvas();
    }
    
    private void BattleManager_OnActionPhaseStart(){
        ShowCanvas();
    }

    private void BoardManager_OnHideOptions(){
        HideOptions();
    }

    private void BoardManager_OnShowOptions(BoardPlace place){
        if(place.IsMonsterPlace){
            ShowMonsterOptions(place.Location, place.Card as MonsterCard);
        }else{
            ShowArcaneOptions(place.Location, place.Card as ArcaneCard);
        }
    }

    private void HideOptions(){
        foreach(var element in _playerMonsterCardsElements){
            element.style.display = DisplayStyle.None;
        }

        if(_button1 != null){
            _button1.clicked -= Option1Clicked;
        }

        if(_button2 != null){
            _button1.clicked -= Option2Clicked;
        }
    }

    /// <summary>
    /// UpdateElements set the opacity to zero for all elements other than the index
    /// </summary>
    private void UpdateElements(int index, List<VisualElement> elementList){
        for(int i = 0; i < elementList.Count; i++ ){
            if(i != index){
                elementList[i].style.display = DisplayStyle.None;
            }else{
                elementList[i].style.display = DisplayStyle.Flex;
            }
        }
    }

    private void ShowMonsterOptions(EBoardPlace place, MonsterCard cardInPlace){
        _card = null;
        _card = cardInPlace;
        _arcaneCards.style.display = DisplayStyle.None;
        _monsterCards.style.display = DisplayStyle.Flex;

        switch(place){
            case EBoardPlace.MonsterFarLeft:
                SetButtons(0, "MonsterFarLeft", cardInPlace);
            break;
            case EBoardPlace.MonsterLeft:
                SetButtons(1, "MonsterLeft", cardInPlace);
            break;
            case EBoardPlace.MonsterCenter:
                SetButtons(2, "MonsterCenter", cardInPlace);
            break;
            case EBoardPlace.MonsterRight:
                SetButtons(3, "MonsterRight", cardInPlace);
            break;
            case EBoardPlace.MonsterFarRight:
                SetButtons(4, "MonsterFarRight", cardInPlace);
            break;
        }
    }

    private void SetButtons(int placeIndex, string placeName, MonsterCard cardInPlace){
        if (!cardInPlace.IsFaceDown && !cardInPlace.CanChangeMode) { return; }
        UpdateElements(placeIndex, _playerMonsterCardsElements);

        if (cardInPlace.IsFaceDown){ // is face down and can flip
            if(cardInPlace.CanFlip){
                _button1 = GetButton(1, "Flip", placeName);
                _button1.clicked -= Option1Clicked;
                _button1.clicked += Option1Clicked;
            }
        }else{// if is face up
            // Debug.Log("Is Face Up");
            if (cardInPlace.IsInAttackMode){// if face up and attack mode
                // Debug.Log("Is In Attack Mode");
                if (cardInPlace.CanAttack && cardInPlace.CanChangeMode){ // if can change mode and can attack
                    _button1 = null;
                    _button2 = null;
                    _button1 = GetButton(1, "Attack!", placeName);
                    _button2 = GetButton(2, "DEF", placeName);

                    _button1.clicked -= AttackButtonClicked;
                    _button1.clicked += AttackButtonClicked;

                    _button2.clicked -= Option2Clicked;
                    _button2.clicked += Option2Clicked;

                }else if (cardInPlace.CanAttack){ // only can attack
                    _button1 = null;
                    _button1 = GetButton(1, "Attack!", placeName);
                    _button1.clicked -= AttackButtonClicked;
                    _button1.clicked += AttackButtonClicked;

                }else if (cardInPlace.CanChangeMode){ // only can change mode
                    _button1 = null;
                    _button1 = GetButton(1, "DEF!", placeName);
                    _button1.clicked -= Option1Clicked;
                    _button1.clicked += Option1Clicked;
                }

            }else{// if face up and deffense mode
                if (cardInPlace.CanChangeMode){ // if can change mode
                    _button1 = null;
                    _button1 = GetButton(1, "ATK", placeName);
                    _button1.clicked -= Option1Clicked;
                    _button1.clicked += Option1Clicked;
                }
            }
        }
    }

    private void ShowArcaneOptions(EBoardPlace place, ArcaneCard cardInPlace){
        _monsterCards.style.display = DisplayStyle.None;
        _arcaneCards.style.display = DisplayStyle.Flex;

        switch(place){
            case EBoardPlace.ArcaneFarLeft:
            break;
            case EBoardPlace.ArcaneLeft:
            break;
            case EBoardPlace.ArcaneCenter:
            break;
            case EBoardPlace.ArcaneRight:
            break;
            case EBoardPlace.ArcaneFarRight:
            break;
        }
    }

    private void AttackButtonClicked(){
        Debug.LogWarning("Implement AttackButtonClicked() on UIActionPhase");
    }

    private void Option1Clicked(){
        Debug.Log("Option1Clicked");
    }

    private void Option2Clicked(){
        Debug.Log("Option2Clicked");
    }

    private Button GetButton(int index, string text, string visualElement){
        var element = _monsterCards.Q<VisualElement>($"{visualElement}");
        var button = element.Q<Button>($"Opt{index}");
        button.style.display = DisplayStyle.Flex;
        button.text = text;
        return button;
    }

    private void ShowCanvas(){
        _actionCanvas.style.display = DisplayStyle.Flex;
        foreach(var element in _playerMonsterCardsElements){
            element.style.display = DisplayStyle.None;
        }
    }

    public override void Awake() {
        base.Awake();
        SetElements();
    }

    private void Start() {
        _playerMonsterCardsElements.Add(_monsterFarLeftCard);
        _playerMonsterCardsElements.Add(_monsterLeftCard);
        _playerMonsterCardsElements.Add(_monsterCenterCard);
        _playerMonsterCardsElements.Add(_monsterRightCard);
        _playerMonsterCardsElements.Add(_monsterFarRightCard);

        _actionCanvas.style.display = DisplayStyle.None;
        _monsterCards.style.display = DisplayStyle.None;
        _arcaneCards.style.display = DisplayStyle.None;
    }

    private void SetElements(){
        _actionCanvas = Root.Q("ActionCanvas");
        _monsterCards = Root.Q("MonsterCards");
        _arcaneCards = Root.Q("ArcaneCards");

        _monsterFarLeftCard = Root.Q("MonsterFarLeft");
        _monsterLeftCard = Root.Q("MonsterLeft");
        _monsterCenterCard = Root.Q("MonsterCenter");
        _monsterRightCard = Root.Q("MonsterRight");
        _monsterFarRightCard = Root.Q("MonsterFarRight");
    }
}