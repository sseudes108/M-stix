using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActionPhase : MonoBehaviour {
    [SerializeField] private BoardManagerSO _boardManager;
    [SerializeField] private TurnManagerSO _turnManager;

    private GameObject _buttonsContainer;
    private Button _button1, _button2;
    private TextMeshProUGUI _button1Text, _button2Text;

    private BoardPlace _buttonPlace = null;

    private MonsterCard _monsterCard;
    private ArcaneCard _arcaneCard;

    private void OnEnable() {
        _boardManager.OnShowOptions.AddListener(BoardManager_OnShowOptions);
        _boardManager.OnHideOptions.AddListener(BoardManager_OnHideOptions);
    }

    private void OnDisable() {
        _boardManager.OnShowOptions.RemoveListener(BoardManager_OnShowOptions);
        _boardManager.OnHideOptions.RemoveListener(BoardManager_OnHideOptions);

        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
    }

    private void Awake() {
        _buttonsContainer = transform.Find("Canvas/ActionPhaseOptionsButton").gameObject;
        _button1 = _buttonsContainer.transform.Find("ActionButton1").GetComponent<Button>();
        _button2 = _buttonsContainer.transform.Find("ActionButton2").GetComponent<Button>();
        _button1Text = _button1.GetComponentInChildren<TextMeshProUGUI>();
        _button2Text = _button2.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void BoardManager_OnHideOptions() {HideOptions(); }
    private void BoardManager_OnShowOptions(BoardPlace place){
        //Options for the player cards on field
        if(_turnManager.IsPlayerTurn && place.IsPlayerPlace){
            if(place.IsMonsterPlace){
                _monsterCard = null;
                _monsterCard = place.CardInPlace as MonsterCard;
                SetMonsterOptions(_monsterCard, place);
                return;
            }

            _arcaneCard = null;
            _arcaneCard = place.CardInPlace as ArcaneCard;
            return;
            //Arcane Card
        }

        //Options to show on enemy cards on field
    }

    private void ShowButtons(EBoardPlace place){
        Vector2 position = new();
        switch(place){
            case EBoardPlace.MonsterFarLeft:
                position = new Vector2(-368f, 42f);
            break;

            case EBoardPlace.MonsterLeft:
                position = new Vector2(-191, 42f);
            break;

            case EBoardPlace.MonsterCenter:
                position = new Vector2(-2, 42f);
            break;

            case EBoardPlace.MonsterRight:
                position = new Vector2(191, 42f);
            break;

            case EBoardPlace.MonsterFarRight:
                position = new Vector2(368, 42f);
            break;
        }
        _buttonsContainer.GetComponent<RectTransform>().anchoredPosition = position;
        _buttonsContainer.SetActive(true);
    }

    private void HideOptions(){
        _buttonsContainer.SetActive(false);
        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
    }

    private void SetMonsterOptions(MonsterCard cardInPlace, BoardPlace place){
        // _buttonPlace = null;
        // _buttonPlace = place;

        if(!cardInPlace.IsFaceDown){//card face Up

            if(cardInPlace.WasFlipedThisTurn == false){
                if(cardInPlace.CanChangeMode && cardInPlace.IsInAttackMode){//is in attack mode and can change
                    ShowButtons(place.Location);

                    _button1Text.text = "Attack!";
                    _button1.onClick.AddListener(Option1Clicked);

                    _button2Text.text = "DEF";
                    _button2.onClick.AddListener(Option2Clicked);
                    return;
                }

                if(cardInPlace.CanChangeMode && !cardInPlace.IsInAttackMode){//is in deffense mode and can change
                    ShowButtons(place.Location);

                    _button2Text.text = "ATK"; //button two "template" is used for aesthetic reasons
                    _button2.onClick.AddListener(Option1Clicked);
                    _button1.gameObject.SetActive(false);
                    return;
                }
            }
        }

        if(cardInPlace.IsFaceDown && cardInPlace.CanFlip){ //Face down and can flip
            ShowButtons(place.Location);

            _button2Text.text = "Flip";
            _buttonPlace = place;
            
            _button2.onClick.AddListener(Option1Clicked);
            _button1.gameObject.SetActive(false);
            return;
        }
    }

    private void Option1Clicked(){
        if(_monsterCard != null && _arcaneCard == null){

            if(_monsterCard.IsFaceDown == false){
                if(_monsterCard.IsInAttackMode){
                    Debug.Log("Attack!");
                    return;
                }
            }

            if(_monsterCard.CanFlip){
                FlipCard(_buttonPlace);
                _button2.gameObject.SetActive(false);
                return;
            }
        }

        if(_arcaneCard != null && _monsterCard == null){

            return;
        }
    }

    private void Option2Clicked(){
        if(_monsterCard != null && _arcaneCard == null){
            if(_monsterCard.CanChangeMode && !_monsterCard.IsInAttackMode){//is in deffense mode and can change
                ChangeMonsterToDef(_buttonPlace);
            }
        }

        // if(_button2Text.text == "DEF"){
        //     Debug.Log("Change To Def");
        //     return;
        // }

        if(_button2Text.text == "ATK"){
            Debug.Log("Change To Atk");
            return;
        }
    }

    private void FlipCard(BoardPlace place){
        place.FlipCard();
        _buttonPlace = null;
    }

    private void ChangeMonsterToDef(BoardPlace place){
        place.ChangeMonsterToDef();
    }
}