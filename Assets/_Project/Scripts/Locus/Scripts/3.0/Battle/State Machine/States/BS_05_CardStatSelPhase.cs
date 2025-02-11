using System.Collections;

namespace Mistix{

    public class BS_05_CardStatSelPhase : AbstractState{
        public BS_05_CardStatSelPhase(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardStatSelectionRoutine()); }

        public override void Exit(){}

        private IEnumerator CardStatSelectionRoutine(){
            yield return null;
        }

        public override string ToString(){ return "Card Stat Sel."; }
    }
}