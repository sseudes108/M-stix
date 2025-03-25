using System.Collections;

namespace Mistix{

    public class BS_05_CardStatSel : AbstractState{
        public BS_05_CardStatSel(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardStatSelectionRoutine()); }

        public override void Exit(){}

        private IEnumerator CardStatSelectionRoutine(){
            BattleSM.ResetCardStats();
            
            if(BattleSM.IsPlayerTurn()){
                BattleSM.ShowCardStatOptions(BattleSM.GetFusionResultCard());//Apresentar botoes de seleção de status
            }else{
                BattleSM.ChangeAISMToCardStatSelPhase();
                //Change AISM To Stat Card Select State
            }

            BattleSM.ResetCardStatSelectionEnded(); // Reseta o bool que verifica se as seleções foram feitas
            while(BattleSM.IsAllStatsSelected() == false){//Aguardar todas as seleções
                yield return null;
            }

            BattleSM.ChangeState(BattleSM.BoardPlaceSelPhase);//mudar para Board place selection phase
            yield return null;
        }

        public override string ToString(){ return "Card Stat Sel."; }
    }
}