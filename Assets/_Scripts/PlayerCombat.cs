using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    // Declaration of public reference variables
    public GameObject opponent; // This variable must be pubic because it will be accessed outsied this script

    // Initialize private reference variables
    void Awake()
    {

    }

    // Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(opponent);
	}
}
