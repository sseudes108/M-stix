using System.Collections.Generic;
using UnityEngine;
using Mistix.Enums;

namespace Mistix{
    public class Fusion:MonoBehaviour{
        public static Fusion Instance;

        private void Awake() {
            if(Instance != null){
                Errors.InstanceError(this);
            }
            Instance = this;
        }

        public void StartFusion(List<Card> selectedCards){
            var card1 = selectedCards[0];
            var card2 = selectedCards[1];

            //Verificar se ambos são mostros//

            var monster1 = card1.GetComponent<MonsterCard>();
            var monster2 = card2.GetComponent<MonsterCard>();

            var monster1Atk = monster1.GetMonsterAtk();
            var monster2Atk = monster2.GetMonsterAtk();

            var monster1Level = monster1.GetMonsterLevel();
            var monster2Level = monster2.GetMonsterLevel();
        }
    }
}