using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbitingShield : PowerUp {

	//List<OrbitingShieldSphere> shieldList;

	//int shieldCount;

	//float shieldSpawnTimer;

	Vector3 playerUpdatingPosition;

	/** Should not be needed, but temporary fix to allow fix of OrbitingShieldSphere class **/
	float shieldSize;
	float shieldRotateSpeed;
	
	GameObject shieldSphere;
	Vector3 shieldUpdatingPosition;
	Vector3 shieldDeltaPos;

	public OrbitingShield (int duration) : base (duration) 
	{
	}

	// Use this for initialization
	public new void Start () {
		powerUpID = 9;
		duration = 10;
		timeLeft = this.duration;

		//shieldList = new List<OrbitingShieldSphere> ();

		shieldSize = 0.5f;
		shieldRotateSpeed = 5.0f;

		playerUpdatingPosition = gameObject.transform.position;
		shieldSphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		shieldSphere.transform.localScale = new Vector3 (shieldSize, shieldSize, shieldSize);
		shieldDeltaPos = new Vector3 (0.0f, 0.0f, 1.0f);
		shieldUpdatingPosition = playerUpdatingPosition + shieldDeltaPos;
		shieldSphere.transform.position = shieldUpdatingPosition;
	}

//	void spawnShieldTimer (){
//		shieldSpawnTimer -= Time.deltaTime;
//		if (shieldSpawnTimer <= 0.0f) {
//			OrbitingShieldSphere oss = new OrbitingShieldSphere(0.5f, 5.0f);
//			shieldList.Add(oss);
//			oss.UpdatePosition(playerUpdatingPosition);
//		}
//	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();

		playerUpdatingPosition = gameObject.transform.position;

//		foreach (OrbitingShieldSphere o in shieldList) {
//			o.UpdatePosition(playerUpdatingPosition);
//		}
		shieldDeltaPos = Quaternion.Euler(0, shieldRotateSpeed, 0.0f) * shieldDeltaPos;
		
		shieldUpdatingPosition = playerUpdatingPosition + shieldDeltaPos;
		shieldUpdatingPosition.y = playerUpdatingPosition.y;
		
		shieldSphere.transform.position = shieldUpdatingPosition;
	}

	public override void ModifyObject() {
//		playerUpdatingPosition = gameObject.transform.position;
//		shieldUpdatingPosition = playerUpdatingPosition + gameObject.transform.right;
//		shieldDeltaPos = playerUpdatingPosition - shieldUpdatingPosition;
//		shieldSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//		shieldSphere.transform.position = shieldUpdatingPosition;
	}

	public override void DemodifyObject() {
//		foreach (OrbitingShieldSphere o in shieldList) {
//			o.DestroyShield();
//		}
		Destroy (shieldSphere);
	}
}
