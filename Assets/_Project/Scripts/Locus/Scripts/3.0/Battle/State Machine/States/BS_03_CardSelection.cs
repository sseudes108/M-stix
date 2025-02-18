using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_03_CardSelection : AbstractState{
        public BS_03_CardSelection(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardSelectPhaseRoutine()); }

        public override void Exit(){}

        private IEnumerator CardSelectPhaseRoutine(){
            if(BattleSM.IsPlayerTurn()){
                BattleSM.AllowCardSelection(); //Itera sobre as cartas na mao do jogador e libera a seleção
            }else{
                BattleSM.ChangeAISMToCardSelectionPhase();
            }

            BattleSM.ResetCardSelectionEnded();//Reseta o bool que verifica se as cartas foram selecionadas
            while(BattleSM.IsCardSelectionEnded() == false){//Aguardar o botão de confirmar ser pressionado (Botão só é mostrado caso alguma carta esteja selecionada)
                yield return null;
            }

            if(BattleSM.IsPlayerTurn()){
                BattleSM.BlockCardSelection(); //Itera sobre as cartas na mao do jogador e bloqueia a seleção
            }

            BattleSM.ChangeState(BattleSM.FusionPhase); //Passar para a próxima fase (Fusão)

            yield return null;
        }

        public override string ToString() { return "Card Sel."; }
    }
}