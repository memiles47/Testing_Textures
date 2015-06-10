using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    // Declaration of public variables
    public GameObject opponent; // This variable must be public because it will be acessed from another script
    public AnimationClip fighting;
    public AnimationClip death;
    public int attackDamage;
    public float range;
    
    // Declaration of private reference variables
    private Animation anim;

    // Declaration of private misc variables
    private double impactTime;
    private bool impacted;
    private int health;
    private bool startedDeath;
    private bool endedDeath;

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
        health = 100;
        startedDeath = false;
        endedDeath = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("Player Health = " + health);
        PlayerDeath();
        if(Input.GetKey(KeyCode.Space) && InRange())
        {
            anim.Play(fighting.name);
            ClickToMove.isFighting = true;
            if (opponent != null)
            {
                transform.LookAt(opponent.transform.position);
            }
        }

        if (anim[fighting.name].time > anim[fighting.name].length * 0.90f)
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
            if(anim[fighting.name].time > anim[fighting.name].length * impactTime && (anim[fighting.name].time < anim[fighting.name].length * 0.90f))
            {
                opponent.GetComponent<Mob>().TakeDamage(attackDamage);
                impacted = true;
            }
        }
    }

    bool InRange()
    {
        if (Vector3.Distance(transform.position, opponent.transform.position) <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetHit(int damage)
    {
        if (health != 0)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
        }
        else
        {
            return;
        }
    }

    public bool IsDead()
    {
        if(health == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void PlayerDeath()
    {
        if(IsDead() && !startedDeath)
        {
            anim.CrossFade(death.name);
            startedDeath = true;
        }

        if(!anim.IsPlaying(death.name) && startedDeath)
        {
            endedDeath = true;
            Debug.Log("You Have Died");
        }
    }
}
