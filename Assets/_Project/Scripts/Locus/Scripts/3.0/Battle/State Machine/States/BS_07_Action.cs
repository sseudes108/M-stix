using System.Collections;

namespace Mistix{
    public class BS_07_Action : AbstractState{
        public BS_07_Action(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}
        public override void Enter(){ BattleSM.StartCoroutine(ActionPhaseRoutine()); }
        public override void Exit(){
            BattleSM.HideEndActionButton();
        }
        
        private IEnumerator ActionPhaseRoutine(){
            BattleSM.ResetActionSelected();//Reseta o bool que verifica se a ação foi selecionada

            BattleSM.MoveUICardOnScreen();

            if(BattleSM.IsPlayerTurn()){
                BattleSM.ShowEndActionButton();
            }else{
                if(BattleSM.EnemyHasArcaneOnField() == false){
                    BattleSM.ChangeState(BattleSM.EndPhase); // Muda a fase direto, pois nao há ação possivel
                    yield break;
                }
            }
            
            while(BattleSM.IsActionSelected() == false){ //Aguardar seleção da ação
                yield return null;
            }

            BattleSM.ChangeState(BattleSM.EndPhase);
            yield return null;
        }

        public override string ToString(){ return "Action"; }
    }
}