using UnityEngine;
using System.Collections;

public class DestructableTile : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer renderer;
	private Rigidbody2D body;
	private ParticleSystem particles;
	private BoxCollider2D collider;
	private bool shattered;

	public Vector2 forceToBreak;

	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		body = GetComponent<Rigidbody2D> ();
		collider = GetComponent<BoxCollider2D> ();
		particles = GetComponent<ParticleSystem> ();
	}

	void OnCollisionEnter2D (Collision2D col){

		Vector3 impact = col.relativeVelocity;

		bool itShattered = Mathf.Abs(impact.x) > forceToBreak.x || Mathf.Abs(impact.y) > forceToBreak.y;

		if( itShattered ) {
			// Disable picture and collider and emit those particles
			renderer.enabled = false;
			collider.enabled = false;
			body.isKinematic = true;
			particles.Emit (30);
			shattered = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
