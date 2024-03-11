using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {

    [SerializeField] private Card _card, _card2;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if(Input.GetKeyDown(KeyCode.F)){
            if(BattleManager.Instance.BattleStateManager.CurrentPhase == BattleManager.Instance.CardSelectionPhase &&
                BattleManager.Instance.CardSelector.GetSelectedCards().Count > 0){
                
                BattleManager.Instance.CardSelectionPhase.EndSelection();
            }
        }
    }
}