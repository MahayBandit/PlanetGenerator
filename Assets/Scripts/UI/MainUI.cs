using SolarSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    VisualElement root;
    public UIDocument orbitSettingsUIDocument;
    public UIDocument planetSettingsUIDocument;
    public UIDocument atmoSettingsUIDocument;
    public UIDocument colourSettingsUIDocument;

    private Button orbitSettingsButton;
    private Button planetSettingsButton;
    private Button atmoSettingsButton;
    private Button colourSettingsButton;

    void Start() {
        Initialize();   
    }

    void  Initialize() {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        var visualTree = Resources.Load<VisualTreeAsset>("MainUI");
        if (visualTree == null) {
            Debug.LogError("Failed to load UXML file.");
            return;
        }

        root.Clear();
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        orbitSettingsButton = root.Q<Button>("OrbitSettingsBtn");
        planetSettingsButton = root.Q<Button>("PlanetSettingsBtn");
        atmoSettingsButton = root.Q<Button>("AtmoSettingsBtn");
        colourSettingsButton = root.Q<Button>("ColourSettingsBtn");

        orbitSettingsButton.clicked += () => SwitchUI(uiDocument, orbitSettingsUIDocument);
        planetSettingsButton.clicked += () => SwitchUI(uiDocument, planetSettingsUIDocument);
        atmoSettingsButton.clicked += () => SwitchUI(uiDocument, atmoSettingsUIDocument);
        colourSettingsButton.clicked += () => SwitchUI(uiDocument, colourSettingsUIDocument);
    }

    void SwitchUI(UIDocument from, UIDocument to) {
        from.rootVisualElement.style.display = DisplayStyle.None; // Hide the current UI
        to.rootVisualElement.style.display = DisplayStyle.Flex;   // Show the target UI
    }
}