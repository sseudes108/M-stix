using System.Collections;

namespace Mistix{
    public class BS_11_End : AbstractState{
        public BS_11_End(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(EndPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator EndPhaseRoutine(){
            yield return null;
        }
        
        public override string ToString(){ return "End"; }
    }
}