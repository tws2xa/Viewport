using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PowerUpManagementScript : MonoBehaviour {

	// This script should go onto pickups.
	// If we want to extend that to both powerups and picker uppers.

	//private SoundManagementScript soundManagerScript;

	//private List<PowerUp> powerUpList = new List<PowerUp>();
	enum powerUpList {TESTPowerUp};
	// Perhaps we need this, perhaps we don't?
	// It's meant to be a list of powerups that are possible.
	// Probably bad coding.

	public int powerUpID;

	// Use this for initialization
	void Start () {
		//loadSoundManager ();
	}

	// Update is called once per frame
	void Update () {
	}

	// Do we need this if we want to play a sound on pickup of a powerup?
	// Loads in the sound manager object.
//	public void loadSoundManager() {
//		GameObject[] soundManagerObjects = GameObject.FindGameObjectsWithTag ("SoundManager");
//		if (soundManagerObjects.Length == 1) {
//			soundManagerScript = soundManagerObjects [0].GetComponent<SoundManagementScript> ();
//		} else {
//			Debug.LogError ("Incorrect number of sound managers. Must have exactly one.");
//		}
//	}

	void CreatePowerupIndicator() {
		// Alert the player that they have a powerup somehow.
		// Not sure if we want to display this on UI or not.
	}

	// Called when the object collides with another object
	void OnCollisionEnter(Collision collision) {
		if (ObjectCanGrabPower(collision.gameObject)) {
			// The other object is guarenteed to have a TransferControlScript because
			// of one of the checks in ObjectCanGrabCamera
			ObjectPowerUpControllerScript otherControlScript = collision.gameObject.GetComponent<ObjectPowerUpControllerScript>();
			//otherControlScript.doSomething();
			if (powerUpID == -1) {
				//PowerUp chosenPowerUp = powerUpList[Random.Range(0, powerUpList.Count + 1)];
				Array values = Enum.GetValues (typeof(powerUpList));
				int randNum = UnityEngine.Random.Range(0, values.Length);
				PowerUp chosenPowerUp = getPowerUp(randNum);
				otherControlScript.ActivatePowerUp(chosenPowerUp);
			} else {
				otherControlScript.ActivatePowerUp(getPowerUp (powerUpID));
			}

			// Increment bkg music

//			if(soundManagerScript != null) {
//				soundManagerScript.updateBkgMusic();
//			}
		}
	}

	PowerUp getPowerUp(int chosenPowerUpID) {
		PowerUp chosenPowerUp = null;
		switch (chosenPowerUpID) {
		case (int)powerUpList.TESTPowerUp: 
			chosenPowerUp = new TESTPowerUp();
			break;
		}
		return chosenPowerUp;
	}

	/// <summary>
	/// Checks if the given object can grab a power up.
	/// </summary>
	/// <returns><c>true</c> if the powerup can be taken be taken, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object with which we have collided.</param>
	bool ObjectCanGrabPower(GameObject obj) {
		// Check the other object allows picking up of power ups.
		if (obj.GetComponent<ObjectPowerUpControllerScript> () == null) {
			return false;
		}

		// Allow object to take powerup
		return true;
	}
	
	/// <summary>
	/// Checks whether or not a powerup is affecting the given object
	/// </summary>
	/// <returns><c>true</c> if is given object is affected by a powerup, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object to check.</param>
	bool PowerupActive(GameObject obj) {
		if (obj.GetComponents<PowerUp>() == null) {
			return false;
		}

		return true;
	}

}
