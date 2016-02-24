using UnityEngine;
using System.Collections;

public class volkodlakSpawnScript : MonoBehaviour {

	private GameObject ProtoVolkodlak;
	public bool spawnOnStart;

	// Use this for initialization
	void Start () {
		ProtoVolkodlak = GameObject.Find ("Volkodlak");
		if (spawnOnStart) {
			spawnOne ();
		}
	}

	public void spawnOne(){
		GameObject clone = (GameObject)Instantiate(ProtoVolkodlak, transform.position, transform.rotation);
		clone.GetComponent<Rigidbody2D> ().isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
