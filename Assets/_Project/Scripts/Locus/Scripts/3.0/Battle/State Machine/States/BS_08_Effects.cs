using System.Collections;

namespace Mistix{

    public class BS_08_Effects : AbstractState{
        public BS_08_Effects(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(EffectPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator EffectPhaseRoutine(){
            yield return null;
        }
        public override string ToString(){ return "Effect"; }
    }
}