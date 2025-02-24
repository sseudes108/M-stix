using System.Collections;

namespace Mistix{
    public class AIS_03_BoardPlaceSel : AbstractState{
        public AIS_03_BoardPlaceSel(BattleSM battleSM, AISM aiSM) : base(battleSM, aiSM){}

        public override void Enter(){ AISM.StartCoroutine(BoardPlaceSelectRoutine()); }

        public override void Exit(){}

        private IEnumerator BoardPlaceSelectRoutine() {
            AISM.StartBoardPlaceSelection();
            yield return null;
        }

        public override string ToString(){ return "Board Place Sel."; }
    }
}