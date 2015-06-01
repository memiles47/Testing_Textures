using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    // Declaration of public variables
    public Vector3 position;
    public float speed;



	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButton(0))
        {
            // Locate where the player clicked on the terrain
            LocatePosition();
        }

        // Move the player to the position clicked
        MoveToPosition();
	}

    void LocatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Debug.Log(position);
        }
    }

    void MoveToPosition()
    {

    }
}
