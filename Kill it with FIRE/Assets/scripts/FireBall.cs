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

    // sets the damage that a fireball deals
    public int fireBallDamage = 1;

    // used to delete the fireballs if they are in the scene for too long
    private float countDown;


    // Use this for initialization
    void Start()
    {

        countDown = (Time.time + 4);

        player = GameObject.Find("FPS_Game").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= countDown)
        {
            SelfDestruct();
        }
    }

    //-------------------------------------------------
    //OnTriggerEnter()
    // called when this object enters another object that is a trigger and if the other object is a fire this object won't spawn another fire
    // Param:
    //      Collider other - the other object
    // Return:
    //      Void
    //-------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            flameOn = true;
        }

        if (flameOn == false && other.gameObject.tag == "Surface")
        {

            GameObject GO = Instantiate(fireLingPrefab, this.transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Player")
        {
            //player.PlayerTakeDamage(fireBallDamage);
            other.gameObject.GetComponent<PlayerScript>().PlayerTakeDamage(fireBallDamage);

            flameOn = true;
        }


        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyScript>().EnemyTakeDamage(fireBallDamage);

            flameOn = true;
        }
        SelfDestruct();
    }

    //-------------------------------------------------
    //OnCollisionEnter()
    // called when this object hits another object and either spawns a fire or tells the other object to run it's damage function
    // Param:
    //      Collision collision - the other object
    // Return:
    //      Void
    //-------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {

        //if (flameOn == false && collision.gameObject.tag == "Surface")
        //{

        //    GameObject GO = Instantiate(fireLingPrefab, this.transform.position, Quaternion.identity);
        //}
        //if (collision.gameObject.tag == "Player")
        //{
        //    collision.gameObject.GetComponent<PlayerScript>().PlayerTakeDamage(fireBallDamage);

        //    flameOn = true;
        //}


        //if (collision.gameObject.tag == "Enemy")
        //{
        //    collision.gameObject.GetComponent<EnemyScript>().EnemyTakeDamage(fireBallDamage);

        //    flameOn = true;
        //}
        //SelfDestruct();
    }

    //-------------------------------------------------
    //SelfDestruct()
    // called either when 4 seconds have passed or when this object hits another object
    // Return:
    //      Void
    //-------------------------------------------------
    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
