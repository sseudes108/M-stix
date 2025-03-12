using System.Collections.Generic;
using UnityEngine;
namespace Mistix{
    public class AIB_FieldChecker : MonoBehaviour {
        public List<MonsterCard> AIMonstersOnFieldThatCanAttack { get; private set; } = new();

        #region AI
            #region AI - Monsters On Field
                public List<MonsterCard> Lvl2OnAIField { get; private set; } = new();
                public List<MonsterCard> Lvl3OnAIField { get; private set; } = new();
                public List<MonsterCard> Lvl4OnAIField { get; private set; } = new();
                public List<MonsterCard> Lvl5OnAIField { get; private set; } = new();
                public List<MonsterCard> Lvl6OnAIField { get; private set; } = new();
                public List<MonsterCard> Lvl7OnAIField { get; private set; } = new();
            #endregion
        #endregion

        #region Player
            #region Player - Monsters On Field
                public List<MonsterCard> Lvl2OnPlayerField { get; private set; } = new();
                public List<MonsterCard> Lvl3OnPlayerField { get; private set; } = new();
                public List<MonsterCard> Lvl4OnPlayerField { get; private set; } = new();
                public List<MonsterCard> Lvl5OnPlayerField { get; private set; } = new();
                public List<MonsterCard> Lvl6OnPlayerField { get; private set; } = new();
                public List<MonsterCard> Lvl7OnPlayerField { get; private set; } = new();
            #endregion
        #endregion

        public void OrganizeAIMonsterCardsOnField(List<MonsterCard> monstersOnAIField){
            ClearAIListsOnField();
            foreach(var card in monstersOnAIField){

                int lvl = card.Level;
                if(card.IsInAttackMode && card.CanAttack){
                    AIMonstersOnFieldThatCanAttack.Add(card);
                }

                switch (lvl){
                    case 2:
                        Lvl2OnAIField.Add(card);
                    break;

                    case 3:
                        Lvl3OnAIField.Add(card);
                    break;

                    case 4:
                        Lvl4OnAIField.Add(card);
                    break;

                    case 5:
                        Lvl5OnAIField.Add(card);
                    break;

                    case 6:
                        Lvl6OnAIField.Add(card);
                    break;

                    case 7:
                        Lvl7OnAIField.Add(card);
                    break;
                }
            }
        }
    
        public void ClearAIListsOnField(){
            AIMonstersOnFieldThatCanAttack.Clear();

            Lvl2OnAIField.Clear();
            Lvl3OnAIField.Clear();
            Lvl4OnAIField.Clear();
            Lvl5OnAIField.Clear();
            Lvl6OnAIField.Clear();
            Lvl7OnAIField.Clear();

            Lvl2OnPlayerField.Clear();
            Lvl3OnPlayerField.Clear();
            Lvl4OnPlayerField.Clear();
            Lvl5OnPlayerField.Clear();
            Lvl6OnPlayerField.Clear();
            Lvl7OnPlayerField.Clear();
        }

        // public MonsterCard CheckCardLvlOnField(int level){
        //     switch(level){
        //         case 7:
        //             if(Lvl7OnAIField.Count > 0){
        //                 return Lvl7OnAIField[0];
        //             }
        //         break;

        //         case 6:
        //             if(Lvl6OnAIField.Count > 0){
        //                 return Lvl6OnAIField[0];
        //             }
        //         break;

        //         case 5:
        //             if(Lvl5OnAIField.Count > 0){
        //                 return Lvl5OnAIField[0];
        //             }
        //         break;

        //         case 4:
        //             if(Lvl4OnAIField.Count > 0){
        //                 return Lvl4OnAIField[0];
        //             }
        //         break;

        //         case 3:
        //             if(Lvl3OnAIField.Count > 0){
        //                 return Lvl3OnAIField[0];
        //             }
        //         break;

        //         case 2:
        //             if(Lvl2OnAIField.Count > 0){
        //                 return Lvl2OnAIField[0];
        //             }
        //         break;
        //     }
        //     return null;
        // }
    }
}