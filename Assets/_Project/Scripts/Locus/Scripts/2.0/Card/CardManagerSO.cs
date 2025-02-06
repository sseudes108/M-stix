// using UnityEngine;
// using UnityEngine.Events;

// [CreateAssetMenu(fileName = "CardManagerSO", menuName = "Mistix/Manager/Card", order = 0)]
// public class CardManagerSO : ScriptableObject {
//     [SerializeField] private MonsterCard _monsterCardPrefab;
//     [SerializeField] private ArcaneCard _arcaneCardPrefab;
//     [SerializeField] private BattleManagerSO _battleManager;

//     public CardCreator Creator { get; private set; }
//     public CardSelector Selector { get; private set; }

//     [HideInInspector] public UnityEvent<Card> OnCardSelected, OnCardDeselected;
//     [HideInInspector] public UnityEvent OnSomeCardSelected, OnNoneCardSelected;

//     private void OnEnable() {
//         Creator ??= new(_monsterCardPrefab, _arcaneCardPrefab);

//         Selector ??= new(this, _battleManager);
//         Selector.RegisterEvents();

//         OnCardSelected ??= new UnityEvent<Card>();
//         OnCardDeselected ??= new UnityEvent<Card>();

//         OnSomeCardSelected ??= new UnityEvent();
//         OnNoneCardSelected ??= new UnityEvent();
//     }

//     private void OnDisable() {
//         Selector.UnregisterEvents();
//     }
    
//     public void CardSelected(Card card) { OnCardSelected?.Invoke(card); }
//     public void CardDeselected(Card card) { OnCardDeselected?.Invoke(card); }
//     public void SomeCardSelected() { OnSomeCardSelected?.Invoke();}
//     public void NoneCardSelected() { OnNoneCardSelected?.Invoke();}
// }