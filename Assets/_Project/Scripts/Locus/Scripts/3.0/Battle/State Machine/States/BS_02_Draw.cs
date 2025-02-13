using System.Collections;
using UnityEngine;

namespace Mistix{
    public class BS_02_Draw : AbstractState{
        public BS_02_Draw(BattleSM battleSM) : base(battleSM) {}

        public override void Enter() { BattleSM.StartCoroutine(DrawPhaseRoutine()); }
        public override void Exit() {}

        private IEnumerator DrawPhaseRoutine() {
            BattleSM.CheckPositionsInHand(); //Checar posições livres na mão

            BattleSM.DrawCards(); //Dono do turno saca as cartas, se for turno 1 os dois sacam
            
            if(BattleSM.IsFirstTurn()){ //Todos sacam, primeiro turno

                yield return new WaitForSeconds(4f); //Tempo para sacar as 5 cartas iniciais
                BattleSM.ChangeState(BattleSM.CardSelectionPhase); // Passar para Card Selection State

            }else{ //Cada um saca em seu turno

                if(BattleSM.IsHandFull()){ //Verifica se o dono do turno já nao tem mais espaço na mão
                    BattleSM.ChangeState(BattleSM.CardSelectionPhase); // Passar para Card Selection State
                }
            }

            yield return null;
        }

        public override string ToString() { return "Draw"; }
    }
}