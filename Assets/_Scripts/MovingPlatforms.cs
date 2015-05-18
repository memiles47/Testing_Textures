using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour
{
    // Declaration of public variables
    public Transform movingPlatform;
    public Transform positionOne;
    public Transform positionTwo;
    public Vector3 newDestination;
    public string currentState;
    public float speed;
    public float resetTime;

	// Use this for initialization
	void Start ()
    {
        ChangeTarget();
	}

    // This function is called every fixed framerate frame.
    public void FixedUpdate()
    {
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newDestination, speed * Time.fixedDeltaTime);
    }

    void ChangeTarget()
    {
        if (currentState == "Moving to position 1")
        {
            currentState = "Moving to position 2";
            newDestination = positionTwo.position;
        }

        else if (currentState == "Moving to position 2")
        {
            currentState = "Moving to position 1";
            newDestination = positionOne.position;
        }

        else if (currentState == "")
        {
            currentState = "Moving to position 2";
            newDestination = positionTwo.position;
        }

        Invoke("ChangeTarget", resetTime);
    }
}
