using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTriggered : MonoBehaviour {

    public bool triggered = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Obstacles")
            triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacles")
            triggered = false;
    }
}
