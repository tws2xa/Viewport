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

	public static void KillPlayer (GameObject player) {
        player.SetActive(false);

        // Code here to allow the player to respawn afterwards

        // If necessary, reassign the camera
        if (player.GetComponent<ViewportControlManagementScript>() != null)
        {
            ViewportControlManagementScript viewportManager = player.GetComponent<ViewportControlManagementScript>();
            if(viewportManager.HasCamera())
            {
                viewportManager.DestroyViewportTargetIcon();
                RandomPlayerCameraReassign();
            }
        }
	}

    public static void RandomPlayerCameraReassign()
    {
        GameObject[] remainingPlayers = GameObject.FindGameObjectsWithTag("Player");
        if(remainingPlayers.Length <= 0)
        {
            return; // No players left!
        }

        int randIndex = (int)Mathf.Floor(Random.value * remainingPlayers.Length);

        GameObject newTarget = remainingPlayers[randIndex];

        if (newTarget.GetComponent<ViewportControlManagementScript>() != null)
        {
            ViewportControlManagementScript viewportManager = newTarget.GetComponent<ViewportControlManagementScript>();
            viewportManager.TakeCamera();

        }
    }
}
