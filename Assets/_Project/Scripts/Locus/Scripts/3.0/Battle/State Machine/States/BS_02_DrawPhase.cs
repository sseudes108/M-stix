using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_02_DrawPhase : AbstractState{
        public BS_02_DrawPhase(BattleSM battleSM) : base(battleSM) {}

        public override void Enter() { BattleSM.StartCoroutine(DrawPhaseRoutine()); }
        public override void Exit() {}

        private IEnumerator DrawPhaseRoutine() {
            BattleSM.CheckPositionsInHand(); //Checar posições livres na mão

            BattleSM.DrawCards(); //Dono do turno saca as cartas, se for turno 1 os dois sacam
            
            yield return new WaitForSeconds(4f);

            BattleSM.ChangeState(BattleSM.CardSelectionPhase);// Passar para Card Selection State
            yield return null;
        }

        public override string ToString() { return "Draw"; }
    }
}