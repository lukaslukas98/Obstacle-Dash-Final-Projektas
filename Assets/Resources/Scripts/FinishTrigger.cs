using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour {

    private GameObject Player;
    private GameObject Engine;

    // Use this for initialization
    void Start ()
    {
        Engine = GameObject.Find("Level Engine");
        Player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            Engine.GetComponent<GameEngine>().invokeLevelWon();
        }
    }
}
