using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour {
    
    public GameObject Player;
    public string NextLevel;
    public GameObject LevelWonCanvas;
    public int lowestCoord = 0;

    // Use this for initialization
    void Start ()
    {
        if(Player == null)
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //Game over if player lower than 0
        if (Player.transform.position.y < lowestCoord)
        {
            invokeGameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    #region Level Won
    public void invokeLevelWon()
    {
        LevelWonCanvas.SetActive(true);
        Invoke("LevelWon", 4);
        Player.GetComponent<PlayerMovement>().enabled = false;
    }

    void LevelWon()
    {
        SceneManager.LoadScene(NextLevel);
    }
    #endregion

    #region Game Over
    public void invokeGameOver()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        Player.GetComponent<PlayerMovement>().enabled = false;
        Invoke("GameOver", 1);
    }

    public void invokeAIGameOver()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        Player.GetComponent<PlayerAI>().enabled = false;
        Invoke("GameOver", 2);
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    public void setlowestCoord(int lowestCoord)
    {
        this.lowestCoord = lowestCoord;
    }
}
