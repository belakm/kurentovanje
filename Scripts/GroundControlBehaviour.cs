using UnityEngine;
using System.Collections;

public class GroundControlBehaviour : MonoBehaviour {

	// Use this for initialization
	private BoxCollider2D sensor;
	public int numberOfObjectsStandingOn;

	void Start () {
		sensor = GetComponent<BoxCollider2D>();
	}

	// Trigger enter: jumping allower
	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag != "Player") numberOfObjectsStandingOn++;
	}
	
	// Trigger exit: jumping disallowed
	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag != "Player") numberOfObjectsStandingOn--;
	}

	public bool canJump(){
		return (numberOfObjectsStandingOn > 0) ? true : false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
