using System.Collections;
using UnityEngine;

public class AICardStatSelector : AIAction {
    public AICardStatSelector(AIActorSO actor){
        _actor = actor;
    }

    public IEnumerator SelectCardStats(Card card){
        if(card is MonsterCard){
            AnimaSelection(card as MonsterCard);
            yield return new WaitForSeconds(2f);

            ModeSelection(card as MonsterCard);
            yield return new WaitForSeconds(2f);

            if(!card.FusionedCard){
                FaceSelection(card as MonsterCard);
                yield return new WaitForSeconds(2f);
            }
        }
        yield return null;
        _actor.CardStatSelectionFinished();
    }

    private void AnimaSelection(MonsterCard card){
        var randomIndex = Random.Range(1, 3);
        if(randomIndex == 1){
            card.Visuals.Anima.Anima1Selected();
        }else{
            card.Visuals.Anima.Anima2Selected();
        }

        card.SelectAnima();
        // Debug.Log("AnimaSelected");
    }

    private void ModeSelection(MonsterCard card){
        var randomIndex = Random.Range(1, 3);
        if(randomIndex == 2){
            card.SelectDeffenseMode();
        }

        card.SelectMode();
        // Debug.Log("ModeSelected");
    }

    private void FaceSelection(MonsterCard card){
        var randomIndex = Random.Range(1, 3);
        if(randomIndex == 2){
            card.SetFaceDown();
        }

        card.SelectFace();
        // Debug.Log("FaceSelected");
    }
}