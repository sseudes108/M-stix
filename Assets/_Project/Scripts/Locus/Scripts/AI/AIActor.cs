using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AIActor : MonoBehaviour {

    [field:SerializeField] public BoardManagerSO BoardManager { get; private set; }
    [field:SerializeField] public AIManagerSO AIManager { get; private set; }

#region Events
    [HideInInspector] public UnityEvent CardSelector_OnSelectionFinished;
    [HideInInspector] public UnityEvent CardStatSelector_OnCardStatSelectionFinished;
    [HideInInspector] public UnityEvent BoardPlaceSelector_OnBoardPlaceSelected;
    [HideInInspector] public UnityEvent EffectSelector_OnEffectSelected;
    [HideInInspector] public UnityEvent ActionPhaseEnd;
#endregion

    public AICardOrganizer CardOrganizer { get; private set; }
    public AIFieldChecker FieldChecker { get; private set; }
    public AIFusioner Fusioner { get; private set; }


    private Card _fusionedCard;
    public Card CardOnBoardToFusion { get; private set; }
    private AI _ai;


    public AICardSelector CardSelector { get; private set; }
    public AICardStatSelector CardStatSelector { get; private set; }
    public AIBoardPlaceSelector BoardPlaceSelector { get; private set; }
    public AIEffectSelector EffectSelector { get; private set; }
    // public AIAttackSelector AttackSelector { get; private set; }

    private void Awake() {
        _ai = GetComponent<AI>();
        CardOrganizer = GetComponent<AICardOrganizer>();
        FieldChecker = GetComponent<AIFieldChecker>();

        AIManager.SetAI(_ai);
        AIManager.SetActor(this);
        

        // FieldChecker ??= new();
        Fusioner ??= new(this);
        CardSelector ??= new(_ai, this, FieldChecker);
        CardStatSelector ??= new(this);
        EffectSelector ??= new(this);
    }

    public bool MakeABoardFusion { get; private set; }
    public void IsBoardFusion(bool IsBoardFusion) { MakeABoardFusion = IsBoardFusion; }


#region Board Fusion
        public void SetBoardFusion(Card cardToFusion){
            BoardManager.BoardFusion();
            MakeABoardFusion = true;
            CardOnBoardToFusion = cardToFusion;
        }

        public void ResetBoardFusion(){
            MakeABoardFusion = false;
            if(CardOnBoardToFusion != null){
                CardOnBoardToFusion.GetBoardPlace().SetPlaceFree();
                CardOnBoardToFusion = null;
            }
        }
#endregion

    public void SetFusionedCard(Card fusionedCard){
        _fusionedCard = null;
        _fusionedCard = fusionedCard;
    }

    public Card GetFusionedCard() { return _fusionedCard; }

#region End Action Signals
    public void CardSelectionFinished() { CardSelector_OnSelectionFinished?.Invoke(); }

    public void CardStatSelectionFinished() { CardStatSelector_OnCardStatSelectionFinished?.Invoke(); }

    public void BoardPlaceSelected() { BoardPlaceSelector_OnBoardPlaceSelected?.Invoke(); }

    public void EffectSelected(){
        /*
            if there some monster on field that can attack
                enter attack select routine
            else
                ActionEnd()
        */
    }

    public void ActionEnd() { ActionPhaseEnd?.Invoke(); }

#endregion
}