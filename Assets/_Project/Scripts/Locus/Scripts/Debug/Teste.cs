using UnityEngine;
using UnityEngine.SceneManagement;

public class Teste : MonoBehaviour {
    public static Teste Instance;
    public CardCreator cardCreator;
    
    public CardSO cardData;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)) {
            var newcard = Instantiate(cardCreator.CreateCard(cardData));
            newcard.transform.position = new Vector3(0, 3, 0);
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            System.GC.Collect();
        }
    }
}