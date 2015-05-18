using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	// Declaration of public variables
    public Transform platform;
    public Transform positionOne;
    public Transform positionTwo;
    public float platformSpeed = 2.0f;

    // Declaration of private reference variables
    private Rigidbody refPlatformRigid;

    // Declaration of private misc. variables
    private Transform destination;
    private Vector3 direction;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        refPlatformRigid = platform.GetComponent<Rigidbody>();
    }
        
    // Use this for initialization
	void Start ()
    {
        SetDestination(positionOne);
    }

    // This function is called every fixed framerate frame
    public void FixedUpdate()
    {
        refPlatformRigid.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
        {
            SetDestination(destination == positionOne ? positionTwo : positionOne);
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

    // Set current destination for platform movement
    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }
}
