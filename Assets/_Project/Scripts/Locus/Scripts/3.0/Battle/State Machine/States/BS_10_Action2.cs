using System.Collections;

namespace Mistix{
    public class BS_10_Action2 : AbstractState{
        public BS_10_Action2(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ BattleSM.StartCoroutine(Action2PhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator Action2PhaseRoutine(){
            yield return null;
        }
        public override string ToString(){ return "Action 2"; }
    }
}