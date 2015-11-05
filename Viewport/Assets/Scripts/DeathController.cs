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
                    KillPlayer(obj);
                } else if (!obj.GetComponent<PlayerDeathController>().getPrevState()) {
                    obj.GetComponent<PlayerDeathController>().setPrevState(true);
                }
            }
        }
	}

	public static void KillPlayer (GameObject player) {
		GameObject particleDummyPrefab = (GameObject)(Resources.Load ("Prefabs/ParticleDummy", typeof(GameObject)));
		GameObject particleDummy = GameObject.Instantiate (particleDummyPrefab);
		particleDummy.transform.position = player.transform.position;
		ParticleSystem playPart = particleDummy.GetComponent<ParticleSystem> ();
		playPart.startColor = Color.red;
		playPart.Emit (100);
		playPart.enableEmission = false;
        PlayerDeathController playerDeath = player.GetComponent<PlayerDeathController>();
        if (playerDeath.getPrevState()) {
            playerDeath.attempts -= 1;
            playerDeath.setPrevState(false);
            playerDeath.setTimer(0f);
        }

        // Code here to allow the player to respawn afterwards

        // If necessary, reassign the camera
        if (player.activeSelf && player.GetComponent<ViewportControlManagementScript>() != null)
        {
            ViewportControlManagementScript viewportManager = player.GetComponent<ViewportControlManagementScript>();
            if(viewportManager.HasCamera())
            {
                viewportManager.DestroyViewportTargetIcon();
                RandomPlayerCameraReassign();
            }
        }

        playerDeath.setTimer(playerDeath.getTimer() + Time.deltaTime);
        if (playerDeath.getTimer() >= 1 + (playerDeath.secondsPerAttempt * playerDeath.attempts))
        {
            player.SetActive(false);
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
