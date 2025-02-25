using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_01_Start : AbstractState{
        public BS_01_Start(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter() { BattleSM.StartCoroutine(StartPhaseRoutine()); }

        public override void Exit() { }

        private IEnumerator StartPhaseRoutine() {
            if(BattleSM.IsFirstTurn()){
                UpdateUIFirstTurn(); //Atualizar UI
                BattleSM.LightOffAllPlaces(); //Apagar board places

                yield return new WaitForSeconds(1f);

                BattleSM.LightUpAllPlaces(); //Iluminar board places

                yield return new WaitForSeconds(2f);
                
            }else{
                BattleSM.UpdateTurn(); //Atualizar UI - Turno
                if(BattleSM.IsPlayerTurn()){
                    BattleSM.MoveHandOnScreen();
                }
            }
            
            yield return new WaitForSeconds(1f);
            BattleSM.ChangeState(BattleSM.DrawPhase); //Passar para a Draw Phase

            yield return null;
        }

        private void UpdateUIFirstTurn(){
            BattleSM.UpdateTurn(); //Atualizar UI - Turno
            BattleSM.ResetLifePoints(); //Atualizar UI - LifePoints
            BattleSM.ResetDeckCount(); //Atualizar UI - DeckCount
        }

        public override string ToString() { return "Start"; }
    }
}