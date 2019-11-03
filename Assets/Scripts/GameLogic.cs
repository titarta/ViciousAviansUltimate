using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public Transform birdLaunchPlaceHolder;
    public GameObject bird;
    public GameObject slingshotMarker;
    public GameObject birdMarker;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {  
        //Update bird Position

        // Get bird XY position
        Vector3 slingshotBackwardDirection = slingshotMarker.transform.forward * -1;
        Vector3 markerToMarkerVector = birdMarker.transform.position - slingshotMarker.transform.position;
        
        Vector2 slingshotBackwardDirection2D = new Vector2(slingshotBackwardDirection.z, -slingshotBackwardDirection.x);
        slingshotBackwardDirection2D = slingshotBackwardDirection2D.normalized;

        Vector2 markerToMarkerVector2D = new Vector2(markerToMarkerVector.x, markerToMarkerVector.z);

        float distanceBirdToSlingshot = Vector2.Dot(slingshotBackwardDirection2D, markerToMarkerVector2D);

        Vector2 slingshotMarkerPos2D = new Vector2(slingshotMarker.transform.position.x, slingshotMarker.transform.position.z);
        Vector2 birdMarkerPos2D = new Vector2(birdMarker.transform.position.x, birdMarker.transform.position.z);
        Vector2 birdXYpos = slingshotMarkerPos2D + slingshotBackwardDirection2D*distanceBirdToSlingshot;

        // Get bird Y position

        float increasedAltitude = Vector2.Distance(birdXYpos,  birdMarkerPos2D);
        Vector3 crossProductAux = Vector3.Cross(slingshotBackwardDirection, markerToMarkerVector);

        if(crossProductAux.y > 0) {
            bird.transform.position = new Vector3(birdXYpos.x,  birdLaunchPlaceHolder.transform.position.y + increasedAltitude , birdXYpos.y);
        } else {
            bird.transform.position = new Vector3(birdXYpos.x, birdLaunchPlaceHolder.transform.position.y + increasedAltitude , birdXYpos.y);
        }

        Debug.Log(bird.transform.position);

        //Check user input

    }
}
