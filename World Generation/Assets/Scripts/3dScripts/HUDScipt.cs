using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScipt : MonoBehaviour
{
    //public refrences
    public GameObject water, plainWater;
    public Toggle waterCheckbox;
    //
    public GameObject clouds;
    public Toggle cloudCheckbox;

    //seed input and button
    public TMP_InputField seedInputField;
    public PlanetGeneration planetGen;
    public TMP_Text seedButtonLabel;

    //resolution
    public Slider resolutionSlider;

    //biome colors
    public Image box1;
    public Image box2;
    public Image box3;
    public Image box4;
    public Image box5;
    public Image box6;
    public Image box7;

    //axis rotation slider
    public Slider rotationSlider;

    //id name
    public TMP_Text planetIdText;
    public TMP_Text systemNameText;

    //dropdown
    public TMP_Dropdown planetTypeDropdown;

    //planettype settings
    public ShapeSettings earthLikeShape;
    public NewColorSetting earthLikeColor;
    public ShapeSettings volcanicHellShape;
    public NewColorSetting volcanicHellColor;
    public ShapeSettings marsLikeShape;
    public NewColorSetting marsLikeColor;
    public ShapeSettings waterWorldShape;
    public NewColorSetting waterWorldColor;


    //atmoshpere
    public Material atmosphereMaterial;
    //clouds
    public Material cloudsMaterial;


    void Start()
    {
        planetTypeDropdown.onValueChanged.AddListener(OnPlanetTypeChanged);
        water.SetActive(true);
        plainWater.SetActive(false);
        seedInputField.onSubmit.AddListener(delegate { ApplyCustomSeed(); });
        seedButtonLabel.text = "Randomize";
        UpdateBiomeColorBoxes();
    }

    //planet type drop down
    void OnPlanetTypeChanged(int index)
    {
        string selected = planetTypeDropdown.options[index].text;

        switch (selected)
        {
            case "Earth-Like":
                ApplyEarthLikeSettings();
                break;;
            case "Volcanic Hell":
                ApplyVolcanicSettings();
                break;
            case "Mars-Like":
                ApplyMarsLikeSettings();
                break;
            case "Ocean World":
                ApplyWaterWorldSettings();
                break;
        }

        planetGen.generatePlanet();
        UpdateBiomeColorBoxes();
    }

    void ApplyEarthLikeSettings()
    {
        planetGen.shapeSettings = earthLikeShape;
        planetGen.colorSettings = earthLikeColor;
        //other
        waterCheckbox.interactable = true;
        waterCheckbox.isOn = true;
        plainWater.SetActive(false );
        cloudCheckbox.interactable = true;
        atmosphereMaterial.SetColor("_AtmoshpeherColor", new Color(0, 0.573717177f, 0.749019682f, 255));
        cloudsMaterial.SetColor("_CloudColor", new Color(1, 1, 1));
        //resolution
        resolutionSlider.minValue = 2;
        resolutionSlider.maxValue = 256;
        if (planetGen.resolution > 256)
            planetGen.resolution = 256;
    }
    void ApplyVolcanicSettings()
    {
        planetGen.shapeSettings = volcanicHellShape;
        planetGen.colorSettings = volcanicHellColor;
        //other
        waterCheckbox.isOn = false;
        waterCheckbox.interactable = false;
        water.SetActive(false);
        plainWater.SetActive(false);
        atmosphereMaterial.SetColor("_AtmoshpeherColor", new Color(0.320754707f, 0, 0.01145487f, 255));
        cloudsMaterial.SetColor("_CloudColor", new Color(0.2515723f, 0.2515723f, 0.2515723f));
        //resolution
        resolutionSlider.minValue = 2;
        resolutionSlider.maxValue = 175;
        if (planetGen.resolution > 175)
            planetGen.resolution = 175;
    }

    void ApplyMarsLikeSettings()
    {
        planetGen.shapeSettings = marsLikeShape;
        planetGen.colorSettings = marsLikeColor;
        // other
        waterCheckbox.isOn = false;
        waterCheckbox.interactable = false;
        water.SetActive(false);
        plainWater.SetActive(false);
        cloudCheckbox.isOn = false;
        cloudCheckbox.interactable = false;
        atmosphereMaterial.SetColor("_AtmoshpeherColor", new Color(0.4f, 0.15f, 0.05f, 255));
        // resolution
        resolutionSlider.minValue = 2;
        resolutionSlider.maxValue = 200;
        if (planetGen.resolution > 200)
            planetGen.resolution = 200;
    }

    void ApplyWaterWorldSettings()
    {
        planetGen.shapeSettings = waterWorldShape;
        planetGen.colorSettings = waterWorldColor;
        //other
        waterCheckbox.interactable = true;
        waterCheckbox.isOn = true;
        plainWater.SetActive(false);
        cloudCheckbox.interactable = true;
        atmosphereMaterial.SetColor("_AtmoshpeherColor", new Color(0, 0.573717177f, 0.749019682f, 255));
        cloudsMaterial.SetColor("_CloudColor", new Color(1, 1, 1));
        //resolution
        resolutionSlider.minValue = 2;
        resolutionSlider.maxValue = 256;
        if (planetGen.resolution > 256)
            planetGen.resolution = 256;

    }



    // Rotation slider
    public void OnRotationChange()
    {
        planetGen.rotationSpeed = rotationSlider.value;
    }


    //resolution slider
    public void resolutionChange()
    {
        planetGen.resolution = (int)resolutionSlider.value;
        planetGen.generatePlanet();
    }


    //when the water checkbox is clicked
    public void WaterCheckbox()
    {
        water.SetActive(!water.activeSelf);
        plainWater.SetActive(!plainWater.activeSelf);
    }

    // Randomize seed when button is clicked
    public void OnRandomizeButton()
    {
        string newSeed = Random.Range(100000, 999999).ToString();
        seedInputField.text = newSeed;
        ApplySeed(newSeed);
    }

    //biome color list of boxes
    void UpdateBiomeColorBoxes()
    {
        var c = planetGen.colorSettings;
        if (c == null) return;
        box1.color = c.shoreFlat;
        box2.color = c.grassFlat;
        box3.color = c.forestFlat;
        box4.color = c.snowFlatColor;
        box5.color = c.snow;
        box6.color = c.cliffColor;
        box7.color = c.spireColor;
    }

    // Called when pressing Enter in the input field
    public void ApplyCustomSeed()
    {
        if (!string.IsNullOrWhiteSpace(seedInputField.text))
        {
            ApplySeed(seedInputField.text);
        }
    }

    void ApplySeed(string seedString)
    {
        int numericSeed = HashSeed(seedString);
        UpdatePlanetIdAndSystem(numericSeed);

        if (planetGen.shapeSettings.noiseLayers != null && planetGen.shapeSettings.noiseLayers.Length > 0)
        {
            var layer = planetGen.shapeSettings.noiseLayers[0];
            var noise = layer.noiseSettings;

            Vector3 seedCentre = new Vector3(
                (numericSeed % 1000) / 10f,
                (numericSeed % 2000) / 10f,
                (numericSeed % 3000) / 10f
            );

            noise.simpleNoiseSettings.centre = seedCentre;

            if (noise.rigidNoiseSettings != null)
            {
                noise.rigidNoiseSettings.centre = seedCentre;
            }

            // Second layer (if present): slight offset variation on X and Z
            if (planetGen.shapeSettings.noiseLayers.Length > 1)
            {
                var layer2 = planetGen.shapeSettings.noiseLayers[1];
                var noise2 = layer2.noiseSettings;

                // Offset range -2 to +2
                float offsetX = ((numericSeed % 5) - 2); // -2 to +2
                float offsetZ = (((numericSeed / 7) % 5) - 2); // -2 to +2

                Vector3 baseCentre = noise2.simpleNoiseSettings.centre;
                noise2.simpleNoiseSettings.centre = new Vector3(
                    baseCentre.x + offsetX,
                    baseCentre.y,
                    baseCentre.z + offsetZ
                );

                if (noise2.rigidNoiseSettings != null)
                {
                    Vector3 rigidCentre = noise2.rigidNoiseSettings.centre;
                    noise2.rigidNoiseSettings.centre = new Vector3(
                        rigidCentre.x + offsetX,
                        rigidCentre.y,
                        rigidCentre.z + offsetZ
                    );
                }
            }

            planetGen.generatePlanet();
        }

        int HashSeed(string input)
        {
            const int fnvPrime = 16777619;
            const int offsetBasis = unchecked((int)2166136261);

            int hash = offsetBasis;
            foreach (char c in input)
            {
                hash ^= c;
                hash *= fnvPrime;
            }
            return Mathf.Abs(hash);
        }
    }

    void UpdatePlanetIdAndSystem(int numericSeed)
    {
        // More word-based, natural prefixes
        string[] idPrefixes = { "PLANET", "WORLD", "SATELLITE", "MOON", "COLONY", "HABITAT", "OUTPOST", "STATION", "BASE", "HAVEN" };
        string idPrefix = idPrefixes[numericSeed % idPrefixes.Length];

        // Number part
        int planetNumber = numericSeed % 9999;

        // Build simple planet ID: PREFIX-0000
        string planetId = $"{idPrefix}-{planetNumber:D4}";

        // System name parts
        string[] prefixes = { "ALPHA", "BETA", "GAMMA", "DELTA", "EPSILON", "ZETA", "OMEGA", "SIGMA", "TAU", "KAPPA", "LYRA", "VEGA", "NOVA", "ORION", "CYGNUS" };
        string[] suffixes = { "PRIME", "NEXUS", "SECTOR", "OUTPOST", "STATION", "REACH", "HAVEN", "HUB", "BASIN" };
        string[] romanNumerals = { "I", "II", "III", "IV", "V", "VI", "VII" };

        int prefixIndex = (numericSeed / 100) % prefixes.Length;
        int suffixIndex = (numericSeed / 1000) % suffixes.Length;
        string roman = romanNumerals[(numericSeed / 7000) % romanNumerals.Length];

        // Sometimes add double-prefix for extra flair
        bool useDoublePrefix = (numericSeed / 2222) % 3 == 0;
        string extraPrefix = prefixes[(numericSeed / 3333) % prefixes.Length];

        string systemName = useDoublePrefix
            ? $"{extraPrefix} {prefixes[prefixIndex]} {suffixes[suffixIndex]} {roman}"
            : $"{prefixes[prefixIndex]} {suffixes[suffixIndex]} {roman}";

        // Set UI text
        if (planetIdText != null)
            planetIdText.text = $"ID: {planetId}";
        if (systemNameText != null)
            systemNameText.text = $"SYSTEM: {systemName}";
    }



}
