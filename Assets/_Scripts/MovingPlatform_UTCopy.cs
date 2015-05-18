using UnityEngine;
using System.Collections;

public class MovingPlatform_UTCopy: MonoBehaviour
{
    // Declaration of public variables
    public Transform platform;
    public Transform startPosition;
    public Transform endPosition;
    public float platformSpeed;

    // Declaration of private variables
    private Vector3 direction;
    private Transform destination;

    void Start()
    {
        SetDestination(startPosition);
    }
    
    void FixedUpdate()
    {
        platform.GetComponent<Rigidbody>().MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

        if(Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
        {
            SetDestination(destination == startPosition ? endPosition : startPosition);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPosition.position, platform.localScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endPosition.position, platform.localScale);
    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }

}
