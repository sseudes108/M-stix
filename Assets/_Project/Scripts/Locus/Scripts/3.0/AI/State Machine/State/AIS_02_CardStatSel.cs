using System.Collections;
using UnityEngine;

namespace Mistix{
    public class AIS_02_CardStatSel : AbstractState{
        public AIS_02_CardStatSel(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ AISM.StartCoroutine(CardStatsSelectRoutine()); }

        public override void Exit(){}

        private IEnumerator CardStatsSelectRoutine() {
            Debug.Log("AI - Selecting Card Stats");
            yield return null;
        }

        public override string ToString(){
            return "Card Stat Sel.";
        }
    }
}