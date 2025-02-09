namespace Mistix{
    public class TurnManager{
        private int CurrentTurn = 1;

        public void EndTurn(){
            CurrentTurn++;
        }

        /// <summary>
        /// Retorna o turno atual e se é o turno do player
        /// </summary>
        /// <returns></returns>
        public (int, bool) GetTurnInfo(){
            if(CurrentTurn == 1) return (CurrentTurn, true);
            return (CurrentTurn, CurrentTurn % 2 == 0);
        }
    }
}