using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public Vector3 cameraOffset = new Vector3(0, 1.5f, -5);
    public Transform CameraPos;

    // Use this for initialization
    void Start (){
        if(CameraPos == null)
        CameraPos = GameObject.Find("Camera").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        CameraPos.transform.position = transform.position + cameraOffset;//Camera follow
    }
}
