using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FusionManager : MonoBehaviour{
    public static Action OnFusion;

    [SerializeField] private Card _cardPrefab;
    [SerializeField] private List<CardSO> _possibleMonsters;

    [SerializeField] private HandController _playerHandController;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            StartCoroutine(FusionRoutine(CardSelector.Instance.SelectedCards));
        }
    }

    private IEnumerator FusionRoutine(List<Card> selectedCards){
        
        if(!isLvlEqual(selectedCards[0], selectedCards[1])){
            Debug.Log(string.Format("lvls are not equals! selectedCards[0]: {0}, selectedCards[1]:{1}", selectedCards[0].MonsterInfo.LVL, selectedCards[1].MonsterInfo.LVL));
            // selectedCards.Remove(selectedCards[0]);
            // _playerHandController.RemoveCardFromHand(selectedCards[0]);
            StopAllCoroutines();
            StartCoroutine(FusionRoutine(selectedCards));
        }

        yield return new WaitForSeconds(1);

        //Verifica o monstro com maior ataque e assim o tipo de monstro a ser instanciado
        CardSO.MonsterType strongestMonsterType = TypeOfStrongestMonster(selectedCards[0], selectedCards[1]);
        //Debug.Log(string.Format("Type of the strongest: {0}", strongestMonsterType));
        yield return new WaitForSeconds(1);
        
        //Pega a lista de monstros do tipo informado
        List<CardSO> listOfMonstersByType = GetListOfMonsters(strongestMonsterType);
        
        //Cria uma nova lista com os monstros do tipo informado com um lvl mais forte que o monstro 2
        List<CardSO> possibleMonsters = MonstersWithHigherLvl(listOfMonstersByType, selectedCards[1].MonsterInfo.LVL);
        _possibleMonsters = possibleMonsters;
        
        //Cria o Controller
        int randomIndex = RandomValue(possibleMonsters);
        //Debug.Log(string.Format("Picked monster: {0}, {1}", possibleMonsters[randomIndex].MonsterInfo.Name, possibleMonsters[randomIndex].MonsterInfo.LVL));

        //Define a data da lista de monstros possiveis criada
        _cardPrefab.SetCardData(possibleMonsters[randomIndex]);

        DisableColliders(selectedCards); 

        Debug.Log("Last Fusion step");
        yield return new WaitForSeconds(2f);
        
        //Instancia
        Card fusionedMonster = Instantiate(_cardPrefab, selectedCards[0].transform.position, selectedCards[0].transform.rotation);

        // CardSelector.Instance.SelectedCards.Remove(selectedCards[0]);
        // CardSelector.Instance.SelectedCards.Remove(selectedCards[1]);
                
        //Check if the list of selected cars is empty
        yield return new WaitForSeconds(0.2f);
        CheckFusionList(selectedCards, fusionedMonster);

        //final Hand Management 
        FinalHandAdjustments(selectedCards[0], selectedCards[1], fusionedMonster);

    }

    private int RandomValue(List<CardSO> list){
        return UnityEngine.Random.Range(0, list.Count);
    }

    private bool isLvlEqual(Card monster1, Card monster2){
        if(monster1.MonsterInfo.LVL == monster2.MonsterInfo.LVL){
            return true;
        }else{
            return false;
        }
    }

    //Verifica o tipo do monstro mais forte
    private CardSO.MonsterType TypeOfStrongestMonster(Card monster1, Card monster2){
        if(monster1.MonsterInfo.ATK <= monster2.MonsterInfo.ATK){
            return monster2.MonsterInfo.Type;
        }else{
            return monster1.MonsterInfo.Type;
        }
    }

    //Lista de monstros do mesmo tipo do monstro mais forte
    private List<CardSO> GetListOfMonsters(CardSO.MonsterType type){
        if(type == CardSO.MonsterType.Dragon){
            return CardsDatabase.Instance.DragonList;
        }else{
            return CardsDatabase.Instance.AngelList;
        }
    }

    //Monstros possiveis (Lista de monstros com 1 nivel superior do tipo do monstro mais forte da fusão)
    private List<CardSO> MonstersWithHigherLvl(List<CardSO> list, int lvl){
        List<CardSO> possibleMonsters = new List<CardSO>();
        foreach (CardSO monster in list){
            if(monster.MonsterInfo.LVL == lvl + 1){
                possibleMonsters.Add(monster);
            }
        }
        return possibleMonsters;
    }

    private void CheckFusionList(List<Card> selectedCards, Card fusionedCard){
        //Debug.Log("Check Fusion List");

        if(selectedCards.Count > 1){return;};

        Debug.Log(string.Format("selected cards count: {0}. Roll again", selectedCards.Count));
        CardSelector.Instance.AddSelectedCard(fusionedCard, fusionedCard.MonsterInfo.Name);
        StopAllCoroutines();
        StartCoroutine(FusionRoutine(selectedCards));
    }

    private void FinalHandAdjustments(Card fusionMaterial1, Card fusionMaterial2, Card fusionedMonster){
        //Debug.Log("Final Hand Adjustments");
        _playerHandController.RemoveCardFromHand(fusionMaterial1);
        _playerHandController.RemoveCardFromHand(fusionMaterial2);

        _playerHandController.MoveFusionedCardToPositionInHand(fusionedMonster);

        OnFusion?.Invoke();
    }

    private void DisableColliders(List<Card> materials){
        int index = 0;
        foreach (Card card in materials){
            Collider collider = materials[index].gameObject.GetComponent<Collider>();
            collider.enabled = false;
            index++;
            //Debug.Log("Disable collider");
        }
    }
}