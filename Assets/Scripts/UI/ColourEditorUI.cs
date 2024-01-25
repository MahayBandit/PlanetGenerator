using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ColourEditorUI : MonoBehaviour {
    public PlanetGenerator planet;
    public UIDocument mainUIDocument;
    UIDocument uiDocument;
    ColourSettings settings;
    VisualElement root;
    int biomeIndex;
    void Start() {
        Initialize();
    }
    
    void Initialize() {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        var visualTree = Resources.Load<VisualTreeAsset>("ColourEditorUI");
        if (visualTree == null) {
            Debug.LogError("Failed to load UXML file.");
            return;
        }

        root.Clear();
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        uiDocument.rootVisualElement.style.display = DisplayStyle.None;

        settings = planet.colourSettings;

        var topBiomeSlider = root.Q<Slider>("B3Slider");
        var topBiomeBtn = root.Q<Button>("B3Colour");
        var midBiomeSlider = root.Q<Slider>("B2Slider");
        var midBiomeBtn = root.Q<Button>("B2Colour");
        var botBiomeSlider = root.Q<Slider>("B1Slider");
        var botBiomeBtn = root.Q<Button>("B1Colour");
        var biomeBlendSlider = root.Q<Slider>("BiomeBlendSlider");
        var oceanColourBtn = root.Q<Button>("OceanColourBtn");
        var backBtn = root.Q<Button>("BackBtn");

        topBiomeSlider.RegisterValueChangedCallback((evt) => { 
            settings.biomeColourSettings.biomes[2].startHeight = evt.newValue;
            planet.colourGenerator.UpdateSettings(settings);
            planet.GenerateColours();
            });
        midBiomeSlider.RegisterValueChangedCallback((evt) => { 
            settings.biomeColourSettings.biomes[1].startHeight = evt.newValue;
            planet.colourGenerator.UpdateSettings(settings);
            planet.GenerateColours();
            });
        botBiomeSlider.RegisterValueChangedCallback((evt) => { 
            settings.biomeColourSettings.biomes[0].startHeight = evt.newValue;
            planet.colourGenerator.UpdateSettings(settings);
            planet.GenerateColours();
            });
        biomeBlendSlider.RegisterValueChangedCallback((evt) => { 
            settings.biomeColourSettings.blendAmount = evt.newValue;
            planet.colourGenerator.UpdateSettings(settings);
            planet.GenerateColours();
            });
        topBiomeBtn.clicked += () => GradientBtnClicked(2);
        midBiomeBtn.clicked += () => GradientBtnClicked(1);
        botBiomeBtn.clicked += () => GradientBtnClicked(0);
        oceanColourBtn.clicked += () => OceanColourBtnClicked();
        backBtn.clicked += () => SwitchUI(uiDocument, mainUIDocument);
    }

    void SwitchUI(UIDocument from, UIDocument to) {
        from.rootVisualElement.style.display = DisplayStyle.None; // Hide the current UI
        to.rootVisualElement.style.display = DisplayStyle.Flex;   // Show the target UI
    }

    void GradientBtnClicked(int setBiomeIndex) {
        biomeIndex = setBiomeIndex;
        GradientPicker.Create(settings.biomeColourSettings.biomes[biomeIndex].gradient, "Biome Colour", SetGradient, GradientFinished);
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    void OceanColourBtnClicked() {
        GradientPicker.Create(settings.oceanColour, "Ocean Colour", SetOcean, GradientFinished);
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    void SetGradient(Gradient currentGradient) {
        settings.biomeColourSettings.biomes[biomeIndex].gradient = currentGradient;
        planet.colourGenerator.UpdateSettings(settings);
        planet.GenerateColours();
    }

    void SetOcean(Gradient currentGradient) {
        settings.oceanColour = currentGradient;
        planet.colourGenerator.UpdateSettings(settings);
        planet.GenerateColours();
    }
    
    private void GradientFinished(Gradient finishedGradient) {
        Debug.Log("Biome Gradient changed to: " + finishedGradient.colorKeys.Length + " Color keys");
        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
