using UnityEngine;

public class CardStatSelections : MonoBehaviour {
    [SerializeField] private BattleManagerSO BattleManager;
    public CardStatEventHandlerSO CardStatSelManager;

    private void OnEnable() {
        CardStatSelManager.OnOption1Clicked.AddListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.AddListener(CardStatSelManager_OnOption2Clicked);
    }

    private void OnDisable() {
        CardStatSelManager.OnOption1Clicked.RemoveListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.RemoveListener(CardStatSelManager_OnOption2Clicked);
    }

    private void CardStatSelManager_OnOption1Clicked(Card card){
        Option1_Clicked(card);
    }

    private void CardStatSelManager_OnOption2Clicked(Card card){
        Option2_Clicked(card);
    }

    public void Option1_Clicked(Card card){
        // if(_resultCard is MonsterCard){
        if(card is MonsterCard){
            var monster = card as MonsterCard;
            if(monster.FusionedCard){
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima not selected
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                    
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    CardStatSelManager.SelectionsEnd();
                }
            }else{
                // normal card

                if(!monster.AnimaSelected){ // Anima not selected
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);

                }else if(!monster.ModeSelected){ //Anima selected and Mode not selected
                    monster.SelectMode();
                    CardStatSelManager.SelectAnother(monster);

                }else if(!monster.FaceSelected){ //Anima selected, Mode seletec and Face not selected
                    monster.SelectFace();
                    CardStatSelManager.SelectionsEnd();
                }
            }
        }
    }

    public void Option2_Clicked(Card card){
        // if(_resultCard is MonsterCard){
        if(card is MonsterCard){
            var monster = card as MonsterCard;
            if(monster.FusionedCard){
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima note selected
                    monster.Visuals.Anima.Anima2Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectDeffenseMode();
                    monster.SelectMode();
                    CardStatSelManager.SelectionsEnd();
                }
            }else{
                // normal card
                if(!monster.AnimaSelected){ // Anima note selected
                    monster.Visuals.Anima.Anima2Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectDeffenseMode();
                    monster.SelectMode();
                    CardStatSelManager.SelectionsEnd();
                }else if(!monster.FaceSelected){ //Anima selected, Mode seletec and Face not selected
                    monster.SetFaceDown();
                    monster.SelectFace();
                    CardStatSelManager.SelectionsEnd();
                }
            }
        }
    }
}
