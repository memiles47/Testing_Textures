using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    // Declaration of public variables
    public float speed;
    public float range;
    public AnimationClip run;
    public AnimationClip idle;

    // Declaration of private reference variables
    private Transform player;
    private Animation anim;
    private CharacterController charController;
    private PlayerCombat playerCombat;

    // Awake is called when the script instance is being loaded
    // Everything in the Awake function is my variation to the code and
    // initiates the private variables rather than the drag and drop in
    // in the inspector
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animation>();
        charController = GetComponent<CharacterController>();
        playerCombat = player.GetComponent<PlayerCombat>();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Check if player is in range, if so do not run, you will be attacking,
        // if not, chase at speed
        if (!InRange())
        {
            // Not in range, chase player character
            Chase();
        }
        else
        {
            // In rangem, stop and attack
            anim.CrossFade(idle.name);
        }
    }

    bool InRange()
    {
        if(Vector3.Distance(transform.position, player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Chase()
    {
        // Rotate to look at player
        transform.LookAt(player.position);

        // Chase Player Character
        // Note: I am using Move here rather than simplemove because if you are
        // going to do extra with your game object (jump, fight etc, it is best
        // to use. (See Unity Answers for the difference)
        charController.Move(transform.forward * Time.deltaTime * speed);
        anim.CrossFade(run.name);
    }

    // OnMouseOver is called every frame while the mouse is over the GUIElement or Collider
    public void OnMouseOver()
    {
        Debug.Log("Mouse is over Mob");
        playerCombat.opponent = gameObject;
    }
}
