using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {

    public HandPlayer playerHand;

    private void Awake() {
        playerHand = GetComponent<HandPlayer>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)){
            playerHand.DrawCard();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}