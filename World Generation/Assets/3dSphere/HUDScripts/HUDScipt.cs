using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScipt : MonoBehaviour
{
    //public refrences
    public GameObject water, plainWater;

    //...
    public TMP_InputField seedInputField;
    public PlanetGeneration planetGen;
    public TMP_Text seedButtonLabel;

    void Start()
    {
        seedInputField.onSubmit.AddListener(delegate { ApplyCustomSeed(); });
        seedButtonLabel.text = "Randomize";
    }


    //when the water checkbox is clicked
    public void WaterCheckbox()
    {
        Debug.Log("clicked");
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
