using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIActionPhase : UIManager {
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

    /// <summary>
    /// UpdateElements set the opacity to zero for all elements other than the index
    /// </summary>
    private void UpdateElements(int index, List<VisualElement> elementList){
        for(int i = 0; i < elementList.Count; i++ ){
            if(i != index){
                elementList[i].style.opacity = 0;
            }else{
                elementList[i].style.opacity = 1;
            }
        }
    }

    private void BoardPlace_OnShowOptions(EBoardPlace place){
        switch(place){
            case EBoardPlace.MonsterFarLeft:
                UpdateElements(0, _playerMonsterCardsElements);
            break;
            case EBoardPlace.MonsterLeft:
                UpdateElements(1, _playerMonsterCardsElements);
            break;
            case EBoardPlace.MonsterCenter:
                UpdateElements(2, _playerMonsterCardsElements);;
            break;
            case EBoardPlace.MonsterRight:
                UpdateElements(3, _playerMonsterCardsElements);
            break;
            case EBoardPlace.MonsterFarRight:
                UpdateElements(4, _playerMonsterCardsElements);
            break;

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

    private void HideOptions(){
        foreach(var element in _playerMonsterCardsElements){
            element.style.opacity = 0;
        }
    }

    private void ShowOptions(BoardPlace place){
        if(place.IsPlayerPlace){
            // if(PlayerMonsterCards.ContainsKey(place.ID)){
            //     PlayerMonsterCards[place.ID].style.opacity = 1;
            // }
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

        // var index = 0;
        // foreach(var element in _playerMonsterCardsElements){
        //     PlayerMonsterCards.Add(index, element);
        //     index ++;
        // }
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