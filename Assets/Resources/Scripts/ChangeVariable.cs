using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVariable : MonoBehaviour {
    
    public GameObject objectWithComponent;
    public string methodName;
    public float newVariable;

    private void OnTriggerEnter(Collider other)
    {
        objectWithComponent.SendMessage(methodName, newVariable);
    }
}
