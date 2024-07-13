using UnityEngine;

public class CardStatSelections : MonoBehaviour {
    [SerializeField] private BattleManagerSO BattleManager;
    public CardStatEventHandlerSO CardStatSelManager;

    public MonsterCard _monsterCard;
    public Card _resultCard;

    private void OnEnable() {
        BattleManager.OnStatSelectStart.AddListener(BattleManager_OnStatSelectStart);
        CardStatSelManager.OnOption1Clicked.AddListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.AddListener(CardStatSelManager_OnOption2Clicked);
    }

    private void OnDisable() {
        BattleManager.OnStatSelectStart.RemoveListener(BattleManager_OnStatSelectStart);
        CardStatSelManager.OnOption1Clicked.RemoveListener(CardStatSelManager_OnOption1Clicked);
        CardStatSelManager.OnOption2Clicked.RemoveListener(CardStatSelManager_OnOption2Clicked);
    }

    private void CardStatSelManager_OnOption1Clicked(){
        Option1_Clicked();
    }

    private void CardStatSelManager_OnOption2Clicked(){
        Option2_Clicked();
    }

    private void BattleManager_OnStatSelectStart(Card card){
        Debug.Log($"CardStatSelections - BattleManager_OnStatSelectStart(Card {card}) <color=red>5</color=red> ");
        StartSelection(card);
    }

    public void StartSelection(Card card){
        Debug.Log($"CardStatSelections - StartSelection(Card {card}) <color=red>6</color=red> ");
        _resultCard = null;
        _resultCard = card;
        if(_resultCard is MonsterCard){
            _monsterCard = _resultCard as MonsterCard;
        }
    }

    public void Option1_Clicked(){
        Debug.Log($"CardStatSelections - Option1_Clicked() <color=red>7</color=red> ");
        if(_resultCard is MonsterCard){
            var monster = _resultCard as MonsterCard;
            if(monster.FusionedCard){
                // fusioned Card
                if(!monster.AnimaSelected){ // Anima note selected
                    Debug.Log($"CardStatSelections - Option1_Clicked() !monster.AnimaSelected <color=red>9</color=red> ");
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    CardStatSelManager.SelectionsEnd();
                }
            }else{
                // normal card
                if(!monster.AnimaSelected){ // Anima note selected
                    Debug.Log($"CardStatSelections - Option1_Clicked() !monster.AnimaSelected <color=red>10</color=red> ");
                    monster.Visuals.Anima.Anima1Selected();
                    monster.SelectAnima();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.ModeSelected){ //Anima selected and Mode not seletec
                    monster.SelectMode();
                    CardStatSelManager.SelectAnother(monster);
                }else if(!monster.FaceSelected){ //Anima selected, Mode seletec and Face not selected
                    monster.SelectFace();
                    CardStatSelManager.SelectionsEnd();
                }
            }
        }
    }

    public void Option2_Clicked(){
        Debug.Log($"CardStatSelections - Option2_Clicked() <color=red>8</color=red> ");
        if(_resultCard is MonsterCard){
            var monster = _resultCard as MonsterCard;
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
