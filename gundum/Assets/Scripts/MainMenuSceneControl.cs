using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneControl : MonoBehaviour
{
    public GameObject menuUI, htpUI;
    bool onMainMenu = true;

    void Update()
    {
        if (Input.GetButtonDown("StartButton"))
            SceneManager.LoadScene("MechLevel");
        if (Input.GetButtonDown("HELP"))
        {
            onMainMenu = false;
            htpUI.SetActive(true);
            menuUI.SetActive(false);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (onMainMenu)
                Application.Quit();
            else
            {
                onMainMenu = true;
                htpUI.SetActive(false);
                menuUI.SetActive(true);
            }
        }
    }
}
