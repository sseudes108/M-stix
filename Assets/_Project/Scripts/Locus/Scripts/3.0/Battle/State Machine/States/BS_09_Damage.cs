using System.Collections;

namespace Mistix{

    public class BS_09_Damage : AbstractState{
        public BS_09_Damage(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ BattleSM.StartCoroutine(DamagePhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator DamagePhaseRoutine(){
            yield return null;
        }
        public override string ToString(){ return "Damage"; }
    }
}