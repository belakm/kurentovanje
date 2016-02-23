using UnityEngine;
using System.Collections;

public class BackgroundBehaviour : MonoBehaviour {

	private Transform background;
	private KurentBehavior playerScript;

	public float parallaxRatio;
		
	// Use this for initialization
	void Start () {
		background = GetComponent<Transform>();
		GameObject thePlayer = GameObject.Find("Kurent");
		playerScript = thePlayer.GetComponent<KurentBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 positionParallax = playerScript.kurent.position;
		positionParallax.x *= parallaxRatio;
		positionParallax.y *= parallaxRatio;
		transform.position = positionParallax;
	}
}
