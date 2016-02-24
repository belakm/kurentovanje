using UnityEngine;
using System.Collections;

public class KurentBehavior : MonoBehaviour {

	public Rigidbody2D kurent;
	private Animator animator;

	// Sprite var
	private int spriteDirection = 1;
	private Vector3 spriteScale;

	// Ground collision
	public bool isGrounded;
	public bool isWalking;

	// public variables can be set in Unity GUI
	public float speed;
	public float jumpSpeedX;
	public float jumpSpeedY;

	// Shooting
	public GameObject bulletPrefab;
	public GameObject bulletAnchor;
	public float roundsPerMinute;
	private float cooldown;

	// Audio
	private AudioSource sounds;

	// Ground control script
	private GroundControlBehaviour groundControl;


	// Use this for initialization
	void Start () {

		// Rigidbody component
		kurent = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		isGrounded = false;
		isWalking = false;

		sounds = GetComponent<AudioSource>();

		groundControl = GameObject.Find ("KurentGroundControl").GetComponent<GroundControlBehaviour>();

	}

	void OnCollisionEnter2D (Collision2D col)
	{

		if (col.gameObject.tag == "Ground" && !isGrounded){
			isGrounded = true;
			animator.SetBool("Grounded", true);
		}

	}
	void OnCollisionExit2D (Collision2D col){

		if (col.gameObject.tag == "Ground" && isGrounded){
			isGrounded = false;
			animator.SetBool("Grounded", false);
		}
	
	}

	// Handle Kurent Moving
	void moving(){

		//float moveHorizontal = Input.GetAxis ("Horizontal");

		float moveHorizontal;

		if (Input.GetKey(KeyCode.LeftArrow)) moveHorizontal = -1f;
		else if (Input.GetKey(KeyCode.RightArrow)) moveHorizontal = 1f;
		else moveHorizontal = 0f;
		
		if (moveHorizontal > 0 && spriteDirection != 1) {
			spriteDirection = 1;
			spriteScale = transform.localScale;
			spriteScale.x *= -1;
			transform.localScale = spriteScale;
		}
		else if (moveHorizontal < 0 && spriteDirection != -1){
			spriteDirection = -1;
			spriteScale = transform.localScale;
			spriteScale.x *= -1;
			transform.localScale = spriteScale;
			
		}
		
		//isGrounded = Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, groundLayer); // checks if you are within 0.15 position in the Y of the ground
		
		//float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement;
		if (isGrounded) movement = new Vector2 (moveHorizontal * speed, 0);
		else movement = new Vector2 (moveHorizontal * jumpSpeedX, 0);
		
		kurent.AddForce (movement);
		
		if (moveHorizontal != 0) animator.SetBool ("Walking", true);
		else animator.SetBool ("Walking", false);
		
		if (Input.GetKeyDown (KeyCode.UpArrow) && groundControl.canJump()) {
			kurent.AddForce (new Vector2 (0, jumpSpeedY), ForceMode2D.Impulse);
			animator.SetTrigger ("Jump");
		}

	}

	void shooting(){

		if(Time.time >= cooldown) {
			if (Input.GetKey (KeyCode.Space)) {
				Fire();
				sounds.Play ();
			}
		}

	}

	void Fire() {
		
		cooldown = Time.time + (60 / roundsPerMinute);

		GameObject bPrefab = Instantiate(bulletPrefab, bulletAnchor.transform.position, Quaternion.identity) as GameObject;

		// Dont collide with player
		Physics2D.IgnoreCollision (bPrefab.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());

		bPrefab.GetComponent<BulletBehaviour> ().Fire (spriteDirection);
	}

	void rotateKurent(){

		float moveHorizontal;

		if (Input.GetKey(KeyCode.LeftArrow)) moveHorizontal = -1f;
		else if (Input.GetKey(KeyCode.RightArrow)) moveHorizontal = 1f;
		else moveHorizontal = 0f;

		if (moveHorizontal > 0 && spriteDirection != 1) {
			spriteDirection = 1;
			spriteScale = transform.localScale;
			spriteScale.x *= -1;
			transform.localScale = spriteScale;
		}
		else if (moveHorizontal < 0 && spriteDirection != -1){
			spriteDirection = -1;
			spriteScale = transform.localScale;
			spriteScale.x *= -1;
			transform.localScale = spriteScale;

		}

		if (moveHorizontal != 0) animator.SetBool ("Walking", true);
		else animator.SetBool ("Walking", false);

		if (Input.GetKeyDown (KeyCode.UpArrow) && groundControl.canJump()) {
			//kurent.AddForce (new Vector2 (0, jumpSpeedY), ForceMode2D.Impulse);
			animator.SetTrigger ("Jump");
		}

	}

	void animate(){
	}
	
	// Update is called once per frame
	void Update () {

		// MOVING
		//moving ();

		rotateKurent ();

		// SHOOTING
		shooting ();

		animate ();

	}
}
