using UnityEngine;
namespace Mistix{
    public class AIB_Fusioner : MonoBehaviour {
        private AIActor _actor;

        private void Awake() { _actor = GetComponent<AIActor>(); }

        public void BoardFusion(Card cardToFusion){
            _actor.SetBoardFusion(cardToFusion);
        }

        public void CheckForBoardMonsterFusion(MonsterCard monsterToPlace){
            var lvl = monsterToPlace.Level;

            switch(lvl){
                // case 7:
                //     if(_AI.Actor.FieldChecker.Lvl7OnAIField.Count > 0){
                //         BoardFusion(_AI.Actor.FieldChecker.Lvl7OnAIField[0]);
                //     }
                // break;

                // case 6:
                //     if(_AI.Actor.FieldChecker.Lvl6OnAIField.Count > 0){
                //         BoardFusion(_AI.Actor.FieldChecker.Lvl6OnAIField[0]);
                //     }
                // break;

                // case 5:
                //     if(_AI.Actor.FieldChecker.Lvl5OnAIField.Count > 0){
                //         BoardFusion(_AI.Actor.FieldChecker.Lvl5OnAIField[0]);
                //     }
                // break;

                case 4:
                    if(_actor.Lvl4OnAIField() > 0){
                        BoardFusion(_actor.GetLvl4OnField());
                    }
                break;

                case 3:
                    if(_actor.Lvl3OnAIField() > 0){
                        BoardFusion(_actor.GetLvl3OnField());
                    }
                break;

                case 2:
                    if(_actor.Lvl2OnAIField() > 0){
                        BoardFusion(_actor.GetLvl2OnField());
                    }
                break;
            }
        }
    }
}