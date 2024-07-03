using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour {
    public UIDocument Document {get; private set;}
    public VisualElement Root {get; private set;}

    public virtual void Awake() {
        Document = GetComponent<UIDocument>();
        Root = Document.rootVisualElement;
    }
}