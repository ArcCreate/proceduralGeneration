using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScipt : MonoBehaviour
{
    //public refrences
    public GameObject water, plainWater;

    //when the water checkbox is clicked
    public void WaterCheckbox()
    {
        Debug.Log("clicked");
        water.SetActive(!water.activeSelf);
        plainWater.SetActive(!plainWater.activeSelf);
    }
}
