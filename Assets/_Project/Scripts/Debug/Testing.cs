using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {

    public HandPlayer playerHand;

    private void Awake() {
        playerHand = GetComponent<HandPlayer>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)){
            playerHand.DrawCards();
        }
        
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if(Input.GetKeyDown(KeyCode.F)){
            if(BattleManager.Instance.BattleStateManager.CurrentPhase == BattleManager.Instance.SelectionPhase &&
                BattleManager.Instance.CardSelector.GetSelectedCards().Count > 0){
                
                BattleManager.Instance.SelectionPhase.EndSelection();
            }
        }
    }
}