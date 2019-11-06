using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject gameLogicObj;
    // Start is called before the first frame update
    void Start()
    {
        gameLogicObj.GetComponent<GameLogic>().increaseNumberMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.impulse.magnitude > 5) {
            //Todo: animations and stuff, more adjustments
            GameObject.Destroy(gameObject);
            gameLogicObj.GetComponent<GameLogic>().decreaseNumberMonsters();
        }
    }
}
