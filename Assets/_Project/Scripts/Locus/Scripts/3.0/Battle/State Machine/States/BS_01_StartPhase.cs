using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_01_StartPhase : AbstractState{
        public BS_01_StartPhase(StateMachine stateMachine) : base(stateMachine){}

        public override void Enter(){
            Debug.Log("Start Phase - Enter");
        }

        public override void Exit(){

        }

        private IEnumerator StartPhaseRoutine(){
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
}