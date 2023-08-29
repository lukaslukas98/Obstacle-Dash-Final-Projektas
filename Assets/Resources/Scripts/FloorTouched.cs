using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTouched : MonoBehaviour {

    public GameObject Player;

    private void Start()
    {
        if (Player == null)
        Player = GameObject.Find("Player");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)//If player touches the floor it's no longer in jump
        {
            Player.SendMessage("SetJumped", 1);
        }
    }
}
