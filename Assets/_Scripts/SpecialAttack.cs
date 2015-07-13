using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour
{
    // Declaration of public variables
    public bool opponentBased;
    public KeyCode key;
    public float damageFactor;
    public int stunTime;
    public bool specialAttack;
    public GameObject particleEffect;
    public int projectile;

    // Declaration of private reference variables
    private PlayerCombat playerCombat;

    // Declaration of private misc variables

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
    }

    //// Use this for initialization
    //void Start ()
    //{
	
    //}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(key) && !playerCombat.specialAttack)
        {
            playerCombat.ResetAttack();
            playerCombat.specialAttack = true;
            specialAttack = true;
        }

        if (specialAttack)
        {
            if(playerCombat.Attack(stunTime, damageFactor, key, particleEffect, projectile, opponentBased))
            {

            }
            else
            {
                specialAttack = false;
            }
        }
	}
}
