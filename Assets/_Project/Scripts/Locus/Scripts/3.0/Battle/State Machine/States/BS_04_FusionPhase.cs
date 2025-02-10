using System.Collections;

namespace Mistix{

    public class BS_04_FusionPhase : AbstractState{
        public BS_04_FusionPhase(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(FusionPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator FusionPhaseRoutine(){
            BattleSM.MoveUICardOffScreen(); //Move UI card para fora da tela
            BattleSM.MoveHandOffScreen(); //Move cartas na mao para fora da tela

            BattleSM.StartFusionRoutine();//Iniciar Rotina de fusão
            
            //Mudar para Card status Select State
            yield return null;
        }

        public override string ToString() { return "Fusion"; }
    }
}