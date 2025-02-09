using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mistix{
    
    public class UI_BattleButtons : MonoBehaviour {
        private Button _actionButton;
        private UIManager _uiManager;
        [SerializeField] private GameObject _actionButtonObject;
        [SerializeField] private TextMeshProUGUI _actionButtonText;

        private void Awake() {
            _uiManager = GetComponent<UIManager>();
            
            _actionButton = _actionButtonObject.GetComponent<Button>();
            _actionButton.onClick.AddListener(EndCardSelection);
        }

        public void ShowEndSelectionButton() {
            _actionButtonObject.SetActive(true);
            _actionButtonText.text = "End Selection";
        }

        public void HideEndSelectionButton() { 
            _actionButtonObject.SetActive(false); 
        }

        private void EndCardSelection(){
            _uiManager.EndCardSelection();
            HideEndSelectionButton();
        }
    }
}