using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpray : MonoBehaviour 
{
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
		if (Input.GetKey (KeyCode.F)) 
		{
			GameObject GO = Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity) as GameObject;
			GO.GetComponent<Rigidbody>().AddForce(flameThrower.transform.forward * fireSpeed, ForceMode.Impulse);
		}
	}
}
