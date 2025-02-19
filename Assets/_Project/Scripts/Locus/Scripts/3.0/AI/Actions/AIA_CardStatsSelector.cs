using System.Collections;
using UnityEngine;

namespace Mistix{    
    public class AIA_CardStatsSelector {
        public AIA_CardStatsSelector(AIActor actor){ _actor = actor; }
        private AIActor _actor;

        public void StartCardStatsSelectionRoutine(){
            _actor.StartCoroutine(CardStatsSelectionRoutine());
        }

        private IEnumerator CardStatsSelectionRoutine(){
            Debug.Log("AIA_CardStatsSelector - Thinking");
            yield return new WaitForSeconds(2f);
            Debug.Log("AIA_CardStatsSelector - Selected");
            yield return null;
        }
    }
}