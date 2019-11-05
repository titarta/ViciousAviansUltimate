using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public GameObject bird;
    public GameObject fireLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        gameObject.transform.position = (fireLocation.transform.position + bird.transform.position)/2.0F;

        // Rotation
        Vector3 dirV = Vector3.Normalize(bird.transform.position - fireLocation.transform.position);

        Debug.Log(dirV);

        gameObject.transform.rotation = new Quaternion(dirV.x, dirV.y, dirV.z, 0);

        // Scale        
        float dist = Vector3.Distance(fireLocation.transform.position, bird.transform.position);

        gameObject.transform.localScale = new Vector3(dist*100, 3, 3);
        
    }


}
