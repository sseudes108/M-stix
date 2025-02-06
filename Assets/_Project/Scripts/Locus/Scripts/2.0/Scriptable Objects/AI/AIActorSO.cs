// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// /// <summary>
// /// AIActorSO Is used to manage all the AI Actions during the battle. All the events of the actions should be invocked from here, since the actions are not monobehaviours
// /// </summary>

// [CreateAssetMenu(fileName = "AIActor", menuName = "Mistix/AI/Actor", order = 0)]
// public class AIActorSO : ScriptableObject {

// #region Actions
//     public AICardSelector CardSelector { get; private set; }
//     public AICardStatSelector CardStatSelector { get; private set; }
//     public AIBoardPlaceSelector BoardPlaceSelector { get; private set; }
//     public AIEffectSelector EffectSelector { get; private set; }
//     public AIAttackSelector AttackSelector { get; private set; }
// #endregion

// #region Events
//     [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
//     [HideInInspector] public UnityEvent CardStatSelector_OnCardStatSelectionFinished;
//     [HideInInspector] public UnityEvent BoardPlaceSelector_OnBoardPlaceSelected;
//     [HideInInspector] public UnityEvent EffectSelector_OnEffectSelected;
//     [HideInInspector] public UnityEvent ActionPhaseEnd;
// #endregion

// [field:SerializeField] public AIManagerSO AIManager { get; private set; }

// // #region Managers
// //     [field:SerializeField] public AIManagerSO AIManager { get; private set; }
// //     [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }
// // #endregion
    
//     public AI AI;
//     public AIFieldChecker FieldChecker { get; private set; }
//     public AIFusioner Fusioner { get; private set; }
    
//     public bool MakeABoardFusion { get; private set; }
//     // public Card CardOnBoardToFusion { get; private set; }

//     // public List<Card> CardsOnAIField { get; private set; } = new();
//     // public List<Card> CardsOnPlayerField { get; private set; } = new();

//     // public MonsterCard AttackingMonster { get; private set; }


//     // public List<MonsterCard> MonstersOnAIField { get; private set; } = new();
//     // public List<MonsterCard> MonstersOnAIFieldThatCanAttack { get; private set; } = new();
//     // public List<MonsterCard> MonsterOnPlayerField { get; private set; } = new();

//     // public CardsOnField CardsOnField;

//     // public List<Card> CardsInHand { get; private set; } = new();
    
//     private void OnEnable() {        
//         FieldChecker ??= new(this);
//         Fusioner ??= new(this, AIManager);

//         CardSelector ??= new(this);
//         CardStatSelector ??= new(this);
//         BoardPlaceSelector ??= new(this, AIManager);
//         EffectSelector ??= new(this);
//         AttackSelector ??= new(this, AIManager.AI.CardOrganizer, AIManager);

//         CardSelector_OnSelectionFinished ??= new UnityEvent();
//         CardStatSelector_OnCardStatSelectionFinished ??= new UnityEvent();
//         EffectSelector_OnEffectSelected ??= new UnityEvent();
//     }

// #region End Action Signals
//     public void CardSelectionFinished() { CardSelector_OnSelectionFinished?.Invoke(); }

//     public void CardStatSelectionFinished() { CardStatSelector_OnCardStatSelectionFinished?.Invoke(); }

//     public void BoardPlaceSelected() { BoardPlaceSelector_OnBoardPlaceSelected?.Invoke(); }

//     public void EffectSelected(){
//         /*
//             if there some monster on field that can attack
//                 enter attack select routine
//             else
//                 ActionEnd()
//         */
//         // SplitCardsOnBoardByType();

//         // if(MonstersOnAIFieldThatCanAttack.Count > 0){
//         //     Debug.Log($"AIMonstersThatCanAttack.Count {MonstersOnAIFieldThatCanAttack.Count}");
//         //     AttackingMonster = MonstersOnAIFieldThatCanAttack[0];
            
//         //     AIManager.AI.StartCoroutine(AttackSelector.SelectAttackRoutine());
//         // }else{
//         //     Debug.Log($"AIMonstersThatCanAttack.Count {MonstersOnAIFieldThatCanAttack.Count}");
//         //     Debug.LogWarning("Action End");
//         //     ActionEnd();
//         // }
//     }

