using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void startMission()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void startEndlessRun()
    {
        SceneManager.LoadScene("Endless run");
    }

    public void startEndlessRunAI()
    {
        SceneManager.LoadScene("Endless run AI");
    }

    public void options()
    {

    }

    public void quit()
    {
        Application.Quit();
    }
}
