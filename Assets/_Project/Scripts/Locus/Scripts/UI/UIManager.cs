using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIBattleScene), typeof(UICardStatSel), typeof(UIActionPhase))]
public class UIManager : MonoBehaviour {
    public UIDocument Document {get; private set;}
    public VisualElement Root {get; private set;}
    public UICardStatSel CardStats {get; private set;}

    public virtual void Awake() {
        Document = GetComponent<UIDocument>();
        Root = Document.rootVisualElement;

        CardStats = GetComponent<UICardStatSel>();
    }
}
