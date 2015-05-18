using UnityEngine;
using System.Collections;

public class PlatformHolder : MonoBehaviour {

    // Detect when and object enters the platform collider and parents it
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.transform.parent = gameObject.transform;
        }
    }

    // Detect when an object exits the platform collider and removes the parent
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = null;
        }
    }
}
