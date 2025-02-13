using System.Collections;

namespace Mistix{

    public class BS_05_CardStatSel : AbstractState{
        public BS_05_CardStatSel(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(CardStatSelectionRoutine()); }

        public override void Exit(){}

        private IEnumerator CardStatSelectionRoutine(){
            BattleSM.ShowCardStatOptions(BattleSM.GetFusionResultCard());//Apresentar botoes de seleção de status

            while(BattleSM.IsAllStatsSelected() == false){//Aguardar todas as seleções
                yield return null;
            }

            BattleSM.ChangeState(BattleSM.BoardPlaceSelPhase); //mudar para Board place selection phase
            yield return null;
        }

        public override string ToString(){ return "Card Stat Sel."; }
    }
}