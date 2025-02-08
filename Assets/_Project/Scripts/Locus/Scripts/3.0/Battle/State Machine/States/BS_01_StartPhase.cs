using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_01_StartPhase : AbstractState{
        public BS_01_StartPhase(BattleSM battleSM) : base(battleSM){}

        public override void Enter() { BattleSM.StartCoroutine(StartPhaseRoutine()); }

        public override void Exit() { }

        private IEnumerator StartPhaseRoutine() {
            BattleSM.LightOffAllPlaces(); //Apagar board places

            yield return new WaitForSeconds(1f);

            BattleSM.LightUpAllPlaces(); //Iluminar board places

            //Atualizar UI - HPs e Contagem de decks

            //Passar para a Draw Phase

            yield return null;
        }
    }
}