using UnityEngine;
using System.Collections;

public class PowerupSpinning : MonoBehaviour {

	//Literally all this does is make the object spin.
	
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
