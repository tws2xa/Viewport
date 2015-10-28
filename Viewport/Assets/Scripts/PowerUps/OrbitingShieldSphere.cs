using UnityEngine;
using System.Collections;

public class OrbitingShieldSphere : ScriptableObject {

	float shieldSize;
	float shieldRotateSpeed;

	GameObject shieldSphere;
	Vector3 shieldUpdatingPosition;
	Vector3 shieldDeltaPos;

	public OrbitingShieldSphere(float size, float rotateSpeed) {
		shieldSize = size;
		shieldRotateSpeed = rotateSpeed;
		shieldSphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
	}

	// Use this for initialization
	public void Start () {

		shieldSphere.transform.localScale = new Vector3 (shieldSize, shieldSize, shieldSize);
		shieldDeltaPos = new Vector3 (0.0f, 0.0f, 2.0f);
	}

	public void UpdatePosition (Vector3 playerPos) {
		shieldDeltaPos = Quaternion.Euler(0, shieldRotateSpeed, 0.0f) * shieldDeltaPos;
		
		shieldUpdatingPosition = playerPos + shieldDeltaPos;
		shieldUpdatingPosition.y = playerPos.y;
		
		shieldSphere.transform.position = shieldUpdatingPosition;
	}

	public void DestroyShield() {
		Destroy (shieldSphere);
	}
}
