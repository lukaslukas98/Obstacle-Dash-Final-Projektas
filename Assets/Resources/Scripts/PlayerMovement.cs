using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private Rigidbody PlayerRigidBody;
    public float speed, movementSpeed, jumpforce;
    private Camera Camera;
    private bool jumped;
    private GameObject Engine;
    public bool downForce = false;

    void Start()
    {
        Engine = GameObject.Find("Level Engine");
        PlayerRigidBody = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
       // Debug.Log("Speed:" + PlayerRigidBody.velocity.z);
        #region Player movement
        PlayerRigidBody.AddForce(new Vector3(0, 0, speed));//Forward momentum

        if (Input.GetKey("d")|| Input.GetKey("right"))//Right
        {
            PlayerRigidBody.AddForce(new Vector3(movementSpeed, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))//Left
        {
            PlayerRigidBody.AddForce(new Vector3(-movementSpeed, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey("w") || Input.GetKey("up"))//Speed up
        {
            PlayerRigidBody.AddForce(new Vector3(0, 0, speed));
            if (Camera.fieldOfView < 80)
                Camera.fieldOfView += 0.2f;
        }
        else
        {
            if (Camera.fieldOfView > 60)
                Camera.fieldOfView -= 0.4f;
        }
        if (Input.GetKey("s") || Input.GetKey("down"))//Slow down
        {
            PlayerRigidBody.AddForce(new Vector3(0, -speed / 3, 0));
        }
        if (Input.GetKey("space") && jumped == false)//Jump
        {
            jumped = true;
            PlayerRigidBody.AddForce(new Vector3(0, jumpforce * 2, 0));
        }
        if (jumped == true || downForce == true)//If in jump, push down
        {
            PlayerRigidBody.AddForce(new Vector3(0, -jumpforce / 15, 0));
        }
        #endregion

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacles")//If player collides with obstacle game is over
        {
            if (PlayerRigidBody.transform.position.z > PlayerPrefs.GetFloat("highscore", 0))
                PlayerPrefs.SetFloat("highscore", PlayerRigidBody.transform.position.z);
            Engine.GetComponent<GameEngine>().invokeGameOver();
        }
    }

    void SetDownforce(int d)
    {
        if (d == 1)
            downForce = true;
        else
            downForce = false;
    }

    void SetJumped(int d)
    {
        if (d == 1)
            jumped = false;
        else
            jumped = true;
    }
}          
