using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private int touchesDone = 0;
    public int numberTries;
    private bool wasFired;
    private Rigidbody birdRB;
    private Transform birdTransform;
    public Transform birdLaunchPlaceHolder;
    public GameObject bird;
    public GameObject slingshotMarker;
    public GameObject birdMarker;
    public GameObject guideLine;
    public GameObject rightElastic;
    public GameObject leftElastic;
    public Transform rightElasticTransform;
    public Transform leftElasticTransform;
    public Text triesText;
    public Text enemiesText;
    private int numberMonsters;
    public bool onTutorial;
    private bool onWin;
    private bool onLoss;
    public string sceneName;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject tutorialScreen;

    void Start()
    {
        birdRB = bird.GetComponent<Rigidbody>();
        wasFired = false;
        birdTransform = bird.transform;
        Physics.gravity = new Vector3(0, -10f, 0);
        Physics.defaultSolverIterations = 1;
        triesText.text = "x" + numberTries.ToString();
        if(onTutorial) {
            tutorialScreen.SetActive(true);
        }
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
            if(isLeft(slingshotMarkerPos2D,birdMarkerPos2D,birdXYpos)) {
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
            if(Input.touchCount >= touchesDone + 1 || Input.GetMouseButtonDown(1)) {
                touchesDone++;
                if(onTutorial) {
                    onTutorial = false;
                    tutorialScreen.SetActive(false);
                } else {
                    shootBird();
                }
            }

        } else {
            Vector2 leftTr = leftElasticTransform.position;
            Vector2 rightTr = rightElasticTransform.position;
            Vector2 bird2Dpos = birdTransform.position;
            if(isLeft(leftTr, rightTr, bird2Dpos)) {
                rightElastic.SetActive(false);
                leftElastic.SetActive(false);
            }

            //check if bird is stopped
            if((bird.transform.position - slingshotMarker.transform.position).magnitude > 3000 || birdTransform.position.y < 1) {
                if(numberTries == 0) {
                    onLoss = true;
                    loseScreen.SetActive(true);
                } else {
                    resetTry();
                }
            }
        }

        
       

    }

    public bool isLeft(Vector2 a, Vector2 b, Vector2 c){
        return ((b.x - a.x)*(c.y - a.y) - (b.y - a.y)*(c.x - a.x)) > 0;
    }

    private void shootBird() {
        Vector3 vecForce = birdLaunchPlaceHolder.position - birdTransform.position;
        birdRB.AddForce(vecForce*5, ForceMode.VelocityChange);
        wasFired = true;
        guideLine.SetActive(false);
        numberTries--;
        triesText.text = "x" + numberTries.ToString();
    }

    private void resetTry() {
        if(numberMonsters == 0) {
            onWin = true;
            winScreen.SetActive(true);
        }
        wasFired = false;
        guideLine.SetActive(true);
        rightElastic.SetActive(true);
        leftElastic.SetActive(true);
        bird.transform.rotation = new Quaternion(-1, 0, 0, 0);
    }

    public void increaseNumberMonsters() {
        numberMonsters++;
        enemiesText.text = numberMonsters.ToString();
    }

    public void decreaseNumberMonsters() {
        numberMonsters--;
        enemiesText.text = numberMonsters.ToString();
        if(numberMonsters == 0) {
            onWin = true;
            winScreen.SetActive(true);
            
        }
    }
}
