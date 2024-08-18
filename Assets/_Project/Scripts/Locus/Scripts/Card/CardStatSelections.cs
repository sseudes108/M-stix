using UnityEngine;

public class CardStatSelections : MonoBehaviour {
    [SerializeField] private BattleManagerSO _battleManager;
    [SerializeField] private FusionManagerSO _fusionManager;
    public CardStatEventHandlerSO CardStatSelManager;

    private void OnEnable() {
        CardStatSelManager.OnOption1Clicked.AddListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.AddListener(CardStatSelManager_OnOption2Clicked);
    }

    private void OnDisable() {
        CardStatSelManager.OnOption1Clicked.RemoveListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.RemoveListener(CardStatSelManager_OnOption2Clicked);
    }

    private void CardStatSelManager_OnOption1Clicked(){
        Option1_Clicked(_fusionManager.ResultCard);
    }

    private void CardStatSelManager_OnOption2Clicked(){
        Option2_Clicked(_fusionManager.ResultCard);
    }

    public void Option1_Clicked(Card card){
        if(card is MonsterCard){
            var monster = card as MonsterCard;
            if(monster.FusionedCard){
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima not selected
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                    return;                    
                }

                if(!monster.ModeSelected){ //Anima selected and Mode not selected
                    monster.SelectMode();
                    CardStatSelManager.SelectionsEnd();
                    return;
                }

                monster.SelectFace(); //Always face up
                
            }else{
                if(!monster.AnimaSelected){ //Anima not Selected
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                    return;
                }

                if(!monster.ModeSelected){ //Mode not selected
                    monster.SelectMode();
                    CardStatSelManager.SelectAnother(monster);
                    return;
                }

                if(!monster.FaceSelected){ //Face not selected
                    monster.SelectFace();
                    CardStatSelManager.SelectionsEnd();
                    return;
                }
            }
        }
    }

    public void Option2_Clicked(Card card){
        if(card is MonsterCard){
            var monster = card as MonsterCard;
            if(monster.FusionedCard){

                if(!monster.AnimaSelected){
                    monster.Visuals.Anima.Anima2Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                    return;
                }

                if(!monster.ModeSelected){
                    monster.SelectMode();
                    monster.SelectDeffenseMode();
                    CardStatSelManager.SelectionsEnd();
                    return;
                }

            }else{

                if(!monster.AnimaSelected){ //Anima not Selected
                    monster.Visuals.Anima.Anima2Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                    return;
                }

                if(!monster.ModeSelected){ //Mode not selected
                    monster.SelectMode();
                    monster.SelectDeffenseMode();
                    CardStatSelManager.SelectAnother(monster);
                    return;
                }

                if(!monster.FaceSelected){ //Face not selected
                    monster.SelectFace();
                    monster.SetFaceDown();
                    CardStatSelManager.SelectionsEnd();
                    return;
                }
            }
        }
    }
}
