using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewportControlManagementScript : MonoBehaviour {

	private Camera viewportCam; // Viewport Camera
	private FollowObject cameraFollowScript; // The camera's follow object script

	private const float GRAB_SHIELD_TIME = 0.5f; // The time (in seconds) an object is protected after grabbing the camera
	private float grabShieldTimer = 0.0f; // Timer on the grab shield

	public Object viewportTargetIndicatorTemlate; // The indicator to show when you have the viewport's control.
	public float viewportIndicatorYOffset = 1.0f; // The Y offset for the indicator when displayed. Should be greater than radius of sphere.
	private Object viewportTargetIndicator; // The current indicator (null if not currently the viewport target)

	private SoundManagementScript soundManagerScript;

    public Dictionary<int, Color> playerColors = new Dictionary<int, Color>()
    {
        {-1, Color.black }, // Something not a player
        {0, new Color(0.6f, 0.2f, 0.2f)}, // Red
        {1, new Color(0.25f, 0.7f, 0.23f)},// Green
        {2, new Color(0.27f, 0.3f, 0.5f)}, // Blue,
        {3, new Color(0.7f, 0.75f, 0.32f)}, // Yellow
        {4, Color.cyan},
        {5, new Color(1, 0.5f, 0.5f)}, // Pink
        {6, new Color(1.0f, 0.1f, 1.0f)}, // Purple
        {7, new Color(1.0f, 0.8f, 0.1f)} // Orange
    };

	// Use this for initialization
	void Start () {
		viewportCam = Camera.main;
		cameraFollowScript = viewportCam.GetComponent<FollowObject> ();
		loadSoundManager ();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (grabShieldTimer > 0) {
			grabShieldTimer -= Time.deltaTime;
			if(grabShieldTimer < 0) {
				grabShieldTimer = 0;
			}
		}

		// Make sure we don't display the viewport target indicator when
		// we're not actually the viewport target.
		if (viewportTargetIndicator != null && !CameraIsTargeting (gameObject)) {
			DestroyViewportTargetIcon();
		}
	}
	
	// Loads in the sound manager object.
	public void loadSoundManager() {
		GameObject[] soundManagerObjects = GameObject.FindGameObjectsWithTag ("SoundManager");
		if (soundManagerObjects.Length == 1) {
			soundManagerScript = soundManagerObjects [0].GetComponent<SoundManagementScript> ();
		} else {
			Debug.LogError ("Incorrect number of sound managers. Must have exactly one.");
		}
	}

	/// <summary>
	/// Takes control of the viewport.
	/// </summary>
	public void TakeCamera(Vector3 iconPosition) {
		if (!CameraIsTargeting (gameObject)) {
			cameraFollowScript.SetTarget (gameObject);
			grabShieldTimer = GRAB_SHIELD_TIME;
			CreateViewportTargetIndicator (iconPosition);
            if(soundManagerScript != null)
            {
                soundManagerScript.HandleCameraSwitch();
            }
		}
	}

	/// <summary>
	/// Creates the viewport target icon.
	/// </summary>
	void CreateViewportTargetIndicator(Vector3 iconStartPosition) {
		if (viewportTargetIndicatorTemlate != null) {
			Vector3 iconPosition = iconStartPosition;
			iconPosition.y += viewportIndicatorYOffset;

			Quaternion iconRotation = Quaternion.Euler(90, 0, 0);
			viewportTargetIndicator = Instantiate (viewportTargetIndicatorTemlate, iconPosition, iconRotation);

			FollowObject indicatorFollowScript = ((GameObject)viewportTargetIndicator).GetComponent<FollowObject>();
			indicatorFollowScript.SetTarget (gameObject);
            indicatorFollowScript.followX = true;
            indicatorFollowScript.followY = true;
            indicatorFollowScript.followZ = true;
            indicatorFollowScript.offsets.y = viewportIndicatorYOffset;
            indicatorFollowScript.moveStepDistance = Camera.main.GetComponent<FollowObject>().moveStepDistance;


            int colNum = -1;
            if (gameObject.GetComponent<PlayerControls>() != null)
            {
                int playerNum = gameObject.GetComponent<PlayerControls>().playerNum;
                colNum = PlayerPrefs.GetInt("p" + playerNum + "ball");
            }
            SpriteRenderer renderer = ((GameObject)viewportTargetIndicator).GetComponent<SpriteRenderer>();
            if(renderer != null)
            {
                renderer.color = playerColors[colNum];
            }
        }
    }

	/// <summary>
	/// Removes the viewport target icon.
	/// </summary>
	public void DestroyViewportTargetIcon() {
		if (viewportTargetIndicator != null) {
			Destroy (viewportTargetIndicator);
		}
	}

	// Called when the object collides with another object
	void OnCollisionEnter(Collision collision) {
		if (ObjectCanGrabCamera(collision.gameObject)) {
			// The other object is guarenteed to have a TransferControlScript because
			// of one of the checks in ObjectCanGrabCamera
			ViewportControlManagementScript otherControlScript = collision.gameObject.GetComponent<ViewportControlManagementScript>();
			DestroyViewportTargetIcon();
			otherControlScript.TakeCamera(gameObject.transform.position);
		}
	}

	/// <summary>
	/// Checks if the given object should grab the camera
	/// </summary>
	/// <returns><c>true</c> if the camera should be taken, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object with which we have collided.</param>
	bool ObjectCanGrabCamera(GameObject obj) {
		// Check we have the camera in the first place.
		if (!CameraIsTargeting (gameObject)) {
			return false;
		}

		// Check we're not shielded.
		if (IsShielded()) {
			return false;
		}

		// Check the other object is a valid camera target
		if (obj.GetComponent<ViewportControlManagementScript> () == null) {
			return false;
		}

		// Transfer the camera
		return true;
	}
	
	/// <summary>
	/// Checks whether or not the camera is targeting the given object
	/// </summary>
	/// <returns><c>true</c> if is given object is targeted, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object to check.</param>
	bool CameraIsTargeting(GameObject obj) {
		return (cameraFollowScript.GetTarget ().Equals(obj));
	}

    /// <summary>
    /// Checks if the object has the camera.
    /// </summary>
    /// <returns>True if the object has the camera, false otherwise</returns>
    public bool HasCamera()
    {
        return CameraIsTargeting(gameObject);
    }

	/// <summary>
	/// Checks if this object is somehow shielded from losing the camera.
	/// </summary>
	/// <returns><c>true</c> if the object is shielded, <c>false</c> otherwise.</returns>
	bool IsShielded() {
		// Grab Shield
		if (grabShieldTimer > 0) {
			return true;
		}

		// No Shield
		return false;
	}

}
