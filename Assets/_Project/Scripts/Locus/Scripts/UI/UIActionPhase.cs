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

    private Card _card;

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
        if(_turnManager.IsPlayerTurn && place.IsPlayerPlace){
            _card = null;
            _card = place.CardInPlace;
            SetCardOptions(_card, place);
        }
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

    private void SetCardOptions(Card cardInPlace, BoardPlace place){
        // _buttonPlace = null;
        _buttonPlace = place;

        if(cardInPlace.MustShowButtons){
            if(cardInPlace.IsFaceDown){//Is Face Down
                if(cardInPlace.CanFlip){ // Can flip
                    ShowButtons(place.Location);

                    _button2Text.text = "Flip";

                    _button2.onClick.AddListener(Option1Clicked);
                    _button1.gameObject.SetActive(false);
                    return;
                }
            }else{
                if(cardInPlace is MonsterCard){
                    MonsterCard monster = _card as MonsterCard;

                    if(monster.IsInAttackMode){
                        if(monster.CanAttack){
                            ShowButtons(place.Location);
                            _button1Text.text = "Attack!";
                            _button1.onClick.AddListener(Option1Clicked);
                        }

                        if(monster.CanChangeMode){
                            ShowButtons(place.Location);
                            _button2Text.text = "DEF";
                            _button2.onClick.AddListener(Option2Clicked);
                        }
                    }else{ //Is in Deffense Mode
                        if(monster.CanChangeMode){
                            ShowButtons(place.Location);
                            _button2Text.text = "ATK"; //button two "template" is used for aesthetic reasons
                            _button2.onClick.AddListener(Option1Clicked);
                            _button1.gameObject.SetActive(false);
                        }
                    }

                }else{
                    //Arcane Options
                }
            }
        }
    }

    private void Option1Clicked(){
        if(_card is MonsterCard){
            if(_card.IsFaceDown){
                if(_card.CanFlip){
                    FlipCard(_buttonPlace);
                }
            }else{
                MonsterCard monster = _card as MonsterCard;
                if(monster.IsInAttackMode){
                    if(monster.CanAttack){
                        Debug.Log("Attack!");
                    }
                }else{// Is In deffense
                    if(monster.CanChangeMode){ //Not needed but keeped for readbility reasons
                        ChangeMonsterToAtk(_buttonPlace);
                    }
                }
            }
        }else{
            //Arcane Card
        }

        _card.SetShowButtons(false);
        HideOptions();
    }

    private void Option2Clicked(){
        if(_card is MonsterCard){
            MonsterCard monster = _card as MonsterCard;
            if(monster.IsInAttackMode && monster.CanChangeMode){
                ChangeMonsterToDef(_buttonPlace);
            }
        }else{
            //Arcane Card
        }

        _card.SetShowButtons(false);
        HideOptions();
    }

    private void FlipCard(BoardPlace place) { place.FlipCard(); }
    private void ChangeMonsterToDef(BoardPlace place) { place.ChangeMonsterToDef(); }
    private void ChangeMonsterToAtk(BoardPlace place) { place.ChangeMonsterToAtk(); }
}