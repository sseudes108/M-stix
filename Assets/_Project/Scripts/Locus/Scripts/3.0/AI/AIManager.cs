using System;
using UnityEngine;
namespace Mistix{

    public class AIManager : MonoBehaviour{
        private AISM _aiSM;

        private void Awake() {
            _aiSM = GetComponent<AISM>();
        }

        public void ChangeAISMToCardSelectionPhase(){
            _aiSM.ChangeState(_aiSM.AI_CardSelection);
        }

        public void ChangeAISMToCardStatSelPhase(){
            _aiSM.ChangeState(_aiSM.AI_CardStatSelection);
        }
    }
}