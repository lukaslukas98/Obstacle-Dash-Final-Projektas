using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleGenerationForest : MonoBehaviour
{
    
    public Transform PlayerPos;
    private Transform GeneratorPos;
    private Transform FloorPos;
    private GameObject[] Obstacles;
    private bool generated = false;
    private int randomnumber;
    public int distanceBetweenObstacles = 30;
    public int distanceToFirstObstacle;
    public int floorPosition = 150;
    public Queue<GameObject> queue = new Queue<GameObject>();
    public int levelLenght = 1000;

    void Start()
    {
        if(PlayerPos == null)
        PlayerPos = GameObject.Find("Player").GetComponent<Transform>();
        GeneratorPos = GameObject.Find("Obstacle_Generator").GetComponent<Transform>();
        FloorPos = GameObject.Find("Floor").GetComponent<Transform>();
        GeneratorPos.position = PlayerPos.position;
        Obstacles = Resources.LoadAll<GameObject>("Prefabs/Obstacles") as GameObject[];
        GenerateOnStart();
    }
    
    void Generate(Vector3 position)
    {
        Transform transform = new GameObject().transform;
        transform.position = position;
        randomnumber = Random.Range(0, Obstacles.Length);
        queue.Enqueue(Instantiate(Obstacles[randomnumber], transform));
    }

    void GenerateOnStart()
    {
        Generate(new Vector3(0, 1, distanceBetweenObstacles + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 2 + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 3 + distanceToFirstObstacle));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 4 + distanceToFirstObstacle));
        Generate(new Vector3(0, 0, distanceBetweenObstacles * 5 + distanceToFirstObstacle));
        Generate(new Vector3(0, 0, distanceBetweenObstacles * 6 + distanceToFirstObstacle));
    }
    

    void FixedUpdate()
    {
        if (GeneratorPos.position.z % distanceBetweenObstacles < 3 && generated == false && PlayerPos.position.z < levelLenght)
        {
            Generate(GeneratorPos.position + new Vector3(0, 0, distanceBetweenObstacles * 7 + distanceToFirstObstacle));
            generated = true;
        }

        if(queue.Count != 0)
        if (GeneratorPos.position.z > queue.Peek().transform.position.z+distanceBetweenObstacles && PlayerPos.position.z < levelLenght)
        {
            Destroy(queue.Dequeue().transform.parent.gameObject);
        }

        if (GeneratorPos.position.z % distanceBetweenObstacles > 3 && generated == true)
        {
            generated = false;
        }
        GeneratorPos.position = new Vector3(0, 1, PlayerPos.position.z);
        if (PlayerPos.position.z < levelLenght)
            FloorPos.position = new Vector3(0, 0, PlayerPos.position.z + floorPosition);
        

    }
}
