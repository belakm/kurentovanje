using UnityEngine;
using System.Collections;

public class SkullBehaviour : MonoBehaviour {

	private Animator animator;
	private VolkodlakBehaviour volkodlak;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		volkodlak = gameObject.transform.parent.gameObject.GetComponent<VolkodlakBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		if (volkodlak.isDead ())
			animator.SetBool ("dead", true);
	}
}
