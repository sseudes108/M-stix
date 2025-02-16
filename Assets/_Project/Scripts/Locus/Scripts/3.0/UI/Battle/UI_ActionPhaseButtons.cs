using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Mistix{
    public class UI_ActionPhaseButtons : MonoBehaviour {
        [SerializeField] private GameObject _buttonsContainer;
        [SerializeField] private Button _button1, _button2;
        [SerializeField] private TextMeshProUGUI _button1Text, _button2Text;

        private UIManager _uiManager;

        private BoardPlace _buttonPlace = null;
        private Card _card;

        private void Awake() {
            _uiManager = GetComponent<UIManager>();
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

        private void HideCardButtons(){
            _card.SetShowButtons(false);
            HideOptions();
        }

        public void HideOptions(){
            _buttonsContainer.SetActive(false);
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
        }

        public void SetCardOptions(Card cardInPlace, BoardPlace place){
            _buttonPlace = place;
            _card = cardInPlace;

            if(cardInPlace.MustShowButtons){
                if(cardInPlace.IsFaceDown){//Is Face Down
                    if(cardInPlace.CanFlip){ // Can flip
                        ShowButtons(place.Location);

                        _button2Text.text = "Flip";

                        _button2.onClick.AddListener(Action1Clicked);
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
                                _button1.onClick.AddListener(Action1Clicked);
                            }

                            if(monster.CanChangeMode){
                                ShowButtons(place.Location);
                                _button2Text.text = "DEF";
                                _button2.onClick.AddListener(Action2Clicked);
                            }
                        }else{ //Is in Deffense Mode
                            if(monster.CanChangeMode){
                                ShowButtons(place.Location);
                                _button2Text.text = "ATK"; //button two "template" is used for aesthetic reasons
                                _button2.onClick.AddListener(Action1Clicked);
                                _button1.gameObject.SetActive(false);
                            }
                        }

                    }else{
                        //Arcane Options
                    }
                }
            }
        }

        private void Action1Clicked(){
            if(_card is MonsterCard){
                if(_card.IsFaceDown){
                    if(_card.CanFlip){
                        FlipCard(_buttonPlace);
                        HideCardButtons();
                    }
                }else{
                    MonsterCard monster = _card as MonsterCard;
                    if(monster.IsInAttackMode){
                        if(monster.CanAttack){
                            // Attack(_buttonPlace);
                        }
                    }else{// Is In deffense
                        if(monster.CanChangeMode){ //Not needed but keeped for readbility reasons
                            _buttonPlace.ChangeMonsterToAtk();
                           HideCardButtons();
                        }
                    }
                }
            }else{
                //Arcane Card
            }
            _uiManager.ActionSelected();
        }
        private void Action2Clicked(){
            if(_card is MonsterCard){
                MonsterCard monster = _card as MonsterCard;
                if(monster.IsInAttackMode && monster.CanChangeMode){
                    _buttonPlace.ChangeMonsterToDef();
                }
            }else{
                //Arcane Card
            }

            _card.SetShowButtons(false);
            HideOptions();
            _uiManager.ActionSelected();
        }
        private void FlipCard(BoardPlace place) { place.FlipCard(); }
    }
}