using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleGeneration : MonoBehaviour
{
    
    private Transform PlayerPos;
    private Transform GeneratorPos;
    private Transform FloorPos;
    private GameObject[] Obstacles;
    private GameObject spotlight;
    private bool generated = false;
    private int randomnumber;
    public int distanceBetweenObstacles = 30;
    public int distanceToFirstObstacle;
    public int floorPosition = 150;
    public Queue<GameObject> queue = new Queue<GameObject>();
    public Queue<GameObject> queue1 = new Queue<GameObject>();
    public int levelLenght = 1000;

    void Start()
    {
        GameObject.Find("Player").GetComponent<Light>().enabled = true;
        PlayerPos = GameObject.Find("Player").GetComponent<Transform>();
        GeneratorPos = GameObject.Find("Obstacle_Generator").GetComponent<Transform>();
        FloorPos = GameObject.Find("Floor").GetComponent<Transform>();
        GeneratorPos.position = PlayerPos.position;
        Obstacles = Resources.LoadAll<GameObject>("Prefabs/Obstacles") as GameObject[];
        spotlight = Resources.Load<GameObject>("Prefabs/Spotlight") as GameObject;
        GenerateOnStart();
    }

    void GenerateWithoutLight(Vector3 position)
    {
        Transform transform = new GameObject().transform;
        transform.position = position;
        randomnumber = Random.Range(0, Obstacles.Length);
        queue.Enqueue(Instantiate(Obstacles[randomnumber], transform));
    }

    void Generate(Vector3 position)
    {
        Transform transform = new GameObject().transform;
        transform.position = position;
        randomnumber = Random.Range(0, Obstacles.Length);
        queue.Enqueue(Instantiate(Obstacles[randomnumber], transform));

        GenerateLight(position);
    }

    void GenerateLight(Vector3 position)
    {
        Transform transform1 = new GameObject().transform;
        transform1.position = position - new Vector3(0, 0, distanceBetweenObstacles * 4);
        queue1.Enqueue(Instantiate(spotlight, transform1));
    }

    void GenerateOnStart()
    {
        GenerateWithoutLight(new Vector3(0, 1, distanceBetweenObstacles * 2 + distanceToFirstObstacle));
        GenerateWithoutLight(new Vector3(0, 1, distanceBetweenObstacles * 3 + distanceToFirstObstacle));
        GenerateWithoutLight(new Vector3(0, 1, distanceBetweenObstacles * 4 + distanceToFirstObstacle));
        GenerateWithoutLight(new Vector3(0, 1, distanceBetweenObstacles * 5 + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 6 + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 7 + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 8 + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 9 + distanceToFirstObstacle));
    }
    

    void FixedUpdate()
    {
        if (GeneratorPos.position.z % distanceBetweenObstacles < 3 && generated == false && PlayerPos.position.z < levelLenght)
        {
            Generate(GeneratorPos.position + new Vector3(0, 0, distanceBetweenObstacles * 10 + distanceToFirstObstacle));
            Destroy(queue.Dequeue().transform.parent.gameObject);
            Destroy(queue1.Dequeue().transform.parent.gameObject);
            generated = true;
        }
        if (GeneratorPos.position.z % distanceBetweenObstacles > 3 && generated == true)
        {
            generated = false;
        }
        GeneratorPos.position = new Vector3(0, 1, PlayerPos.position.z);
        if (PlayerPos.position.z < levelLenght)
            FloorPos.position = new Vector3(0, 0, PlayerPos.position.z + floorPosition);

        if (GeneratorPos.position.z % distanceBetweenObstacles < 3 && generated == false && PlayerPos.position.z > levelLenght)
        {
            GenerateLight(GeneratorPos.position + new Vector3(0, 0, distanceBetweenObstacles * 10 + distanceToFirstObstacle));
            Destroy(queue1.Dequeue().transform.parent.gameObject);
            generated = true;
        }

    }
}
