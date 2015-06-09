using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    // Declaration of public variables
    public GameObject opponent; // This variable must be public because it will be acessed from another script
    public AnimationClip fighting;
    public int attackDamage;
    
    // Declaration of private reference variables
    private Animation anim;

    // Declaration of private misc variables
    //private Mob mob;
    private double impactTime;
    private bool impacted;

    // Initialize private reference variables
    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    // Use this for initialization
	void Start ()
    {
        impactTime = 0.35f;
        impacted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            anim.Play(fighting.name);
            ClickToMove.isFighting = true;
            if (opponent != null)
            {
                transform.LookAt(opponent.transform.position);
            }
        }

        if (!anim.IsPlaying(fighting.name))
        {
            ClickToMove.isFighting = false;
            impacted = false;
        }

        Impact();
	}

    void Impact()
    {
        if(opponent != null && anim.IsPlaying(fighting.name) && !impacted)
        {
            if(anim[fighting.name].time > anim[fighting.name].length * impactTime)
            {
                opponent.GetComponent<Mob>().TakeDamage(attackDamage);
                impacted = true;
            }
        }
    }
}
