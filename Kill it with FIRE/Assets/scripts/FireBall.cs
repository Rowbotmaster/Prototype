using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour 
{
	public GameObject fireLingPrefab;


	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 4f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter (Collision collision)
	{
        GameObject GO = Instantiate(fireLingPrefab, this.transform.position, Quaternion.identity);
        Destroy (this.gameObject);
	}
}
