using System.Collections;
using UnityEngine;

namespace Mistix{    
    public class AIA_CardStatsSelector : AIA_Action{
        public AIA_CardStatsSelector(AIActor actor) : base(actor){}

        public override void StartActionRoutine(){ _actor.StartCoroutine(ActionRoutine()); }

        public override IEnumerator ActionRoutine(){
            var card = _actor.GetFusionedCard();

            if(card is MonsterCard){
                AnimaSelection(card as MonsterCard);
                yield return new WaitForSeconds(2f);

                ModeSelection(card as MonsterCard);
                yield return new WaitForSeconds(2f);

                if(!card.FusionedCard){
                    FaceSelection(card as MonsterCard);
                    yield return new WaitForSeconds(2f);
                }
            }else{
                //Arcane Options
            }
            yield return null;
            // _Actor.CardStatSelectionFinished();
            _actor.EndAICardStatsSelection();
        }

        private void AnimaSelection(MonsterCard card){
            var randomIndex = Random.Range(1, 3);
            if(randomIndex == 1){
                card.SelectAnima(1);
                // card.Visuals.Anima.Anima1Selected();
            }else{
                card.SelectAnima(2);
                // card.Visuals.Anima.Anima2Selected();
            }

            card.SelectAnima(randomIndex);
            Debug.Log("AnimaSelected");
        }

        private void ModeSelection(MonsterCard card){
            var randomIndex = Random.Range(1, 3);
            if(randomIndex == 2){
                card.SetDeffenseMode();
            }
            
            card.SetDeffenseMode();

            card.SelectMode();
            Debug.Log("ModeSelected");
        }

        private void FaceSelection(MonsterCard card){
            var randomIndex = Random.Range(1, 3);
            if(randomIndex == 2){
                card.SetFaceDown();
            }

            card.SelectFace();
            Debug.Log("FaceSelected");
        }
    }
}