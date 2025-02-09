using System.Collections;

namespace Mistix{
    public class BS_03_CardSelection : AbstractState{
        public BS_03_CardSelection(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardSelectPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator CardSelectPhaseRoutine() {
            BattleSM.AllowCardSelection(); //Iterar sobre as cartas na mao do jogador e liberar sua seleção

            //Aguardar o botão de confirmar ser pressionado
            //Passar para a próxima fase
            yield return null;
        }

        public override string ToString() { return "Card Sel."; }
    }
}