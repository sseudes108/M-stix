// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class FusionManager : MonoBehaviour{
//     public static Action OnFusion;

//     [SerializeField] private Card _cardPrefab;
//     [SerializeField] private List<CardSO> _possibleMonsters;
//     [SerializeField] private HandController _playerHandController;

//     private bool _listEnded;
    
//     private void Update() {
//         if(Input.GetKeyDown(KeyCode.F)){
//             Fusion();
//         }
//     }

//     private void Fusion(){
//         _listEnded = false;
//         StopAllCoroutines();
//         StartCoroutine(FusionRoutine(CardSelector.Instance.SelectedCards));
//     }

//     private IEnumerator FusionRoutine(List<Card> selectedCards){
//         Debug.Log("First Fusion step");
//         DisableColliders(selectedCards);
//         yield return new WaitForSeconds(0.5f);

//         if(selectedCards.Count != 1){
            
//             if(!IsLvlEqual(selectedCards[0], selectedCards[1])){
//                 Debug.Log(string.Format("lvls are not equals! selectedCards[0]: {0}, selectedCards[1]:{1}", selectedCards[0].MonsterInfo.LVL, selectedCards[1].MonsterInfo.LVL));

//                 _playerHandController.RemoveCardFromHand(selectedCards[0]);
//                 selectedCards.Remove(selectedCards[0]);

//                 if(selectedCards.Count == 1){
//                     _listEnded = true;
//                     Debug.Log(string.Format("_listEnded: {0}", _listEnded));
//                 }
//                 if(_listEnded){
//                     Debug.Log(string.Format("_listEnded: {0}", _listEnded));
//                     FinalFusionAdjustments(null, null, selectedCards[0]);
//                 }

//                 StopAllCoroutines();
//                 yield return new WaitForSeconds(0.5f);

//                 if(!_listEnded){
//                     Debug.Log(string.Format("_listEnded: {0} - Restart coroutine", _listEnded));
//                     StartCoroutine(FusionRoutine(selectedCards));
//                 }
                
//             }
//             yield return new WaitForSeconds(0.5f);

//             //Verifica o monstro com maior ataque e assim o tipo de monstro a ser instanciado
//             CardSO.MonsterType strongestMonsterType = TypeOfStrongestMonster(selectedCards[0], selectedCards[1]);
//             yield return new WaitForSeconds(0.5f);
            
//             //Pega a lista de monstros do tipo informado
//             List<CardSO> listOfMonstersByType = GetListOfMonsters(strongestMonsterType);
//             yield return new WaitForSeconds(0.5f);
            
//             //Cria uma nova lista com os monstros do tipo informado com um lvl mais forte que o monstro 2
//             List<CardSO> possibleMonsters = MonstersWithHigherLvl(listOfMonstersByType, selectedCards[1].MonsterInfo.LVL);
//             _possibleMonsters = possibleMonsters;
//             yield return new WaitForSeconds(0.5f);
            
//             //Define a data da lista de monstros possiveis criada
//             int randomIndex = UnityEngine.Random.Range(0, possibleMonsters.Count);       
//             _cardPrefab.SetCardData(possibleMonsters[randomIndex]);

//             Debug.Log("Last Fusion step");
//             yield return new WaitForSeconds(0.5f);
            
//             //Instancia
//             Card fusionedMonster = Instantiate(_cardPrefab, selectedCards[0].transform.position, selectedCards[0].transform.rotation);

//             CheckFusionList(selectedCards, fusionedMonster);

//             //final Hand Management 
//             if(_listEnded){
//                 FinalFusionAdjustments(selectedCards[0], selectedCards[1], fusionedMonster);
//             }

//         }else{
//             FinalFusionAdjustments(null, null, selectedCards[0]);
//             StopAllCoroutines();
//         }
//     }

//     private bool IsLvlEqual(Card monster1, Card monster2){
//         if(monster1.MonsterInfo.LVL == monster2.MonsterInfo.LVL){
//             return true;
//         }else{
//             return false;
//         }
//     }

