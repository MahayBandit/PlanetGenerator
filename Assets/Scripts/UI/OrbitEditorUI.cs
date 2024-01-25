using SolarSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbitEditorUI : MonoBehaviour {

    public PlanetOrbit planet;
    public UIDocument mainUIDocument;
    VisualElement root;
    Slider dayStateSlider;
    Slider yearStateSlider;
    
    void Start() {
        Initialize();
    }

    void  Initialize() {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        var visualTree = Resources.Load<VisualTreeAsset>("OrbitEditorUI");
        if (visualTree == null) {
            Debug.LogError("Failed to load UXML file.");
            return;
        }

        root.Clear();
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        uiDocument.rootVisualElement.style.display = DisplayStyle.None;

        var orbitAngleSlider = root.Q<Slider>("OrbitAngleSlider");
        var periapisField = root.Q<FloatField>("PeriapisField");
        var apoapsisField = root.Q<FloatField>("ApoapsisField");
        var planetTiltSlider = root.Q<Slider>("PlanetTiltSlider");
        var dayDurField = root.Q<FloatField>("DayDurField");
        var yearDurField = root.Q<FloatField>("YearDurField");
        dayStateSlider = root.Q<Slider>("DayStateSilder");
        yearStateSlider = root.Q<Slider>("YearStateSlider");
        var backBtn = root.Q<Button>("BackBtn");

        orbitAngleSlider.value = planet.orbitAngle;
        periapisField.value = planet.periapis;
        apoapsisField.value = planet.apoapsis;
        planetTiltSlider.value = planet.tilt;
        dayDurField.value = planet.dayDurationMinutes;
        yearDurField.value = planet.yearDurationMinutes;
        dayStateSlider.value = planet.dayT;
        yearStateSlider.value = planet.yearT;
        
        orbitAngleSlider.RegisterValueChangedCallback((evt) => { planet.orbitAngle = evt.newValue; });
        periapisField.RegisterValueChangedCallback((evt) => { planet.periapis = evt.newValue; });
        apoapsisField.RegisterValueChangedCallback((evt) => { planet.apoapsis = evt.newValue; });
        planetTiltSlider.RegisterValueChangedCallback((evt) => { planet.tilt = evt.newValue; });
        dayDurField.RegisterValueChangedCallback((evt) => { planet.dayDurationMinutes = evt.newValue; });
        yearDurField.RegisterValueChangedCallback((evt) => {  planet.yearDurationMinutes = evt.newValue; });
        dayStateSlider.RegisterValueChangedCallback((evt) => { planet.dayT = evt.newValue; });
        yearStateSlider.RegisterValueChangedCallback((evt) => { planet.yearT = evt.newValue; });
        backBtn.clicked += () => SwitchUI(uiDocument, mainUIDocument);
    }

    void Update() {
        dayStateSlider.value = planet.dayT;
        yearStateSlider.value = planet.yearT;    
    }

    void SwitchUI(UIDocument from, UIDocument to) {
        from.rootVisualElement.style.display = DisplayStyle.None; // Hide the current UI
        to.rootVisualElement.style.display = DisplayStyle.Flex;   // Show the target UI
    }
}