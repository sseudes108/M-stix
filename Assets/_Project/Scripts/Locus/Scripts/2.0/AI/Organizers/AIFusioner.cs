// public class AIFusioner : AIAction{
//     public AIFusioner(AI ai, AIActor actor) { 
//         _AI = ai;
//         _Actor = actor; }

//     /// <summary>
//     /// cardToFusion is the card on the field the will be used after the fusion from hand
//     /// </summary>
//     public void BoardFusion(Card cardToFusion){
//         _Actor.SetBoardFusion(cardToFusion);
//     }

//     public void CheckForBoardMonsterFusion(MonsterCard monsterToPlace){
//         var lvl = monsterToPlace.Level;

//         switch(lvl){
//             case 7:
//                 if(_AI.Actor.FieldChecker.Lvl7OnAIField.Count > 0){
//                     BoardFusion(_AI.Actor.FieldChecker.Lvl7OnAIField[0]);
//                 }
//             break;

//             case 6:
//                 if(_AI.Actor.FieldChecker.Lvl6OnAIField.Count > 0){
//                     BoardFusion(_AI.Actor.FieldChecker.Lvl6OnAIField[0]);
//                 }
//             break;

//             case 5:
//                 if(_AI.Actor.FieldChecker.Lvl5OnAIField.Count > 0){
//                     BoardFusion(_AI.Actor.FieldChecker.Lvl5OnAIField[0]);
//                 }
//             break;

//             case 4:
//                 if(_AI.Actor.FieldChecker.Lvl4OnAIField.Count > 0){
//                     BoardFusion(_AI.Actor.FieldChecker.Lvl4OnAIField[0]);
//                 }
//             break;

//             case 3:
//                 if(_AI.Actor.FieldChecker.Lvl3OnAIField.Count > 0){
//                     BoardFusion(_AI.Actor.FieldChecker.Lvl3OnAIField[0]);
//                 }
//             break;

//             case 2:
//                 if(_AI.Actor.FieldChecker.Lvl2OnAIField.Count > 0){
//                     BoardFusion(_AI.Actor.FieldChecker.Lvl2OnAIField[0]);
//                 }
//             break;
//         }
//     }
// }