using System.Collections;

namespace Mistix{

    public class BS_04_Fusion : AbstractState{
        public BS_04_Fusion(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}
        public override void Enter(){ BattleSM.StartCoroutine(FusionPhaseRoutine()); }
        public override void Exit(){}
        private IEnumerator FusionPhaseRoutine(){
            BattleSM.MoveUICardOffScreen(); //Move UI card para fora da tela
            BattleSM.MoveHandOffScreen(); //Move cartas não selecionadas na mao para fora da tela

            BattleSM.StartFusionRoutine(); //Inicia Rotina de fusão

            while(BattleSM.IsFusionEnded() == false){ //Aguarda o final da fusão
                yield return null;
            }

            BattleSM.ChangeState(BattleSM.CardStatSelPhase); //Muda para Card status Select State
            
            yield return null;
        }

        public override string ToString() { return "Fusion"; }
    }
}