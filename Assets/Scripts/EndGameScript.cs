using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.touchCount >= 1) {
             SceneManager.UnloadSceneAsync(sceneName);
             SceneManager.LoadScene("MainMenu");
         }
    }
}
