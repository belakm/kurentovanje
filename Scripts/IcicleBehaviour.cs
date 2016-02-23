using UnityEngine;
using System.Collections;

public class IcicleBehaviour : MonoBehaviour {

	public bool triggered;
	public float icicleMass;
	public float icicleGravityScale;

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Player" && !triggered) {
			triggered = true;
			Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
			body.mass = icicleMass;
			body.gravityScale = icicleGravityScale;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
