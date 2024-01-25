using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetEditorUI : MonoBehaviour {
    public PlanetGenerator planet;
    public UIDocument mainUIDocument;
    VisualElement root;

    void Start() {
        Initialize();
    }

    void Initialize() {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        var visualTree = Resources.Load<VisualTreeAsset>("PlanetEditorUI");
        if (visualTree == null) {
            Debug.LogError("Failed to load UXML file.");
            return;
        }

        root.Clear();
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        uiDocument.rootVisualElement.style.display = DisplayStyle.None;

        var noiseStrField = root.Q<FloatField>("NoiseStrField");
        var numOfLayersSlider = root.Q<SliderInt>("NumOfLayersSlider");
        var baseRoughnessField = root.Q<FloatField>("BaseRoughField");
        var roughnessField = root.Q<FloatField>("RoughField");
        var persistenceField = root.Q<FloatField>("PersistenceField");
        var noiseOffset = root.Q<Vector3Field>("NoiseOffset");
        var oceanLevelField = root.Q<FloatField>("OceanLevelField");
        var backBtn = root.Q<Button>("BackBtn");
        
        //NoiseSettings.SimpleNoiseSettings noiseSettings = planet.shapeGenerator.settings.noiseLayers[0].noiseSettings.simpleNoiseSettings;
        ShapeSettings settings = planet.shapeSettings;

        noiseStrField.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength;
        numOfLayersSlider.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.numLayers;
        baseRoughnessField.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.baseRoughness;
        roughnessField.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.roughness;
        persistenceField.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.persistence;
        noiseOffset.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre;
        oceanLevelField.value = settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.minValue;

        noiseStrField.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength = evt.newValue;
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        numOfLayersSlider.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.numLayers = evt.newValue; 
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        baseRoughnessField.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.baseRoughness = evt.newValue; 
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        roughnessField.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.roughness = evt.newValue; 
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        persistenceField.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.persistence = evt.newValue; 
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        noiseOffset.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre = evt.newValue; 
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        oceanLevelField.RegisterValueChangedCallback((evt) => { 
            settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.minValue = evt.newValue; 
            planet.shapeGenerator.UpdateSettings(settings);
            planet.GeneratePlanet();
            });
        backBtn.clicked += () => SwitchUI(uiDocument, mainUIDocument);
    }

    void Update() {
        
    }

    void SwitchUI(UIDocument from, UIDocument to) {
        from.rootVisualElement.style.display = DisplayStyle.None; // Hide the current UI
        to.rootVisualElement.style.display = DisplayStyle.Flex;   // Show the target UI
    }
}
