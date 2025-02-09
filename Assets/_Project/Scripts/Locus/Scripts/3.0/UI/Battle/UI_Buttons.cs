using TMPro;
using UnityEngine;

namespace Mistix{
    
    public class UI_Buttons : MonoBehaviour {
        [SerializeField] private GameObject _actionButton;
        [SerializeField] private TextMeshProUGUI _actionButtonText;

        public void ShowEndSelectionButton() {
            _actionButton.SetActive(true);
            _actionButtonText.text = "End Selection";
        }

        public void HideEndSelectionButton() { 
            _actionButton.SetActive(false); 
        }
    }
}