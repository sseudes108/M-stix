using System.Collections;

namespace Mistix{
    public class BS_03_CardSelection : AbstractState{
        public BS_03_CardSelection(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardSelectPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator CardSelectPhaseRoutine() {
            yield return null;
        }

        public override string ToString() { return "Card Sel."; }
    }
}