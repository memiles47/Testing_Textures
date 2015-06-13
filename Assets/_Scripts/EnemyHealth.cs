using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    // System information
    // Screen Width and Height 2160 x 1440

    // Declaration of public variables
    public Texture2D frame;
    public Rect framePosition;
    public Texture2D healthBar;
    public Rect healthBarPosition;
    public float percentHealth;

    // Declaration of private misc variables
    private  float barWidthFac;
    private float healthBarXPercent;
    private float healthBarYPercent;
    private float healthBarHeightPercent;
    private float healthBarWidthPercent;

    // Declaration of private reference variables
    private GameObject player;
    private PlayerCombat playerCombat;
    private Mob target;

    // Awake is called wheather GameObject is instantiated or not
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
    }

	// Use this for initialization
	void Start ()
    {
        healthBarXPercent = 1.19f;
        healthBarYPercent = 1.55f;
        healthBarHeightPercent = 0.41f;
        healthBarWidthPercent = 0.792f;
        //percentHealth = 100.00f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // target changes during combat
        if (playerCombat.opponent != null)
        {
            target = playerCombat.opponent.GetComponent<Mob>();
            percentHealth = (float)target.health / (float)target.maxHealth;
        }
        else
        {
            target = null;
            percentHealth = 0;
        }
	}

    void OnGUI()
    {
        if (target != null && playerCombat.countDown != 0)
        {
            DrawFrame();
            DrawBar();
        }
    }

    void DrawFrame()
    {
        framePosition.x = (Screen.width - framePosition.width) / 2;
        GUI.DrawTexture(framePosition, frame);
    }

    void DrawBar()
    {
        healthBarPosition.x = framePosition.x * healthBarXPercent;
        healthBarPosition.y = framePosition.y * healthBarYPercent;
        healthBarPosition.width = framePosition.width * healthBarWidthPercent * percentHealth;
        healthBarPosition.height = framePosition.height * healthBarHeightPercent;
        GUI.DrawTexture(healthBarPosition, healthBar);
    }
}
