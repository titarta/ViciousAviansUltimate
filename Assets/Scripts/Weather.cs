using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;
using System.Globalization;

public class Weather : MonoBehaviour
{
    private string weather;
    public GameObject[] WeatherParticle;
    IEnumerator Start()
    {
        string url = "http://api.openweathermap.org/data/2.5/weather?q=Paranhos,PT&mode=xml&appid=91a7f473af329acafd2bb58bd03cfae8";

        WWW www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);

            Debug.Log(xmlDoc.SelectSingleNode("current/precipitation/@value").InnerText + "CONA");
            double rain = double.Parse(xmlDoc.SelectSingleNode("current/precipitation/@value").InnerText, CultureInfo.InvariantCulture.NumberFormat);
            int clouds = Int32.Parse(xmlDoc.SelectSingleNode("current/clouds/@value").InnerText);

            if(rain >= 0.5) {
                 WeatherParticle[0].SetActive(true);
            }

            if(clouds > 50){
                WeatherParticle[1].SetActive(true);
            }

            if(dateValue(DateTime.Now.ToShortTimeString(), xmlDoc.SelectSingleNode("current/city/sun/@set").InnerText))
            {  
                WeatherParticle[3].SetActive(true);
            } else {
                WeatherParticle[2].SetActive(true);
            }
        }
    }

    private bool dateValue(string date1, string date2) 
    {
        string[] dates1 = date1.Split(':');
        string[] dates2 = date2.Split('T')[1].Split(':');
        return (Int32.Parse(dates1[0]) * 100 + Int32.Parse(dates1[1])) > (Int32.Parse(dates2[0]) * 100 + Int32.Parse(dates2[1]));
    }
}
