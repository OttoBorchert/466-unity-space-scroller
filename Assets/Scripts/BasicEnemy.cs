using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {

	//The prefab to use as our explosion graphic
	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		//Create the explosion and destroy the enemy when anything collides with it.
		Instantiate (explosion, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
