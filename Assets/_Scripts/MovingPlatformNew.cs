using UnityEngine;
using System.Collections;

public class MovingPlatformNew : MonoBehaviour {

	// Declaration of public variables
    public Transform platform;
    public Transform positionOne;
    public Transform positionTwo;
    public float platformSpeed = 2.0f;
    public int waitTime = 2;
    
    // Declare private reference variables
    private GameObject platformWalls;

    // Declaration of private Misc variables
    private Vector3 newDestination;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        //platform = gameObject.GetComponent<Transform>();
        platformWalls = GameObject.Find("PlatformWalls");

        newDestination = positionOne.position;
    }

    // This function is called every fixed framerate frame
    public void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, newDestination, platformSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(platform.position, newDestination) < platformSpeed * Time.fixedDeltaTime)
        {
            if (newDestination == positionOne.position)
            {
                StartCoroutine(StopWaitSet(positionTwo));
            }
            
            else if(newDestination == positionTwo.position)
            {
                StartCoroutine(StopWaitSet(positionOne));
            }
        }
    }
	
    // Draw boxes in the area of the platform destinations
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(positionOne.position, platform.localScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(positionTwo.position, platform.localScale);
    }

    IEnumerator StopWaitSet(Transform dest)
    {
        platformWalls.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        platformWalls.SetActive(true);
        newDestination = dest.position;
    }
}
