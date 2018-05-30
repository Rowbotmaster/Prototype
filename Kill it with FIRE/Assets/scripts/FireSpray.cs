using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpray : MonoBehaviour
{
    // how fast the fireballs will be traveling when that are created
    private float fireSpeed = 11f;
    // defines for the script what to create for the fireballs
    public GameObject firePrefab;
    // defines where the fireballs will initially be spawned
    public Transform fireSpawnPoint;
    // defines the flamethrower and uses said flamethrower to determin the direction the fireballs will travel
    public GameObject flameThrower;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameObject GO = Instantiate(firePrefab, fireSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            GO.GetComponent<Rigidbody>().AddForce(flameThrower.transform.forward * fireSpeed, ForceMode.Impulse);
        }
    }
}
