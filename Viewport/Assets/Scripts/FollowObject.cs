using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public bool followX = true; // Follow along the x axis?
	public bool followY = false; // Follow along the y axis?
	public bool followZ = true; // Follow along the z axis?

    public Vector3 offsets = new Vector3(0, 0, 0); // Permanent offsets applied when following

	public GameObject initialTarget; // The initial object to follow
	private GameObject target; // The current target
	private Transform targetTransform; // The transform of the current  target
    private PlayerControls targetPlayer;

	private bool lockedOnTarget = false;
	public float moveStepDistance = 1.0f; // Amount this object can move each frame when trying to lock on to the target.

	// Use this for initialization
	void Start() {
		SetTarget (initialTarget);
	}

	// Update is called once per frame
	void Update () {
		if (targetTransform != null) {
			Vector3 desiredPos = new Vector3 (
			followX ? targetTransform.position.x + offsets.x : transform.position.x,
			followY ? targetTransform.position.y + offsets.y : transform.position.y,
			followZ ? targetTransform.position.z + offsets.z : transform.position.z
			);

			Vector3 newPos = transform.position;
			if (lockedOnTarget) {
				// Stay with the target once it's locked on,
				// no matter the movement speed of the target.
				newPos = desiredPos;
			} else {
				Vector3 separationVector = (desiredPos - transform.position);
				float separationDistance = separationVector.magnitude;

				// If the object is within the move step distance, jump to the object's position.
				if (separationDistance < moveStepDistance) {
					newPos = desiredPos;
					lockedOnTarget = true;
				} else {
					// Get the direction the object must move in order to approach the target
					separationVector.Normalize ();
					// We will approach by the moveStepDistance
					separationVector *= moveStepDistance;
					// Move the position
					newPos += separationVector;
				}
			}

			transform.position = newPos;
		}
	}

	/// <summary>
	/// Sets the target object.
	/// </summary>
	/// <param name="newTarget">The new target object.</param>
	public void SetTarget(GameObject newTarget) {
        if (newTarget != null)
        {
            if (target != initialTarget && target != null)
            { 
                targetPlayer = target.GetComponent<PlayerControls>();
                targetPlayer.setTarget(false);
            }         
			target = newTarget;
            if (target != initialTarget)
            {
                targetPlayer = target.GetComponent<PlayerControls>();
                targetPlayer.setTarget(true);
            }
            targetTransform = newTarget.transform;
			lockedOnTarget = false;
		}
	}

	/// <summary>
	/// Gets the target object.
	/// </summary>
	/// <returns>The target object.</returns>
	public GameObject GetTarget() {
		return target;
	}
}
