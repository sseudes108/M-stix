using System.Collections;
using UnityEngine;

namespace Mistix{
    public class AIS_01_CardSelection : AbstractState{
        public AIS_01_CardSelection(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ AISM.StartCoroutine(CardSelectRoutine()); }

        public override void Exit(){}

        private IEnumerator CardSelectRoutine() {
            AISM.StartCardSelection();
            yield return null;
        }

        public override string ToString(){
            return "Card Sel.";
        }
    }
}