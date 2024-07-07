using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIActionPhase : UIManager {

    private Dictionary<int, VisualElement> PlayerMonsterCards = new();

    private VisualElement _monsterFarLeftCard;
    private VisualElement _monsterLeftCard;
    private VisualElement _monsterCenterCard;
    private VisualElement _monsterRightCard;
    private VisualElement _monsterFarRightCard;
    public VisualElement _actionCanvas;

    public List<VisualElement> _playerMonsterCardsElements = new();

    private Button _button1, button2;

    private void OnEnable() {
        ActionPhase.OnActionPhaseStart += ActionPhase_OnActionPhaseStart;
        BoardPlace.OnShowOptions += BoardPlace_OnShowOptions;
        BoardPlace.OnHideOptions += BoardPlace_OnHideOptions;
    }

    private void OnDisable() {
        ActionPhase.OnActionPhaseStart -= ActionPhase_OnActionPhaseStart;
        BoardPlace.OnShowOptions -= BoardPlace_OnShowOptions;
        BoardPlace.OnHideOptions -= BoardPlace_OnHideOptions;
    }

    private void ActionPhase_OnActionPhaseStart(){
        ShowCanvas();
    }

    private void BoardPlace_OnHideOptions(){
        HideOptions();
    }

    private void BoardPlace_OnShowOptions(BoardPlace place){
        ShowOptions(place);
    }

    private void HideOptions(){
        foreach(var element in _playerMonsterCardsElements){
            element.style.opacity = 0;
        }
    }

    private void ShowOptions(BoardPlace place){
        if(place.IsPlayerPlace){
            if(PlayerMonsterCards.ContainsKey(place.ID)){
                PlayerMonsterCards[place.ID].style.opacity = 1;
            }
        }
    }

    private void ShowCanvas(){
        _actionCanvas.style.display = DisplayStyle.Flex;
        foreach(var element in _playerMonsterCardsElements){
            element.style.opacity = 0;
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

        var index = 0;
        foreach(var element in _playerMonsterCardsElements){
            PlayerMonsterCards.Add(index, element);
            index ++;
        }
    }

    private void SetElements(){
        _actionCanvas = Root.Q("ActionCanvas");
        _monsterFarLeftCard = Root.Q("MonsterFarLeft");
        _monsterLeftCard = Root.Q("MonsterLeft");
        _monsterCenterCard = Root.Q("MonsterCenter");
        _monsterRightCard = Root.Q("MonsterRight");
        _monsterFarRightCard = Root.Q("MonsterFarRight");
    }
}