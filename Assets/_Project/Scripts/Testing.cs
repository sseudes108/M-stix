using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour{

    PlayerHand _playerHand;

    private void Awake() {
        _playerHand = GetComponent<PlayerHand>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            _playerHand.DrawCards();
        }
        
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.F)){
            FusionLogic.Instance.Fusion(CardSelector.Instance.SelectedCards);
        }
    }
}
