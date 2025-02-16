using System.Collections;

namespace Mistix{
    public class BS_07_Action : AbstractState{
        public BS_07_Action(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}
        public override void Enter(){ BattleSM.StartCoroutine(ActionPhaseRoutine()); }
        public override void Exit(){}
        
        private IEnumerator ActionPhaseRoutine(){
            BattleSM.ResetActionSelected();//Reseta o bool que verifica se a ação foi selecionada

            if(BattleSM.IsFirstTurn()){
                if(BattleSM.PlayerHasArcaneOnField() == false){
                    BattleSM.ChangeState(BattleSM.EndPhase); // Muda a fase direto, pois nao há ação possivel
                    yield break;
                }else{
                    BattleSM.ShowEndActionButton();//Mostrar botoao de encerrar fase caso haja arcanes no campo, ou nao seja o primeiro turno e for turno do player
                }
            }else{
                if(BattleSM.EnemyHasArcaneOnField() == false){
                    BattleSM.ChangeState(BattleSM.EndPhase); // Muda a fase direto, pois nao há ação possivel
                    yield break;
                }
                //Change Ai to Action Selection
            }

            // BattleSM.ResetActionSelected();//Reseta o bool que verifica se a ação foi selecionada

            while(BattleSM.IsActionSelected() == false){ //Aguardar seleção da ação
                yield return null;
            }

            // BattleSM.ChangeState(BattleSM.EffectsPhase);
            yield return null;
        }

        public override string ToString(){ return "Action"; }
    }
}