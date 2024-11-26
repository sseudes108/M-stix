public class AIFusioner : AIAction{
    public AIFusioner(AIActor actor) { _Actor = actor; }

    // public void ResetBoardFusion(){
    //     _actor.ResetBoardFusion();
    //     _manager.AI.CardOrganizer.ResetBoardFusion();
    // }

    /// <summary>
    /// cardToFusion is the card on the field the will be used after the fusion from hand
    /// </summary>
    public void BoardFusion(Card cardToFusion){
        _Actor.SetBoardFusion(cardToFusion);
        _Actor.CardOrganizer.SetBoardFusion(cardToFusion);
    }

    public void CheckForBoardMonsterFusion(MonsterCard monsterToPlace){
        var lvl = monsterToPlace.Level;

        switch(lvl){
            case 7:
                if(_AI.Actor.FieldChecker.Lvl7OnAIField.Count > 0){
                    BoardFusion(_AI.Actor.FieldChecker.Lvl7OnAIField[0]);
                }
            break;

            case 6:
                if(_AI.Actor.FieldChecker.Lvl6OnAIField.Count > 0){
                    BoardFusion(_AI.Actor.FieldChecker.Lvl6OnAIField[0]);
                }
            break;

            case 5:
                if(_AI.Actor.FieldChecker.Lvl5OnAIField.Count > 0){
                    BoardFusion(_AI.Actor.FieldChecker.Lvl5OnAIField[0]);
                }
            break;

            case 4:
                if(_AI.Actor.FieldChecker.Lvl4OnAIField.Count > 0){
                    BoardFusion(_AI.Actor.FieldChecker.Lvl4OnAIField[0]);
                }
            break;

            case 3:
                if(_AI.Actor.FieldChecker.Lvl3OnAIField.Count > 0){
                    BoardFusion(_AI.Actor.FieldChecker.Lvl3OnAIField[0]);
                }
            break;

            case 2:
                if(_AI.Actor.FieldChecker.Lvl2OnAIField.Count > 0){
                    BoardFusion(_AI.Actor.FieldChecker.Lvl2OnAIField[0]);
                }
            break;
        }
    }
}

// public class AIFusioner : AIAction{
//     // public AIFusioner(AIActorSO actor, AIManagerSO manager){
//     //     _actor = actor;
//     //     _manager = manager;
//     // }

//     public void ResetBoardFusion(){
//         // _actor.ResetBoardFusion();
//         // _manager.AI.CardOrganizer.ResetBoardFusion();
//     }

//     /// <summary>
//     /// cardToFusion is the card on the field the will be used after the fusion from hand
//     /// </summary>
//     public void BoardFusion(Card cardToFusion){
//         // _actor.SetBoardFusion(cardToFusion);
//         // _manager.AI.CardOrganizer.SetBoardFusion(cardToFusion);
//     }

//     public void CheckForBoardMonsterFusion(MonsterCard monsterToPlace){
//         var lvl = monsterToPlace.Level;

//         // switch(lvl){
//         //     case 7:
//         //         if(_actor.FieldChecker.Lvl7OnAIField.Count > 0){
//         //             BoardFusion(_actor.FieldChecker.Lvl7OnAIField[0]);
//         //         }
//         //     break;

//         //     case 6:
//         //         if(_actor.FieldChecker.Lvl6OnAIField.Count > 0){
//         //             BoardFusion(_actor.FieldChecker.Lvl6OnAIField[0]);
//         //         }
//         //     break;

//         //     case 5:
//         //         if(_actor.FieldChecker.Lvl5OnAIField.Count > 0){
//         //             BoardFusion(_actor.FieldChecker.Lvl5OnAIField[0]);
//         //         }
//         //     break;

//         //     case 4:
//         //         if(_actor.FieldChecker.Lvl4OnAIField.Count > 0){
//         //             BoardFusion(_actor.FieldChecker.Lvl4OnAIField[0]);
//         //         }
//         //     break;

//         //     case 3:
//         //         if(_actor.FieldChecker.Lvl3OnAIField.Count > 0){
//         //             BoardFusion(_actor.FieldChecker.Lvl3OnAIField[0]);
//         //         }
//         //     break;

//         //     case 2:
//         //         if(_actor.FieldChecker.Lvl2OnAIField.Count > 0){
//         //             BoardFusion(_actor.FieldChecker.Lvl2OnAIField[0]);
//         //         }
//         //     break;
//         // }

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