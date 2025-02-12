using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mistix{
    
    public class UI_ButtonAction : MonoBehaviour {
        private Button _actionButton;
        private UIManager _uiManager;
        [SerializeField] private GameObject _actionButtonObject;
        [SerializeField] private TextMeshProUGUI _actionButtonText;

        private void Awake() {
            _uiManager = GetComponent<UIManager>();
            
            _actionButton = _actionButtonObject.GetComponent<Button>();
            _actionButton.onClick.AddListener(ActionButtonClicked);
        }

        public void ShowEndCardSelectionButton() {
            _actionButtonObject.SetActive(true);
            _actionButtonText.text = "End Selection";
        }

        public void HideActionButton() { 
            _actionButtonObject.SetActive(false); 
        }

        // É chamada ao clique do botão
        private void ActionButtonClicked(){
            //Checar o Current State da battle e tomar chamar os metodos correspondentes
            if(_uiManager.IsCardSelectionPhase()){
                _uiManager.EndCardSelection();
                HideActionButton();
                return;
            }
        }
    }
}