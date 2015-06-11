using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
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

	// Use this for initialization
	void Start ()
    {
        barWidthFac = 1.125f;
        healthBarXPercent = 1.315f;
        healthBarYPercent = 1.70f;
        healthBarHeightPercent = 0.41f;
        healthBarWidthPercent = 0.792f;
        percentHealth = 100.00f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnGUI()
    {
        DrawFrame();
        DrawBar();
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
        healthBarPosition.width = framePosition.width * healthBarWidthPercent * (percentHealth / 100);
        healthBarPosition.height = framePosition.height * healthBarHeightPercent;
        GUI.DrawTexture(healthBarPosition, healthBar);
    }
}
