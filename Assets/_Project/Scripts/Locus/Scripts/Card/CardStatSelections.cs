using UnityEngine;

public class CardStatSelections : MonoBehaviour {
    [SerializeField] private BattleManagerSO BattleManager;
    public CardStatEventHandlerSO CardStatSelManager;

    public MonsterCard _monsterCard;
    public Card _resultCard;

    private void OnEnable() {
        // BattleManager.OnStatSelectStart.AddListener(BattleManager_OnStatSelectStart);
        CardStatSelManager.OnOption1Clicked.AddListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.AddListener(CardStatSelManager_OnOption2Clicked);
    }

    private void OnDisable() {
        // BattleManager.OnStatSelectStart.RemoveListener(BattleManager_OnStatSelectStart);
        CardStatSelManager.OnOption1Clicked.RemoveListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.RemoveListener(CardStatSelManager_OnOption2Clicked);
    }

    private void CardStatSelManager_OnOption1Clicked(Card card){
        Option1_Clicked(card);
    }

    private void CardStatSelManager_OnOption2Clicked(Card card){
        Option2_Clicked(card);
    }

    // private void BattleManager_OnStatSelectStart(Card card){
    //     StartSelection(card);
    // }

    // public void StartSelection(Card card){
    //     Debug.Log($"T {card.name}");
    // }

    public void Option1_Clicked(Card card){
        // if(_resultCard is MonsterCard){
        if(card is MonsterCard){
            Tester.Instance.Helper.AllYellow("_resultCard is MonsterCard - 1");
            var monster = card as MonsterCard;
            if(monster.FusionedCard){
                Tester.Instance.Helper.AllYellow("monster.FusionedCard - 2");
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima not selected
                    Tester.Instance.Helper.AllYellow("!monster.AnimaSelected - 3");
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    Tester.Instance.Helper.AllYellow("!monster.ModeSelected - 4");
                    monster.SelectMode();
                    CardStatSelManager.SelectionsEnd();
                }
            }else{
                // normal card
                Tester.Instance.Helper.AllYellow("normal card - 5");
                if(!monster.AnimaSelected){ // Anima not selected
                    Tester.Instance.Helper.AllYellow("!monster.AnimaSelected - 6");
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not selected
                    Tester.Instance.Helper.AllYellow("!monster.ModeSelected - 7");
                    monster.SelectMode();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.FaceSelected){ //Anima selected, Mode seletec and Face not selected
                    Tester.Instance.Helper.AllYellow("!monster.FaceSelected - 8");
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
