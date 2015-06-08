using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    // Declaration of public variables
    public GameObject opponent; // This variable must be public because it will be acessed from another script
    public AnimationClip combat;
    
    // Declaration of private reference variables
    private Animation anim;

    // Initialize private reference variables
    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    // Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            anim.Play(combat.name);
            ClickToMove.fighting = true;
        }

        if (!anim.IsPlaying(combat.name))
        {
            ClickToMove.fighting = false;
        }
	}
}
