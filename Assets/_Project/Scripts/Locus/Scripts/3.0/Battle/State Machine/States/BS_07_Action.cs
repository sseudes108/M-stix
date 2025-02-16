using System.Collections;

namespace Mistix{
    public class BS_07_Action : AbstractState{
        public BS_07_Action(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(ActionPhaseRoutine()); }

        public override void Exit(){}
        private IEnumerator ActionPhaseRoutine(){
            if(BattleSM.IsFirstTurn()){
                //Caso não haja nenhuma arcane no campo{
                BattleSM.ChangeState(BattleSM.EndPhase);
                yield break;
                //}
            }

            BattleSM.ShowEndActionButton(); //Mostrar botoao de encerrar fase
            while(BattleSM.IsActionSelected() == false){ //Aguardar seleção da ação
                yield return null;
            }

            // BattleSM.ChangeState(BattleSM.EffectsPhase);
            yield return null;
        }

        public override string ToString(){ return "Action"; }
    }
}