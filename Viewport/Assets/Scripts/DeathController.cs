using UnityEngine;
using System.Collections.Generic;

public class DeathController : MonoBehaviour {

    HashSet<string> tags;

	void Start () {
        tags = new HashSet<string>();
        tags.Add("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        foreach (string tag in tags) {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag)) {
                if (!GeometryUtility.TestPlanesAABB(planes, obj.GetComponent<Collider>().bounds)) {
                    killObj(obj);
                }
            }
        }
	}

    void killObj(GameObject obj) {
        Destroy(obj);
    }

	public static void KillPlayer (GameObject obj) {
		obj.SetActive (false);
		// Code here to allow the player to respawn afterwards
	}
}
