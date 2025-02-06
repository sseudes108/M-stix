// using UnityEngine;

// public class CardCreator {
//     private readonly MonsterCard _monsterCardPrefab;
//     private readonly ArcaneCard _arcaneCardPrefab;

//     public CardCreator(MonsterCard monsterCardPrefab, ArcaneCard arcaneCardPrefab){
//         _monsterCardPrefab = monsterCardPrefab;
//         _arcaneCardPrefab = arcaneCardPrefab;
//     }

//     public Card CreateCard(ScriptableObject cardData){
//         Card newCard;
//         if (cardData is MonsterCardSO){
//             newCard = _monsterCardPrefab;
//         }else{
//             newCard = _arcaneCardPrefab;
//         }
//         newCard.SetCardData(cardData);

//         return newCard;
//     }
// }