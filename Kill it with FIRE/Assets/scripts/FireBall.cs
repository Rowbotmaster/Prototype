using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour 
{
    // referance for creating fires
    public GameObject fireLingPrefab;
    
    // variables to prevent fire spawning in existing fires
    private bool flameOn = false;

    // tells the script that the PlayerScript exists
    private PlayerScript player;

    public int fireBallDamage = 1;

    private float countDown;


    // Use this for initialization
    void Start ()
    {
        // balls of fire delete themselves after 4 second if they haven't touched anything yet to prevent filling up the scene with balls
        countDown = (Time.time + 4);

        // tells the script how to find the PlayerScript
        player = GameObject.Find("FPS_Game").GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time == countDown)
        {
            SelfDestruct();
        }
	}

    private void OnTriggureEnter(Collider other)
    {
        // tests if a fire is already here
        if (other.tag == "Fire")
        {
            flameOn = true;
        }
    }

	private void OnCollisionEnter (Collision collision)
	{
        // if statement prevents fires stacking and fire spawning in the players face
        if (flameOn == false && collision.gameObject.tag == "Surface")
        {
            // spawns the fires
            GameObject GO = Instantiate(fireLingPrefab, this.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "Player")
        {
            // finding the players health variable
            player.PlayerTakeDamage(fireBallDamage);

            //tells the script not to make a lingering fire here
            flameOn = true;
        }

        // if the player walks into a lingering fire they will take damage
        if (collision.gameObject.tag == "Enemy")
        {
            // finding the players health variable
            collision.gameObject.GetComponent<EnemyScript>().EnemyTakeDamage(fireBallDamage);

            //tells the script not to make a lingering fire here
            flameOn = true;
        }

        // destroys the ball after spawning the fire
        SelfDestruct();
    }

    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
