using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Push : PowerUp {

	public List<string> tags;

	float gravForceModifier;

	ParticleSystem objPart;
	ParticleSystem prefabPart;
	
	GameObject playerPrefab;

	public Push(int duration) : base(duration)
	{
	}

	// Use this for initialization
	public new void Start () {
		powerUpID = 5;
		duration = 10;
		timeLeft = this.duration;
		gravForceModifier = 10.0f;
		objPart = gameObject.GetComponent<ParticleSystem> ();
		playerPrefab = (GameObject)(Resources.Load("Prefabs/PlayerPrefab", typeof(GameObject)));
		prefabPart = playerPrefab.GetComponent<ParticleSystem> ();
		tags = new List<string> ();
		tags.Add ("Player");
		tags.Add ("Moveable");
		ModifyObject ();
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
				pullForce = pullForce * gravForce * gravForceModifier;
				otherRB.AddForce(pullForce);
			}
		}
	}

	public override void ModifyObject() {
		objPart.emissionRate = 50.0f;
		objPart.startSpeed = 5.0f;
		objPart.startLifetime = 1.0f;
		objPart.startSize = 0.4f;
		objPart.startColor = Color.yellow;
	}
	
	public override void DemodifyObject() {
		objPart.emissionRate = prefabPart.emissionRate;
		objPart.startSpeed = prefabPart.startSpeed;
		objPart.startLifetime = prefabPart.startLifetime;
		objPart.startSize = prefabPart.startSize;
		objPart.startColor = prefabPart.startColor;
	}
}
