using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScipt : MonoBehaviour
{
    //public refrences
    public GameObject water, plainWater;

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


    void Start()
    {
        water.SetActive(true);
        plainWater.SetActive(false);
        seedInputField.onSubmit.AddListener(delegate { ApplyCustomSeed(); });
        seedButtonLabel.text = "Randomize";
        UpdateBiomeColorBoxes();
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
}
