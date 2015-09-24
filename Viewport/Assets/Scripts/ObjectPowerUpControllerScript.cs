using UnityEngine;
using System.Collections;

public class ObjectPowerUpControllerScript : MonoBehaviour {

	// If only one powerup allowed at one time
	Component currentlyActivePowerUp = null;
	// If many powerups allowed at one time.
	// ArrayList<PowerUp> activePowerUps = new ArrayList<>();

	// If limit to how many powerups player can have.
	int powerUpLimit = 1;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



	public void ActivatePowerUp(PowerUp power) {
		//Component addedComp
		// activePowerUps.add(power);
		gameObject.AddComponent (power.GetType());
	}

	public void DeactivatePowerUp(PowerUp power) {
		if (power == null) {
			currentlyActivePowerUp = null;
			// activePowerUps.clear();
		} else {
			// activePowerUps.remove(pu);
		}
		//gameObject.GetComponent<pu.GetType()>
	}
}