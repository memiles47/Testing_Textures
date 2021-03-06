﻿using UnityEngine;
using System.Collections;

public class MovingPlatformNew : MonoBehaviour
{

	// Declaration of public variables
    public Transform platform;
    public Transform[] platformArray;
    public float platformSpeed = 2.0f;
    public int waitTime = 2;
    
    // Declare private reference variables
    private GameObject platformWalls;
    private Transform positionOne;
    private Transform positionTwo;

    // Declaration of private Misc variables
    private Vector3 newDestination;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        // Initialize reference variables
        platformWalls = GameObject.Find("PlatformWalls");
        positionOne = platformArray[0];
        positionTwo = platformArray[1];

        // Initialize misc variables
        newDestination = positionOne.position;
    }

    // This function is called every fixed framerate frame
    public void FixedUpdate()
    {
        // Move platform using Vector3.MoveTowards('current Vector3 position', 'Vector3 destination', 'float step' multiplied by Time.fixedDeltaTime)
        transform.position = Vector3.MoveTowards(transform.position, newDestination, platformSpeed * Time.fixedDeltaTime);

        // Check distance to destination and move to new destination if we have arrived and depending on where we arrived
        if (Vector3.Distance(platform.position, newDestination) < platformSpeed * Time.fixedDeltaTime)
        {
            if (newDestination == positionOne.position)
            {
                StartCoroutine(StopWaitSet(positionTwo));
            }

            else if (newDestination == positionTwo.position)
            {
                StartCoroutine(StopWaitSet(positionOne));
            }
        }
    }
	
    // Draw boxes in the area of the platform destinations
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(platformArray[0].position, platform.localScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(platformArray[1].position, platform.localScale);
    }

    // Turn off wall colliders, wait for variable time, trun walls back on and set newDestination
    IEnumerator StopWaitSet(Transform dest)
    {
        platformWalls.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        platformWalls.SetActive(true);
        newDestination = dest.position;
    }
}
