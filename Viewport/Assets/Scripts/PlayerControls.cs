using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	private Rigidbody rigidBody;

	public float normalMovementForce = 10.0f; // The torque to add when moving normally
	public float chargeMovementForce = 20.0f; // The torque to add when charging

	public float turnAssist = 10.0f; // Makes it easier to change direction
	public float assistThreshold = 1.0f; // How different must desired and actual direction be before turn assist is activate?

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizAxis = Input.GetAxis("Horizontal");
		float vertAxis = Input.GetAxis ("Vertical");
		bool charging = Input.GetButton ("Charge");

		Vector3 forceToApply = new Vector3 (
			horizAxis * (charging ? chargeMovementForce : normalMovementForce),
			0,
			vertAxis * (charging ? chargeMovementForce : normalMovementForce)
		);
		rigidBody.AddForce (forceToApply);


		// Turn assist
		if (turnAssist > 0) {
			Vector3 desiredDirection = new Vector3 (horizAxis, rigidBody.velocity.y, vertAxis);
			desiredDirection.Normalize ();

			Vector3 currentDirection = rigidBody.velocity;
			currentDirection.Normalize ();
		
			Vector3 assistDirection = desiredDirection - currentDirection;
			assistDirection.y = 0; // Do not assist in the up/down direction.

			if(assistDirection.magnitude > assistThreshold) {
				rigidBody.AddForce (assistDirection * turnAssist);
			}
		}


		if(Input.GetButton ("Fire2")) {
			ViewportControlManagementScript viewportControlManager = gameObject.GetComponent<ViewportControlManagementScript>();
			viewportControlManager.TakeCamera();
		}
	}

}
