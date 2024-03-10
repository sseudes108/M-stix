using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {

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

        if(Input.GetKeyDown(KeyCode.Y)){
            BattleManager.Instance.CameraManager.FusionFailed();
        }
    }
}