// using TMPro;
// using UnityEngine;

// public class TesterUI : MonoBehaviour {
//     public static TesterUI Instance;

//     public virtual void Awake() {
//         if(Instance != null){
//             Debug.LogError("More Than One Instance");
//             Destroy(Instance);
//         }
//         Instance = this;
//     }
    
//     public CardManagerSO _cardManager;

//     public TextMeshProUGUI Phase;
//     public TextMeshProUGUI AIState;
//     public TextMeshProUGUI Turn;
//     public TextMeshProUGUI T;


//     public void UpdateBattlePhaseText(string currentphase){
//         Phase.text = $"Phase: {currentphase}";
//     }
    
//     public void UpdateAIStateText(string state){
//         AIState.text = $"AI: {state}";
//     }

//     public void UpdateTurnText(string turn, string owner){
//         Turn.text = $"Turn: {turn} - {owner}";
//     }

//     public void UpdateTLabelText(string text, string t){
//         T.text = $"{text}: {t}";
//     }
// }