using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    // Declaration of public variables
    //public CharacterController charController;
    public Vector3 position;
    public float speed;
    public AnimationClip run;
    public AnimationClip idle;

    // Declaration of private reference variables
    private Animation anim;
    private CharacterController charController;

    // Initialize private reference variables
    public void Awake()
    {
        anim = GetComponent<Animation>();
        charController = GetComponent<CharacterController>(); // **My change to the code**
    }

    // Use this for initialization
	void Start ()
    {
        // Establish player start position
        position = transform.position;
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
            //Debug.Log(position);
        }
    }

    void MoveToPosition()
    {
        if (Vector3.Distance(position, transform.position) > 0.75f)
        {
            // Excecuted when the gameobject is moving
            // Calculate the location of the clicked position relative to the players location
            Vector3 relativePos = position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(relativePos);

            // Reset x and z axis in newRotation
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;

            // Smoothly rotate the player towards the clicked position
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);

            charController.Move(transform.forward * Time.deltaTime * speed);

            anim.CrossFade(run.name);
        }
        else
        {
            // Excuted when the gameobject is not moving
            anim.CrossFade(idle.name);
        }
    }
}
