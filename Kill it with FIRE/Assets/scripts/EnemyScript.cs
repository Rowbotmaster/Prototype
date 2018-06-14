using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    // how close the player can come before the enmey will persue them
    public float detectionRange = 25;

    // defines the player is filled in once the game starts
    public GameObject thePlayer;

    // allows the enemy to navigate the environment
    private NavMeshAgent theWay;

    // defines the damage indicator object
    public GameObject oof;

    // used to turn the damage indicator off once it has been used
    public float unOof;

    // how fast the fireballs will be traveling when that are created
    private float fireSpeed = 11f;
    // defines for the script what to create for the fireballs
    public GameObject firePrefab;
    // defines where the fireballs will initially be spawned
    public Transform fireSpawnPoint;
    // defines the flamethrower and uses said flamethrower to determin the direction the fireballs will travel
    public GameObject flameThrower;

    // stores the enemies current health value
    public int enemyHealth = 70;

    // records the damage that the enemy last took so it can be used for damage over time when thay are burning
    private int damageToTake;

    // recordes if the enemy is burning
    private bool enemyIsBurning = false;
    // how long the enemy has been burning
    public float enemyBurning;

    // prevents the player from taking damage from multiple sources at once and dying instantly
    private bool recentlyDamaged = false;

    // turns the particle system attached to the enemy on
    public GameObject enemyParticles;


    // Use this for initialization
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        theWay = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBurning <= Time.time)
        {
            recentlyDamaged = false;
        }

        if (enemyIsBurning == true)
        {
            EnemyTakeDamage(damageToTake);
        }

        if (Vector3.Distance(transform.position, thePlayer.transform.position) < detectionRange)
        {
            theWay.destination = thePlayer.transform.position;
        }

        if (unOof < Time.time)
        {
            UnOofNow();
        }

        RaycastHit hit;

        float distanceOfRay = 8;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceOfRay))
        {
            if (hit.transform.tag == "Player")
            {
                ShootFire();
            }
        }
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceOfRay);
    }

    //---------------------------------------------------------------------------------------------------------------------
    // EnemyTakeDamage()
    // called when the enemy is damaged and reduces said enemies health while setting them on fire for damage over time
    // also now turns on a damage indicator allowing the player to see that they are affecting the enemy
    //
    // Param:
    //      int damage - the damage the enemy will take
    //
    // return:
    //      Void
    //----------------------------------------------------------------------------------------------------------------------
    public void EnemyTakeDamage(int damage)
    {
        if (recentlyDamaged == false)
        {
            enemyHealth -= damage;

            oof.SetActive(true);
            unOof = Time.time + 0.1f;

            damageToTake = damage;

            enemyBurning = Time.time + 0.5f;


            enemyIsBurning = true;

            enemyParticles.SetActive(true);


            if (enemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }

            recentlyDamaged = true;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------
    //ShootFire()
    // called when the player is in front of an enemy and the enemy shoots fireballs
    //
    // return:
    //      Void
    //----------------------------------------------------------------------------------------------------------------------
    public void ShootFire()
    {
        GameObject GO = Instantiate(firePrefab, fireSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        GO.GetComponent<Rigidbody>().AddForce(flameThrower.transform.forward * fireSpeed, ForceMode.Impulse);
    }

    //-----------------------------------------------------------------------------------------------------------------
    //UnOofNow()
    // called after the damage indicator has been active for long enough
    //
    // return:
    //      Void
    //-------------------------------------------------------------------------------------------------------------
    public void UnOofNow()
    {
        oof.SetActive(false);
    }
}