//     public void ActionEnd() { ActionPhaseEnd?.Invoke(); }

// #endregion

// #region Card Lists
//     public void UpdateCardLists(List<Card> cardsInHand, List<MonsterCard> monstersOnAIField){
//         FieldChecker.OrganizeCardLists(cardsInHand, monstersOnAIField);
//     }

//     // public void SetAICardLists( List<Card> aICardsOnField, List<Card> playerCardsOnField){
//     //     CardsOnAIField = aICardsOnField;
//     //     CardsOnPlayerField = playerCardsOnField;
//     // }

//     public void SetBoardPlaces(List<BoardPlace> monsterPlaces, List<BoardPlace> arcanePlaces){
//         BoardPlaceSelector.SetBoardPlaces(monsterPlaces, arcanePlaces);
//     }
// #endregion

//     // public void SetBoardFusion(bool boardFusion) { MakeABoardFusion = boardFusion; }
//     public void SetAI(AI ai) { AI = ai; }

//     public void IsBoardFusion(bool IsBoardFusion){
//         MakeABoardFusion = IsBoardFusion;
//     }

//     // #region Board Fusion
//     //     public void SetBoardFusion(Card cardToFusion){
//     //         BoardManager.BoardFusion();
//     //         MakeABoardFusion = true;
//     //         CardOnBoardToFusion = cardToFusion;
//     //     }

//     //     public void ResetBoardFusion(){
//     //         MakeABoardFusion = false;
//     //         if(CardOnBoardToFusion != null){
//     //             CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
//     //             CardOnBoardToFusion = null;
//     //         }
//     //     }
//     // #endregion


//     // public void SetAIMAnager(AIManagerSO aimanager) { AIManager = aimanager; }

//     // public void SplitCardsOnBoardByType(){
//     //     ClearOnFieldLists();

//     //     foreach(var card in CardsOnAIField){
//     //         if(card is MonsterCard){
//     //             MonstersOnAIField.Add(card as MonsterCard);
//     //             if(card is MonsterCard){
//     //                 MonsterCard monsterCard = card as MonsterCard;
//     //                 if(monsterCard.CanAttack && monsterCard.IsInAttackMode){
//     //                     MonstersOnAIFieldThatCanAttack.Add(monsterCard);
//     //                 }
//     //             }
//     //         }else{
//     //             // _arcanesOnAIField.Add(card as ArcaneCard);
//     //         }
//     //     }

//     //     foreach(var card in CardsOnPlayerField){
//     //         if(card is MonsterCard){
//     //             MonsterOnPlayerField.Add(card as MonsterCard);
//     //         }else{
//     //             // _arcanesOnPlayerField.Add(card as ArcaneCard);
//     //         }
//     //     }

//     //     CardsOnField = null;
//     //     CardsOnField = new(
//     //         MonsterOnPlayerField, 
//     //         MonstersOnAIField,
//     //         MonstersOnAIFieldThatCanAttack
//     //     );
//     // }

//     // private void ClearOnFieldLists(){
//     //     MonstersOnAIField.Clear();
//     //     MonsterOnPlayerField.Clear();
//     //     MonstersOnAIFieldThatCanAttack.Clear();
//     // }
// }

// // public class CardsOnField{
// //     public List<MonsterCard> MonstersOnPlayerField;
// //     public List<MonsterCard> MonstersOnAIFieldThatCanAttack;

// //     public List<MonsterCard> MonstersOnAIField;

// //     public CardsOnField(
// //         List<MonsterCard> monstersOnPlayerField,
// //         List<MonsterCard> monstersOnAIField,
// //         List<MonsterCard> monstersOnAIFieldThatCanAttack
// //     ){
// //         MonstersOnPlayerField = monstersOnPlayerField;
// //         MonstersOnAIField = monstersOnAIField;
// //         MonstersOnAIFieldThatCanAttack = monstersOnAIFieldThatCanAttack;
// //     }
// // }