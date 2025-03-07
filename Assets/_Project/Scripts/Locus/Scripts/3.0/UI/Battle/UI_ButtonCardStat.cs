using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mistix{
    public class UI_ButtonCardStat : MonoBehaviour {
        [SerializeField] private GameObject _statSelButtonsContainer;
        [SerializeField] private Button _statButton1, _statButton2;
        [SerializeField] private TextMeshProUGUI _statText1, _statText2;
        private UIManager _uiManager;

        private void Awake() { _uiManager = GetComponent<UIManager>(); }

        public void ShowOptions(Card card){
            _statSelButtonsContainer.SetActive(true);
            SetButtonText(card);
            _statButton1.onClick.AddListener(_uiManager.Option1_Clicked);
            _statButton2.onClick.AddListener(_uiManager.Option2_Clicked);
        }

        public void HideOptions(){
            _statSelButtonsContainer.SetActive(false);
            _statButton1.onClick.RemoveAllListeners();
            _statButton2.onClick.RemoveAllListeners();
        }

        public void SetButtonText(Card card){
            if(card is MonsterCard){

                var monsterCard = card as MonsterCard;
                if(!monsterCard.AnimaSelected){
        
                    //Anima
                    _statText1.text = $"{monsterCard.FirstAnima}";
                    _statText2.text = $"{monsterCard.SecondAnima}";
                }else if(!monsterCard.ModeSelected){
        
                    //Mode
                    _statText1.text = $"Attack";
                    _statText2.text = $"Deffense";
                }else if(!monsterCard.FusionedCard){
                    
                    //Face
                    _statText1.text = $"Face Up";
                    _statText2.text = $"Face Down";
                }
            }
        }
    }
}