using UnityEngine;
using UnityEngine.UIElements;

public class AtmosphereEditorUI : MonoBehaviour{
    public GameObject atmosphere;
    public UIDocument mainUIDocument;
    VisualElement root;
    void Start() {
        Initialize();
    }
    
    void Initialize() {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        var visualTree = Resources.Load<VisualTreeAsset>("AtmosphereEditorUI");
        if (visualTree == null) {
            Debug.LogError("Failed to load UXML file.");
            return;
        }

        root.Clear();
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        uiDocument.rootVisualElement.style.display = DisplayStyle.None;

        var densityField = root.Q<FloatField>("AtmoDensityField");
        var rimIntensityField = root.Q<FloatField>("RimIntensityField");
        var colourBtn = root.Q<Button>("AtmoColourBtn");
        var backBtn = root.Q<Button>("BackBtn");

        densityField.value = atmosphere.GetComponent<Renderer>().sharedMaterial.GetFloat("_apmosphereDensity");
        rimIntensityField.value = atmosphere.GetComponent<Renderer>().sharedMaterial.GetFloat("_rimIntensity");;

        densityField.RegisterValueChangedCallback((evt) => { 
            atmosphere.GetComponent<Renderer>().sharedMaterial.SetFloat("_apmosphereDensity", evt.newValue);
        });
        rimIntensityField.RegisterValueChangedCallback((evt) => { 
            atmosphere.GetComponent<Renderer>().sharedMaterial.SetFloat("_rimIntensity", evt.newValue);
        });
        colourBtn.clicked += () => ColourBtnClicked();
        backBtn.clicked += () => SwitchUI(uiDocument, mainUIDocument);
    }

    void SwitchUI(UIDocument from, UIDocument to) {
        from.rootVisualElement.style.display = DisplayStyle.None; // Hide the current UI
        to.rootVisualElement.style.display = DisplayStyle.Flex;   // Show the target UI
    }

    void ColourBtnClicked() {
        ColorPicker.Create(atmosphere.GetComponent<Renderer>().sharedMaterial.GetColor("_color"), "Atmosphere Colour", SetColor, ColorFinished, true);
    }

    void SetColor(Color currentCol) {
        atmosphere.GetComponent<Renderer>().sharedMaterial.SetColor("_color", currentCol);
    }

    void ColorFinished(Color finishedColor) {
        Debug.Log("Atmosphere colour changed to " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }
}
