using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    // Declaration of public variables
    public float speed;
    public float range;
    public int attackDamage;
    public AnimationClip run;
    public AnimationClip idle;
    public AnimationClip die;
    public AnimationClip attack;
    public int health;
    public int maxHealth;

    // Declaration of private reference variables
    private Transform player;
    private Animation anim;
    private CharacterController charController;
    private PlayerCombat playerCombat;

    // Declaration of private misc variables
    private double impactTime;
    private bool impacted;
    

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
    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        impactTime = 0.35f;
        impacted = false;
        attackDamage = 25;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Enemy Health = " + health);
        // Check if player is in range, if so do not run, you will be attacking,
        // if not, chase at speed
        if (!IsDead())
        {
            if (!InRange())
            {
                // Not in range, chase player character
                Chase();
            }
            else
            {
                // In rangem, stop and attack
                //anim.CrossFade(idle.name);
                anim.CrossFade(attack.name);
                MobCombat();

                if(anim[attack.name].time > anim[attack.name].length * 0.95f)
                {
                    impacted = false;
                }
            }
        }
        else
        {
            Death();
        }
    }

    bool InRange()
    {
        if (Vector3.Distance(transform.position, player.position) <= range)
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
    void OnMouseOver()
    {
        playerCombat.opponent = gameObject;
    }

    public void TakeDamage(int damage)
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

    bool IsDead()
    {
        return health <= 0;
    }

    void Death()
    {
        playerCombat.opponent = null;
        anim.CrossFade(die.name);

        if(anim[die.name].time > anim[die.name].length * 0.90f)
        {
            Destroy(gameObject);
        }
    }

    void MobCombat()
    {
        if(playerCombat.IsDead())
        {
            anim.CrossFade(idle.name);
        }
        else if((anim[attack.name].time > anim[attack.name].length * impactTime) && !impacted &&
            (anim[attack.name].time < anim[attack.name].length * 0.95f) && !playerCombat.IsDead())
        {
            playerCombat.GetHit(attackDamage);
            impacted = true;
        }
    }
}