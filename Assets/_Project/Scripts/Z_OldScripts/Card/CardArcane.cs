// using TMPro;
// using UnityEngine;

// public class CardArcane : Card {

//     [Header("Stats")]
//     [SerializeField] private TextMeshProUGUI _name;
//     [SerializeField] private TextMeshProUGUI _effect;

//     //
//     [SerializeField] private EArcaneType _arcaneType;

//     [Header("Equip / Field")]
//     [SerializeField] private EAnimaType _animaLink;
//     [SerializeField] private int _atkModifier, _defModifier, _lvlModifier;

//     [Header("Damage / Heal")]
//     [SerializeField] private int _amount;
//     [SerializeField] private bool _isDamageCard;

//     //Needs to be Serialize Fields (Dont Know Why)//

//     public override void SetCardData(ScriptableObject cardData){
//         _cardData = cardData as CardArcaneSO;
//         SetUpCardVariables();
//     }

//     public override void SetUpCardVariables(){
//         var arcaneData = _cardData as CardArcaneSO;

//         _arcaneType = arcaneData.ArcaneType;
//         _ilustration = arcaneData.Ilustration;
//         _name.text = arcaneData.Name;
//         _effect.text = arcaneData.Effect;

//         //Equip
//         _animaLink = arcaneData.AnimaLink;
//         _atkModifier = arcaneData.AttackModifier;
//         _defModifier = arcaneData.DefenseModifier;
//         _lvlModifier = arcaneData.LevelModifier;

//         //Heatlh Points
//         _amount = arcaneData.Amount;
//         _isDamageCard = arcaneData.DamageCard;
//     }

//     public override ECardType GetCardType(){return ECardType.Arcane;}
//     public EArcaneType GetArcaneType() {return _arcaneType;}
//     public EAnimaType GetAnimaLink() {return _animaLink;}
//     public (int, int, int) GetModifiers(){return (_atkModifier, _defModifier, _lvlModifier);}
//     public int GetHealOrDamageAmount() => _amount;
//     public bool IsDamageCard() => _isDamageCard;
// }