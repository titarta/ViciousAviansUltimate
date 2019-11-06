using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudy : MonoBehaviour
{
    public GameObject lightObj;
    private Light light;
    void Start()
    {
        light = lightObj.GetComponent<Light>();
    }

    void Update () 
	{
		if(light.intensity > 4)
        {
            light.intensity = light.intensity - 4;
        } else {
            light.intensity = 1;
        }
	}
}
