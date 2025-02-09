using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_01_StartPhase : AbstractState{
        public BS_01_StartPhase(BattleSM battleSM) : base(battleSM){}

        public override void Enter() { BattleSM.StartCoroutine(StartPhaseRoutine()); }

        public override void Exit() { }

        private IEnumerator StartPhaseRoutine() {
            UpdateUI(); //Atualizar UI

            BattleSM.LightOffAllPlaces(); //Apagar board places

            yield return new WaitForSeconds(1f);

            BattleSM.LightUpAllPlaces(); //Iluminar board places

            yield return new WaitForSeconds(2f);

            BattleSM.ChangeState(BattleSM.DrawPhase); //Passar para a Draw Phase

            yield return null;
        }

        private void UpdateUI(){
            BattleSM.UpdateTurn(); //Atualizar UI - Turno
            BattleSM.ResetLifePoints(); //Atualizar UI - LifePoints
            BattleSM.ResetDeckCount(); //Atualizar UI - DeckCount
        }

        public override string ToString() { return "Start"; }
    }
}