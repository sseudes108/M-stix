using System.Collections;

namespace Mistix{

    public class BS_06_BoardPlaceSel : AbstractState{
        public BS_06_BoardPlaceSel(BattleSM battleSM) : base(battleSM){}

        public override void Enter(){ BattleSM.StartCoroutine(BoardPlaceSelectionRoutine()); }

        public override void Exit(){}

        private IEnumerator BoardPlaceSelectionRoutine(){
            BattleSM.MoveToBoardPlaceSelection();//Mover carta para a posição de seleção de board place

            BattleSM.HighLightPossiblePlaces();//Highlight os places possiveis

            //esperar a seleção

            //posicionar a carta

            //passar para action phase

            yield return null;
        }
        
        public override string ToString(){ return "Board Place Sel."; }
    }
}