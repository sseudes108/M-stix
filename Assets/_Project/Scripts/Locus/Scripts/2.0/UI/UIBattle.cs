// using TMPro;
// using UnityEngine;

// public class UIBattle : MonoBehaviour {    

//     [SerializeField] private UIEventHandlerSO _UIEventHandler;

//     [Header("Player")]
//     [SerializeField] private TextMeshProUGUI _playerLP;
//     [SerializeField] private TextMeshProUGUI _playerDeck;

//     [Header("Enemy")]
//     [SerializeField] private TextMeshProUGUI _enemyLP;
//     [SerializeField] private TextMeshProUGUI _enemyDeck;

//     private void OnEnable() {
//         _UIEventHandler.OnLifePointsUpdate.AddListener(OnLifePointsUpdate);
//         _UIEventHandler.OnDeckCountUpdate.AddListener(OnDeckCountUpdate);
//     }

//     private void OnDisable() {
//         _UIEventHandler.OnLifePointsUpdate.AddListener(OnLifePointsUpdate);
//         _UIEventHandler.OnDeckCountUpdate.AddListener(OnDeckCountUpdate);
//     }

//     private void OnLifePointsUpdate(bool isPlayerUpdate, int lifePoints){
//         if(isPlayerUpdate){
//             UpdatePlayerHealth(lifePoints);
//         }else{
//             UpdateEnemyHealth(lifePoints);
//         }
//     }

//     private void OnDeckCountUpdate(bool isPlayerUpdate, int deckCount){
//         if(isPlayerUpdate){
//             UpdatePlayerDeckCount(deckCount);
//         }else{
//             UpdateEnemyDeckCount(deckCount);
//         }
//     }

//     public void UpdatePlayerHealth(int playerLP){
//         _playerLP.text = $"Life Points: {playerLP}";
//     }

//     public void UpdateEnemyHealth(int enemyLP){
//         _enemyLP.text = $"Life Points: {enemyLP}";
//     }

//     public void UpdatePlayerDeckCount(int pDeckCount){
//         _playerDeck.text = $"Deck: {pDeckCount}";
//     }

//     public void UpdateEnemyDeckCount(int eDeckCount){
//         _enemyDeck.text = $"Deck: {eDeckCount}";
//     }
// }