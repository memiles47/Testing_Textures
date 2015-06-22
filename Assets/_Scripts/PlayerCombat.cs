using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    // Declaration of public variables
    public GameObject opponent; // This variable must be public because it will be acessed from another script
    public Mob stun; // This variable must be public because it will be acessed from another script
    public AnimationClip fighting;
    public AnimationClip death;
    public int countDown;
    public int attackDamage;
    public int health;
    public int maxHealth;
    public bool specialAttack;
    
    // Declaration of private reference variables
    private Animation anim;

    // Declaration of private misc variables
    private double impactTime;
    private bool impacted;
    private int combatEscapeTime;
    private bool startedDeath;
    private float range;
    private PlayerCombat playerCombat;
    private bool endedDeath;
    
    // Initialize private reference variables
    void Awake()
    {
        anim = GetComponent<Animation>();
        playerCombat = GetComponent<PlayerCombat>();
        //maxHealth = 100000;
    }

    // Use this for initialization
	void Start ()
    {
        impactTime = 0.35f;
        impacted = false;
        health = maxHealth;
        startedDeath = false;
        combatEscapeTime = 5;
        countDown = combatEscapeTime;
        range = 1.25f;
        endedDeath = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!specialAttack)
        {
            Attack(0, 1.0f, KeyCode.Space, null);
        }

        PlayerDeath();
	}

    public bool Attack(int stunTIme, float damageFactor, KeyCode key, GameObject particleEffect)
    {
        if (Input.GetKey(key) && InRange())
        {
            Debug.Log("You Pressed" + key);

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
            if(specialAttack)
            {
                specialAttack = false;
            }
            return false;
        }

        Impact(stunTIme, damageFactor, particleEffect);
        return true;
    }

    public void ResetAttack()
    {
        ClickToMove.isFighting = false;
        impacted = false;
        anim.Stop(fighting.name);
    }


    void Impact(int timeStunned, float scaledDamage, GameObject particleEffect)
    {
        if(opponent != null && anim.IsPlaying(fighting.name) && !impacted)
        {
            if(anim[fighting.name].time > anim[fighting.name].length * impactTime && (anim[fighting.name].time < anim[fighting.name].length * 0.90f))
            {
                countDown = combatEscapeTime + 2;
                CancelInvoke("CombatCountDown");
                InvokeRepeating("CombatCountDown", 0, 1);
                opponent.GetComponent<Mob>().TakeDamage((int) (attackDamage * scaledDamage));
                opponent.GetComponent<Mob>().GetStunned(timeStunned);

                // Play Particle Effect
                if (particleEffect != null)
                {
                    Instantiate(particleEffect, new Vector3(opponent.transform.position.x,
                        opponent.transform.position.y + 1.5f, opponent.transform.position.z), Quaternion.identity);
                }

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
        //else
        //{
        //    return;
        //}
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

        if(!anim.IsPlaying(death.name) && startedDeath && !endedDeath)
        {
            Debug.Log("You Have Died");
            endedDeath = true;
        }
    }

    void CombatCountDown()
    {
        countDown -= 1;
        if(countDown == 0)
        {
            CancelInvoke("CombatCountDown");
        }
    }
}
