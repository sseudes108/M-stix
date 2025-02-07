using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_01_StartPhase : AbstractState{
        public BS_01_StartPhase(StateMachine stateMachine, BattleManager battleManager) : base(stateMachine, battleManager){}

        public override void Enter(){
            // Debug.Log("Start Phase - Enter");
            StateMachine.StartCoroutine(StartPhaseRoutine());
        }

        public override void Exit(){

        }

        private IEnumerator StartPhaseRoutine(){
            yield return new WaitForSeconds(1f);
            BattleManager.BoardManager.StartPhase();
            yield return null;
        }
    }
}