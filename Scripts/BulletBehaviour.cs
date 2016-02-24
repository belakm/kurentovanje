using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	public float bulletMass;
	public float bulletGravityScale;
	public float bulletSpeed;
	private SpriteRenderer image;

	private bool isDestroyed;
	private float timeLeft;

	private bool richoched;
	// Audio
	private AudioSource sounds;
	
	// Use this for initialization
	void Start () {
		isDestroyed = false;
		timeLeft = 1.0f;
		sounds = GetComponent<AudioSource> ();
		image = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isDestroyed)
			DestroyTimer ();
	}

	public void Fire(int direction){

		Rigidbody2D bullet = GetComponent<Rigidbody2D>();

		bullet.gravityScale = bulletGravityScale;
		bullet.mass = bulletMass;
		bullet.AddForce(new Vector2 (bulletSpeed * direction,  0), ForceMode2D.Impulse);

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag != "Player")
			isDestroyed = true;

		if (col.gameObject.tag == "Volkodlak") {
			Destroy (image);
		}
		else if (!richoched){
			richoched = true;
			sounds.Play (); // Play richochet sound if
		}
	}

	void DestroyTimer(){
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 ) Destroy (gameObject);
	}
}
