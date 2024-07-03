using UnityEngine;

public class Teste : MonoBehaviour {
    public static Teste Instance;
    public CardCreator cardCreator;
    
    public CardSO cardData;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)) {
            Instantiate(cardCreator.CreateCard(cardData));
        }
    }
}