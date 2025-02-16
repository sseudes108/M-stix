using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_11_End : AbstractState{
        public BS_11_End(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ BattleSM.StartCoroutine(EndPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator EndPhaseRoutine(){
            BattleSM.EndTurn();
            yield return new WaitForSeconds(1f);
            BattleSM.ChangeState(BattleSM.StartPhase);
            yield return null;
        }
        
        public override string ToString(){ return "End"; }
    }
}