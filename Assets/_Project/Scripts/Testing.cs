using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour{
    
    [SerializeField] private List<Card> cards;

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            Fusion.Instance.StartFusionLine(cards);
        }

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
        }
    }
}