using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private bool wasFired;
    private Rigidbody birdRB;
    public Transform birdLaunchPlaceHolder;
    public GameObject bird;
    public GameObject slingshotMarker;
    public GameObject birdMarker;
    // Start is called before the first frame update
    void Start()
    {
        birdRB = bird.GetComponent<Rigidbody>();
        wasFired = false;
    }


    // Update is called once per frame
    void Update()
    {  
        //Update bird Position

        // Get bird XY position
        if(!wasFired) {
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
            float increasedAltitude = 0;
            if(isLeft(slingshotMarkerPos2D,birdMarkerPos2D,birdXYpos )) {
                increasedAltitude = Vector2.Distance(birdXYpos,  birdMarkerPos2D);
            } else {
                increasedAltitude = - Vector2.Distance(birdXYpos,  birdMarkerPos2D);
            }
            
            Vector3 crossProductAux = Vector3.Cross(slingshotBackwardDirection, markerToMarkerVector);

            if(crossProductAux.y > 0) {
                bird.transform.position = new Vector3(birdXYpos.x,  birdLaunchPlaceHolder.transform.position.y + increasedAltitude , birdXYpos.y);
            } else {
                bird.transform.position = new Vector3(birdXYpos.x, birdLaunchPlaceHolder.transform.position.y + increasedAltitude , birdXYpos.y);
            }

            //Check user input
            if(Input.touchCount >= 1) {
                shootBird();
            }

        }

        
       

    }

    public bool isLeft(Vector2 a, Vector2 b, Vector2 c){
        return ((b.x - a.x)*(c.y - a.y) - (b.y - a.y)*(c.x - a.x)) > 0;
    }

    private void shootBird() {
        birdRB.AddForce(10, 0, 0, ForceMode.VelocityChange);
        wasFired = true;
    }
}
