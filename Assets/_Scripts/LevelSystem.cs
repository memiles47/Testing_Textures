using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour
{
    // Declaration of public variables
    public int level;
    public int expPoints;

    // Declaration of private reference variables

    // Declaration of private misc variables
    //private int levelUpExp;
    private PlayerCombat  playerCombat;
    private int baseLevelUp;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Use this for initialization
	void Start ()
    {
        level = 1;
        baseLevelUp = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LevelUp();
	}

    public void LevelUp()
    {
        if(expPoints >= level * baseLevelUp + (int)Mathf.Pow((float)level, 2.0f))      
        {
            expPoints -= level * baseLevelUp + (int)Mathf.Pow((float)level, 2.0f);
            level += 1;
            LevelEffects();
        }
    }

    public void LevelEffects()
    {
        playerCombat.maxHealth += 10 + (int)Mathf.Pow((float)level, 2.0f);
        playerCombat.attackDamage += (int)Mathf.Pow((float)level, 2.0f);
        playerCombat.health = playerCombat.maxHealth;
    }
}
