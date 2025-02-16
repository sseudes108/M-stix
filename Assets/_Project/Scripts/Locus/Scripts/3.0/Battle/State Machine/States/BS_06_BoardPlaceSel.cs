using System.Collections;
using UnityEngine;

namespace Mistix{

    public class BS_06_BoardPlaceSel : AbstractState{
        public BS_06_BoardPlaceSel(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ BattleSM.StartCoroutine(BoardPlaceSelectionRoutine()); }

        public override void Exit(){}

        private IEnumerator BoardPlaceSelectionRoutine(){
            BattleSM.MoveToBoardPlaceSelection();//Mover carta para a posição de seleção de board place

            BattleSM.HighLightPossiblePlaces();//Highlight os places possiveis

            // if(!BattleSM.IsPlayerTurn()){
                //Change AI Stat para Board place Select
            // }

            //BattleSM.ResetBoardPlaceSelected()// Reseta o bool que verifica se o place foi selecionado
            while(BattleSM.BoardPlaceSelected() == false){//esperar a seleção
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            
            BattleSM.ChangeState(BattleSM.ActionPhase);//passar para action phase

            yield return null;
        }
        
        public override string ToString(){ return "Place Sel."; }
    }
}