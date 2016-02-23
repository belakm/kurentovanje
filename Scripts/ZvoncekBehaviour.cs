using UnityEngine;
using System.Collections;

public class ZvoncekBehaviour : MonoBehaviour {

	private bool opened;
	private Animator animator;
	private AudioSource sounds;
	public float soundDelay;

	// Use this for initialization
	void Start () {
		opened = false;
		animator = GetComponent<Animator>();
		sounds = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Player" && !opened) {
			animator.SetBool("Opened", true);
			opened = true;
			sounds.PlayDelayed(soundDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
