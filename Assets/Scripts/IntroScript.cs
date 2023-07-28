using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    int sceneActuelleIndex;

    // Start is called before the first frame update
    void Start()
    {
        int sceneActuelleIndex = SceneManager.GetActiveScene().buildIndex;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(sceneActuelleIndex + 1);
        }

   
    }
}
