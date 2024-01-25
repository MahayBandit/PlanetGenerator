using UnityEngine;
using UnityEngine.UIElements;

public class UISwitcher : MonoBehaviour
{
    public UIDocument mainUIDocument;
    public UIDocument orbitSettingsUIDocument;
    public UIDocument planetSettingsUIDocument;

    private Button orbitSettingsButton;
    private Button planetSettingsButton;
    private Button orbitBackButton;
    private Button planetBackButton;

    void Start() {
        var mainRoot = mainUIDocument.rootVisualElement;
        orbitSettingsButton = mainRoot.Q<Button>("OrbitSettingsBtn");
        planetSettingsButton = mainRoot.Q<Button>("PlanetSettingsBtn");

        orbitSettingsButton.clicked += () => SwitchUI(mainUIDocument, orbitSettingsUIDocument);
        planetSettingsButton.clicked += () => SwitchUI(mainUIDocument, planetSettingsUIDocument);

        //var orbitRoot = orbitSettingsUIDocument.rootVisualElement;
        //orbitBackButton = orbitRoot.Q<Button>("BackBtn");
        //orbitBackButton.clicked += () => SwitchUI(orbitSettingsUIDocument, mainUIDocument);

        //var planetRoot = planetSettingsUIDocument.rootVisualElement;
        //planetBackButton = planetRoot.Q<Button>("BackBtn");
        //planetBackButton.clicked += () => SwitchUI(planetSettingsUIDocument, mainUIDocument);
    }

    void SwitchUI(UIDocument from, UIDocument to) {
        Debug.Log("Switch UI called");
        from.rootVisualElement.style.display = DisplayStyle.None; // Hide the current UI
        to.rootVisualElement.style.display = DisplayStyle.Flex;   // Show the target UI
    }
}