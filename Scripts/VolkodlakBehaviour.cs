using UnityEngine;
using System.Collections;

public class VolkodlakBehaviour : MonoBehaviour {

	private Rigidbody2D volkodlak;
	private float walkTimer;
	public float TurnTimer;
	public int walkDirection;

	public float volkodlakWalkSpeed;
	public float volkodlakHuntSpeed;
	public bool triggered;
	public int bloodParticlesEmitted;
	private bool dead;

	private Animator animator;
	private Rigidbody2D playerObj;

	private SpriteRenderer volkodlakImage;
	private SpriteRenderer skullImage;
	private GameObject skull;
	private PolygonCollider2D volkodlakCollider;
	private ParticleSystem bloodSystem;

	// GroundControl
	private bool isGrounded;
	public Transform groundChecker1;
	public Transform groundChecker2;

	// Use this for initialization
	void Start () {
		volkodlak = GetComponent<Rigidbody2D> ();
		triggered = false;
		walkTimer = Time.time + TurnTimer;
		walkDirection = 1;
		GameObject rawPlayer = GameObject.Find ("Kurent");
		playerObj = rawPlayer.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();

		volkodlakCollider = GetComponent<PolygonCollider2D> ();

		// SPRITES
		volkodlakImage = GetComponent<SpriteRenderer> ();
		skull = gameObject.transform.FindChild("Skull").gameObject;
		skullImage = skull.GetComponent<SpriteRenderer> ();
		skullImage.enabled = false;

		// BLOOD
		bloodSystem = gameObject.transform.FindChild("BloodEmitter").gameObject.GetComponent<ParticleSystem> ();
		bloodSystem.enableEmission = false;

	}

	public bool isDead(){
		return dead;
	}

	// Trigger enter: start hunting player
	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Player") {
			triggered = true;
		}
	}

	// Trigger exit: stop hunting player
	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "Player") {
			triggered = false;
			walkTimer = TurnTimer;
		}
	}

	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "Bullet") {
			volkodlakCollider.enabled = false;
			volkodlakImage.enabled = false;
			skullImage.enabled = true;	
			dead = true;
			skull.layer = 11; // DEAD ENEMIES LAYER
			bloodSystem.Emit(bloodParticlesEmitted);

			// spawn another volkodlak
			spawnABrother ();
		}

	}

	// Move left an right
	void walk(){
		walkTimer -= Time.deltaTime;
		if (walkTimer < 0) {
			walkDirection *= -1;
			volkodlak.velocity = Vector2.zero;
			walkTimer = TurnTimer;
			Vector3 spriteScale = transform.localScale;
			spriteScale.x = walkDirection;
			transform.localScale = spriteScale;
		}
		volkodlak.AddForce (new Vector2 (volkodlakWalkSpeed * walkDirection, 0));
	}

	// Run towards player
	void hunt(){
		int huntDirection;
		if (playerObj.transform.position.x > transform.position.x)
			huntDirection = 1;
		else
			huntDirection = -1;

		if (huntDirection != walkDirection) {
			Vector3 spriteScale = transform.localScale;
			spriteScale.x *= -1;
			transform.localScale = spriteScale;
			walkDirection = huntDirection;
		}
		volkodlak.AddForce (new Vector2 (volkodlakHuntSpeed * huntDirection, 0));
	}
	
	// Update is called once per frame
	void Update () {

		if (!dead) {

			checkGround ();
		
			if (isGrounded) { // on ground
				if (!triggered) 
					walk ();
				else
					hunt ();

				getFrameRate ();

			} else { // falling, stop animation
				animator.speed = 0;
			}

		} else {
			animator.speed = 1;
		}

	}

	void checkGround (){
	
		isGrounded = Physics2D.Linecast(transform.position, groundChecker1.position, 1 << LayerMask.NameToLayer("Ground"))
					|| Physics2D.Linecast(transform.position, groundChecker2.position, 1 << LayerMask.NameToLayer("Ground"));

	}

	// Increase framerate of animation based on velocity of Volkodlak
	void getFrameRate(){
		float rawFPS = (8.0f * (volkodlak.velocity.x / 14.5f));
		float framesPerSec = (float) Mathf.Abs(rawFPS); // 8 frames = max, 14.5 max velocity
		if (framesPerSec < 4)
			framesPerSec = 4;
		if (animator.speed != framesPerSec) animator.speed = framesPerSec;
	}

	// Call to SpawnPointsToSpawnAVolkodlak
	void spawnABrother(){
	
		// Get spawn points
		GameObject spawnPoints = GameObject.Find ("VolkodlakSpawnPoints");

		// Get random spawn and call its public method spawnOne() to spawn one 
		spawnPoints.transform.GetChild (Random.Range (0, spawnPoints.transform.childCount - 1)).GetComponent<volkodlakSpawnScript>().spawnOne();
	
	}

}
