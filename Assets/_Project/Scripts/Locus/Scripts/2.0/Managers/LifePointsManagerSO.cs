// using System.Collections;
// using UnityEngine;

// [CreateAssetMenu(fileName = "LifePointsManagerSO", menuName = "Mistix/Manager/LifePoints", order = 0)]
// public class LifePointsManagerSO : ScriptableObject {
//     [field:SerializeField] private UIEventHandlerSO _UIEventHandler;

//     private const int INITIALP = 8100;
//     public int PlayerLP { get; private set; } = 0;
//     public int EnemyLP { get; private set; } = 0;

//     public void ResetLifePoints(GameManager manager){
//         PlayerLP = 0;
//         EnemyLP = 0;

//         _UIEventHandler.UpdateLifePoints(true, PlayerLP);
//         _UIEventHandler.UpdateLifePoints(false, EnemyLP);

//         manager.StartCoroutine(ResetPlayerLife());
//         manager.StartCoroutine(ResetEnemyLife());
//     }

//     private IEnumerator ResetPlayerLife(){
//         while(PlayerLP < 8100){
//             PlayerLP += 100;
//             yield return new WaitForSeconds(0.01f);
//             _UIEventHandler.UpdateLifePoints(true, PlayerLP);
//         }

//         if(PlayerLP > INITIALP){
//             PlayerLP = INITIALP;
//         }

//         yield return null;
//     }
//     private IEnumerator ResetEnemyLife(){
//         while(EnemyLP < 8100){
//             EnemyLP += 100;
//             yield return new WaitForSeconds(0.01f);
//             _UIEventHandler.UpdateLifePoints(false, EnemyLP);
//         }

//         if(EnemyLP > INITIALP){
//             EnemyLP = INITIALP;
//         }

//         yield return null;
//     }
    
//     private IEnumerator UpdateLifePoints(bool isPlayer, int lifePoints){
//         if(isPlayer){
//             int targetPoints = PlayerLP + lifePoints;
//             while(PlayerLP < targetPoints){
//                 PlayerLP += 100;
//                 yield return new WaitForSeconds(0.01f);
//                 _UIEventHandler.UpdateLifePoints(true, PlayerLP);
                
//             }

//             if(PlayerLP > targetPoints){
//                 PlayerLP = targetPoints;
//                 _UIEventHandler.UpdateLifePoints(true, PlayerLP);
//             }

//         }else{
//             int targetPoints = EnemyLP + lifePoints;
//             while(EnemyLP < targetPoints){
//                 EnemyLP += 100;
//                 yield return new WaitForSeconds(0.01f);
//                 _UIEventHandler.UpdateLifePoints(false, EnemyLP);
//             }

//             if(EnemyLP > targetPoints){
//                 EnemyLP = targetPoints;
//                 _UIEventHandler.UpdateLifePoints(false, EnemyLP);
//             }
//         }
//     } 

// }