using UnityEngine;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance;
    [SerializeField] private CardManager _cardManager;

    public CardCreator CardCreator => _cardManager.CardCreator;

    private void Awake(){
        SetSingletonInstance();
    }

    private void SetSingletonInstance(){
        if (Instance != null){
            Debug.Log("More than one instance of BattleManager");
            Destroy(gameObject);
        }
        Instance = this;
    }
}