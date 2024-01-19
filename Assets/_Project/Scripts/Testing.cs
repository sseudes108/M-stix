using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour{

    Hand _playerHand;

    private void Awake() {
        _playerHand = GetComponent<Hand>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)){
            _playerHand.DrawCards();
        }
        
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
