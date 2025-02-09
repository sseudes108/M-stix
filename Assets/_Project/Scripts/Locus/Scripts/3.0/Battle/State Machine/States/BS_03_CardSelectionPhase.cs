using System.Collections;

namespace Mistix{
    public class BS_03_CardSelectionPhase : AbstractState{
        public BS_03_CardSelectionPhase(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardSelectPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator CardSelectPhaseRoutine() {
            BattleSM.AllowCardSelection(); //Itera sobre as cartas na mao do jogador e libera a seleção
            
            while(BattleSM.IsCardSelectionEnded() == false){ //Aguardar o botão de confirmar ser pressionado (Botão só é mostrado caso alguma carta esteja selecionada)
                yield return null;
            }

            BattleSM.BlockCardSelection(); //Itera sobre as cartas na mao do jogador e bloqueia a seleção
            
            BattleSM.ChangeState(BattleSM.FusionPhase); //Passar para a próxima fase (Fusão)

            yield return null;
        }

        public override string ToString() { return "Card Sel."; }
    }
}