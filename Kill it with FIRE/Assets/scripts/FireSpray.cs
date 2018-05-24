using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpray : MonoBehaviour 
{
    // establishing variables
	private float fireSpeed = 11f;
	public GameObject firePrefab;
	public Transform fireSpawnPoint;
	public GameObject flameThrower;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Change this to left click when you get the chance
        // when the fire button is held down balls of fire are spawned and arch out from the end of the flamethrower
		if (Input.GetKey (KeyCode.Mouse0)) 
		{
			GameObject GO = Instantiate(firePrefab, fireSpawnPoint.transform.position, Quaternion.identity) as GameObject;
			GO.GetComponent<Rigidbody>().AddForce(flameThrower.transform.forward * fireSpeed, ForceMode.Impulse);
		}
	}
}
