using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneControl : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("StartButton")) 
            SceneManager.LoadScene("game");
        if (Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene("main");
    }
}
