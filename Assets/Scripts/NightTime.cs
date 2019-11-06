using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTime : MonoBehaviour
{
    public GameObject lightObj;
    private Light light;
    void Start()
    {
        light = lightObj.GetComponent<Light>();
    }

    void Update () 
	{
		light.intensity = 0.8f;
	}
}
