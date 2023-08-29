using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour {

    public GameObject textObject;
    public string text;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            textObject.GetComponent<Animator>().Play("Text Enter");
            textObject.GetComponent<Text>().text = text;
            Invoke("AnimationExit", 3);
        }
    }

    void AnimationExit()
    {
        textObject.GetComponent<Animator>().Play("Text Exit");
    }
}
