using System.Collections;

namespace Mistix{
    public class BS_07_Action : AbstractState{
        public BS_07_Action(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(ActionPhaseRoutine()); }

        public override void Exit(){}
        private IEnumerator ActionPhaseRoutine(){
            yield return null;
        }

        public override string ToString(){ return "Action"; }
    }
}