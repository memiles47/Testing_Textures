using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    // Declaration of public variables
    public float speed;
    public float range;
    public AnimationClip idle;
    public AnimationClip run;

    // Declaration of private reference variables
    private Transform player;
    private Animation anim;
    private CharacterController enemyController;

    // Awake is called when the script instance is being loaded
    // Everything in the Awake function is my variation to the code
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyController = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Chase();
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
        transform.LookAt(player.position);
    }
}
