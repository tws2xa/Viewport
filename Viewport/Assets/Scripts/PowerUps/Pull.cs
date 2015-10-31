using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pull : PowerUp {

	public List<string> tags;

	float gravForceModifier;

	public Pull(int duration) : base(duration)
	{
	}

	// Use this for initialization
	public override void Start () {
		powerUpID = 6;
		duration = 10;
		timeLeft = this.duration;
		gravForceModifier = 10.0f;
		tags = new List<string> ();
		tags.Add ("Player");
		tags.Add ("Moveable");
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
			if (!obj.Equals(gameObject)){
				Rigidbody otherRB = obj.GetComponent<Rigidbody>();
				Rigidbody thisRB = gameObject.GetComponent<Rigidbody>();
				Vector3 distanceVector = obj.transform.position - gameObject.transform.position;
				float gravForce = (otherRB.mass + thisRB.mass) / distanceVector.magnitude;
				Vector3 pullForce = Vector3.Normalize(distanceVector);
				pullForce = pullForce * -gravForce * gravForceModifier;
				otherRB.AddForce(pullForce);
			}
		}
	}

	public override void ModifyObject() {

	}

	public override void DemodifyObject() {

	}
}
