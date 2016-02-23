using UnityEngine;
using System.Collections;

public class WindScript : MonoBehaviour {

	// Use this for initialization
	public float minimumSecondsBeforeTurn;
	public float maximumSecondsBeforeTurn;
	public float minWindForce;
	public float maxWindForce;
	public float windForce;

	public WindZone wind;
	private float timeLeftBeforeTurn;

	void Start () {
		wind = GetComponent<WindZone> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (timeLeftBeforeTurn < 0) {
			float windRotation = (Random.value > 0.5f) ? 1 : -1;
			float windForceCalculation = Random.Range(minWindForce, maxWindForce) *  windRotation;
			wind.windMain = windForceCalculation;
			timeLeftBeforeTurn = Random.Range (minimumSecondsBeforeTurn, maximumSecondsBeforeTurn);
			windForce = windForceCalculation;
		}
		else {
			timeLeftBeforeTurn -= Time.fixedDeltaTime;
		}

	}
}