//     //Verifica o tipo do monstro mais forte
//     private CardSO.MonsterType TypeOfStrongestMonster(Card monster1, Card monster2){
//         if(monster1.MonsterInfo.ATK <= monster2.MonsterInfo.ATK){
//             return monster2.MonsterInfo.Type;
//         }else{
//             return monster1.MonsterInfo.Type;
//         }
//     }

//     //Lista de monstros do mesmo tipo do monstro mais forte
//     private List<CardSO> GetListOfMonsters(CardSO.MonsterType type){

//         switch (type){
//             case CardSO.MonsterType.Dragon:
//                 return CardsDatabase.Instance.DragonList;
//             case CardSO.MonsterType.Angel:
//                 return CardsDatabase.Instance.AngelList;
//             case CardSO.MonsterType.Machine:
//                 return CardsDatabase.Instance.MachineList;
//             case CardSO.MonsterType.Alchemist:
//                 return CardsDatabase.Instance.AlchemistList;
//             case CardSO.MonsterType.Beast:
//                 return CardsDatabase.Instance.BeastList;
//             case CardSO.MonsterType.Witch:
//                 return CardsDatabase.Instance.WitchList;
//             case CardSO.MonsterType.Demon:
//                 return CardsDatabase.Instance.DemonList;
//             case CardSO.MonsterType.Golem:
//                 return CardsDatabase.Instance.GolemList;
//             default:
//                 Debug.Log("Type error");
//                 return null;
//         }
//     }

//     //Monstros possiveis (Lista de monstros com 1 nivel superior do tipo do monstro mais forte da fusão)
//     private List<CardSO> MonstersWithHigherLvl(List<CardSO> list, int lvl){
//         List<CardSO> possibleMonsters = new List<CardSO>();
//         foreach (CardSO monster in list){
//             if(monster.MonsterInfo.LVL == lvl + 1){
//                 possibleMonsters.Add(monster);
//             }
//         }
//         return possibleMonsters;
//     }

//     private void CheckFusionList(List<Card> selectedCards, Card fusionedMonster){
//         Debug.Log("CheckFusionList");
//         if(selectedCards.Count > 2 ){

//             _playerHandController?.RemoveCardFromHand(CardSelector.Instance.SelectedCards[0]);
//             _playerHandController?.RemoveCardFromHand(CardSelector.Instance.SelectedCards[1]);

//             CardSelector.Instance.SelectedCards.RemoveRange(0,2);
//             CardSelector.Instance.SelectedCards.Insert(0,fusionedMonster);

//             StopAllCoroutines();
//             StartCoroutine(FusionRoutine(selectedCards));

//             _listEnded = false;
//         }else{
//             _listEnded = true;
//         }
//     }

//     private void FinalFusionAdjustments(Card fusionMaterial1, Card fusionMaterial2, Card fusionedMonster){
//         Debug.Log("Final Hand Adjustments");

//         if(fusionMaterial1 != null){_playerHandController?.RemoveCardFromHand(fusionMaterial1);}
//         if(fusionMaterial2 != null){_playerHandController?.RemoveCardFromHand(fusionMaterial2);}

//         Collider fusionedMonsterCollider = fusionedMonster.gameObject.GetComponent<Collider>();
//         fusionedMonsterCollider.enabled = true;
//         fusionedMonster.DeselectCardFromRejectedFusion();

//         //_playerHandController?.MoveFusionedCardToPositionInHand(fusionedMonster);

//         _playerHandController?.MoveCardToPlaceInBoard(fusionedMonster);
        
//         OnFusion?.Invoke();
//         StopAllCoroutines();
//     }

//     private void DisableColliders(List<Card> materials){
//         int index = 0;
//         foreach (Card card in materials){
//             Collider collider = materials[index].gameObject.GetComponent<Collider>();
//             collider.enabled = false;
//             index++;
//         }
//     }
// }