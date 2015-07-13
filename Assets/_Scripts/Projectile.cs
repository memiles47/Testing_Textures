using UnityEngine;
using System.Collections;

public class Projectile: MonoBehaviour
{
    // Declaration of private reference variables
    private PlayerCombat playerCombat;
    private GameObject player;
    
    // Declaration of public misc variables
    public float speed;
    public int damage;
    //public GameObject particleEffect;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        
    }

    // Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    // OnTriggerEnter is called when the Collider other enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit" + other.tag);
            playerCombat.opponent.GetComponent<Mob>().TakeDamage(gameObject.GetComponent<Projectile>().damage);
        }
    }
}
