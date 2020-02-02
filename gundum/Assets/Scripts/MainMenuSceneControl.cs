using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("StartButton"))
            SceneManager.LoadScene("MechLevel");
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }
}
