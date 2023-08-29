using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessRunEngine : MonoBehaviour {

    public Transform PlayerPos;
    private Transform GeneratorPos;
    private Transform FloorPos;
    private GameObject[] Obstacles;
    private bool generated = false;
    private int randomnumber;
    public int distanceBetweenObstacles;
    public Text Score;
    public Text Highscore;
    public Queue<GameObject> queue = new Queue<GameObject>();
    
    void Start (){
        if(PlayerPos == null)
        PlayerPos = GameObject.Find("Player").GetComponent<Transform>();
        GeneratorPos = GameObject.Find("Generate_obstacles").GetComponent<Transform>();
        FloorPos = GameObject.Find("Floor").GetComponent<Transform>();
        Obstacles = Resources.LoadAll<GameObject>("Prefabs/Obstacles") as GameObject[];
       // PlayerPrefs.DeleteAll();
        Highscore.text = PlayerPrefs.GetFloat("highscore",0).ToString("0");
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
        Generate(new Vector3(0, 1, distanceBetweenObstacles*2));
        Generate(new Vector3(0, 1, distanceBetweenObstacles*3));
        Generate(new Vector3(0, 1, distanceBetweenObstacles*4));
        Generate(new Vector3(0, 1, distanceBetweenObstacles*5));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 6));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 7));
        Generate(new Vector3(0, 1, distanceBetweenObstacles * 8));
    }
    
    void FixedUpdate () {
        if (GeneratorPos.position.z % distanceBetweenObstacles < 2 && generated == false )
        {
            Generate(GeneratorPos.position+new Vector3(0,0, distanceBetweenObstacles*9));
            if(GeneratorPos.position.z> distanceBetweenObstacles*3)
            Destroy(queue.Dequeue().transform.parent.gameObject);
            generated = true;
        }
        if (GeneratorPos.position.z % distanceBetweenObstacles > 2 && generated == true)
        {
            generated = false;
        }
        GeneratorPos.position = new Vector3(0, 1, PlayerPos.position.z);
        FloorPos.position = new Vector3(0, 0, PlayerPos.position.z + 150);
        Score.text = PlayerPos.position.z.ToString("0");
    }
}
