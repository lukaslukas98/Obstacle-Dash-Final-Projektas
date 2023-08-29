using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{

    private Rigidbody PlayerRigidBody;
    public float speed, movementSpeed, jumpforce;
    private Camera Camera;
    private bool jumped;
    public bool downForce = false;
    public GameObject perceptorParent;
    public IsTriggered[] perceptorArray;
    public IsTriggered[] perceptorArray2;
    public IsTriggered JumpTrigger;
    public IsTriggered DownTrigger;
    public IsTriggered SlowTrigger;
    Transform perceptor;
    private GameObject Engine;

    void Start()
    {
        Engine = GameObject.Find("Level Engine");
        PlayerRigidBody = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        getOrders();
        PlayerRigidBody.AddForce(new Vector3(0, 0, speed));//Forward momentum
        perceptorParent.transform.position = new Vector3(0, 1, transform.position.z - 2);

        //Jei pašokimo receptorius yra aktyvuotas ir žaidėjo objektas nėra pašokes
        if (JumpTrigger.triggered == true && jumped == false)
        {
            //Nurodyti kad žaidėjo objektas yra pašokes
            jumped = true;
            //Pridėti prie žaidėjo objekto jėga kurios kryptis yra į viršų
            PlayerRigidBody.AddForce(new Vector3(0, jumpforce * 2, 0));
        }

        //Jei lėtinimo receptorius aktyvuotas
        if (SlowTrigger.triggered == true)
        {
            //Pridėti jėga priešinga žaidėjo objekto judejimui
            PlayerRigidBody.AddForce(new Vector3(0, 0, -speed / 4));
        }

        // Debug.Log("Speed:" + PlayerRigidBody.velocity.z);



        /*
        #region Player movement

        if (Input.GetKey("d"))//Right
        {
            PlayerRigidBody.AddForce(new Vector3(movementSpeed, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))//Left
        {
            PlayerRigidBody.AddForce(new Vector3(-movementSpeed, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey("w"))//Speed up
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
        if (Input.GetKey("s"))//Slow down
        {
            PlayerRigidBody.AddForce(new Vector3(0, 0, -speed/4));
        }
        if (Input.GetKey("space") && jumped == false)//Jump
        {
            jumped = true;
            PlayerRigidBody.AddForce(new Vector3(0, jumpforce * 2, 0));
        }
        #endregion*/
    }

    void getOrders()
    {
        //Sukuriamas sąrašas kuriame bus laikomi receptoriai nenusiduriantys su kliūtimis
        List<Transform> notTriggeredPerceptors = new List<Transform>();
        int number = 0;
        foreach (var perceptor in perceptorArray)
        {
            //Jei receptoriai nesusiduria arba jų antros eilės receptoriai nesusiduria su kliūtimis
            if (perceptor.triggered == false && perceptorArray2[number].triggered == false )
            {
                //Receptoriai pridedami į sąrašą
                notTriggeredPerceptors.Add(perceptor.gameObject.transform);
            }
            number++;
        }

        //Jei yra receptorių kurie nesusiduria su kliūtimis sąrašas nėra tuščias
        if (notTriggeredPerceptors.Count != 0)
        {
            Transform closestPerceptor = null;
            foreach (var perceptor in notTriggeredPerceptors)
            {
                if (closestPerceptor == null)
                    closestPerceptor = perceptor;
                //Ieškome receptoriaus arčiausio prie centro, iš žaidėjo objekto pusės
                if (Mathf.Abs(closestPerceptor.position.x - PlayerRigidBody.transform.position.x/1.2f) > Mathf.Abs(perceptor.position.x - PlayerRigidBody.transform.position.x/1.2f))
                {
                    closestPerceptor = perceptor;
                }
            }
            perceptor = closestPerceptor;
        }

    }

    void doOrders()
    {
        float direction = 0;
        float aimingfor = 0;

        //Randama žaidėjo judėjimo kryptis pagal arčiausiai rastą laisvą receptorių
        if (PlayerRigidBody.transform.position.x > perceptor.position.x +0.1f)
        {
            aimingfor = perceptor.position.x;
            direction = -movementSpeed;
        }
        if (PlayerRigidBody.transform.position.x < perceptor.position.x - 0.1f)
        {
            aimingfor = perceptor.position.x;
            direction = movementSpeed;
        }
        
        //Žaidėjo objektas stumiamas nurodyto receptoriaus kryptimi
        PlayerRigidBody.AddForce(new Vector3(direction, 0, 0), ForceMode.VelocityChange);

        /*
        Debug.Log("Aiming for:"+ aimingfor);
        Debug.Log("Currently at:" + PlayerRigidBody.transform.position.x);
        Debug.Log("Moving towards:" + direction);
        Debug.Log("-------------------------------------");*/
    }

    private void Update()
    {

        //Jei žaidėjo objektas yra ore arba nustatyta pastovi jėga žemyn
        if (jumped == true || downForce == true)
        {
            //Jei nusileidimo receptorius neliečia jokių kliūčių 
            if (DownTrigger.triggered == false)
            {
                //Padidinti jėga stumiančia žemyn
                PlayerRigidBody.AddForce(new Vector3(0, -jumpforce / 7, 0));
            }
            //Jėga pastoviai stumianti žemyn
            PlayerRigidBody.AddForce(new Vector3(0, -jumpforce / 15, 0));
        }

        doOrders();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            // if (PlayerPrefs.GetFloat("highscore", 0) < PlayerRigidBody.transform.position.z)
            //     PlayerPrefs.SetFloat("highscore", PlayerRigidBody.transform.position.z);
            
            if(PlayerRigidBody.transform.position.z> PlayerPrefs.GetFloat("highscore", 0))
            PlayerPrefs.SetFloat("highscore",PlayerRigidBody.transform.position.z);

            Engine.GetComponent<GameEngine>().invokeAIGameOver();
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
