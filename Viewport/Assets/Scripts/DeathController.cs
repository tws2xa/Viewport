using UnityEngine;
using System.Collections.Generic;

public class DeathController : MonoBehaviour {

    HashSet<string> tags;
	public static bool gameOver = false;
	public static float waitForEndMenu = 3;
	static float toEndMenuTimer = -1;

    public AudioClip deathSound;
    public float deathSoundVolume = 1.0f;
    private static AudioSource deathSoundSource;

	private static float initialTime = 0.0f;

    public enum DeathCause
    {
        OutOfView,
        Lava
    }

	void Start () {
        tags = new HashSet<string>();
        tags.Add("Player");
        if (deathSound != null)
        {
            deathSoundSource = gameObject.AddComponent<AudioSource>();
            deathSoundSource.clip = deathSound;
            deathSoundSource.loop = false;
            deathSoundSource.volume = deathSoundVolume;
            deathSoundSource.playOnAwake = false;
        }
		initialTime = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main != null)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            foreach (string tag in tags)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
                {
                    if (!GeometryUtility.TestPlanesAABB(planes, obj.GetComponent<Collider>().bounds))
                    {
                        KillPlayer(obj, DeathCause.OutOfView);
                    }
                    else if (!obj.GetComponent<PlayerDeathController>().getPrevState())
                    {
                        obj.GetComponent<PlayerDeathController>().setPrevState(true);
                    }
                }
            }
        }

		if (gameOver) { 
			toEndMenuTimer -= Time.deltaTime;
			if(toEndMenuTimer < 0) {
				toEndMenuTimer = -1;
				gameOver = false;
				Application.LoadLevel("endMenu");
			}
		}

	}

	public static void KillPlayer (GameObject player, DeathCause cause) {
		GameObject particleDummyPrefab = (GameObject)(Resources.Load ("Prefabs/ParticleDummy", typeof(GameObject)));
		GameObject particleDummy = GameObject.Instantiate (particleDummyPrefab);
		particleDummy.transform.position = player.transform.position;
		ParticleSystem playPart = particleDummy.GetComponent<ParticleSystem> ();
		//playPart.Emit (50);
        PlayerDeathController playerDeath = player.GetComponent<PlayerDeathController>();
		if (cause.Equals (DeathCause.OutOfView)) {
			FollowObject fo = particleDummy.AddComponent<FollowObject>();
            particleDummy.GetComponent<SelfDestruct>().waitForCenter = true;
			fo.followY = false;
			fo.SetTarget(Camera.main.gameObject);
			//fo.offsets = (particleDummy.transform.position * 0.75f) - Camera.main.transform.position;
		}
        /*if (playerDeath.getPrevState()) {
            playerDeath.attempts -= 1;
            playerDeath.setPrevState(false);
            playerDeath.setTimer(0f);
        }*/

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


        if (deathSoundSource != null)
        {
            deathSoundSource.Play();
        }

        player.SetActive(false);
		PlayerDeath(player.GetComponent<PlayerControls>().playerNum, cause);
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

	public static void PlayerDeath(int playerNum, DeathCause cause) {
		//Debug.Log ("p" + playerNum.ToString ());
		//Debug.Log (Time.fixedTime);
		PlayerPrefs.SetFloat ("p" + playerNum.ToString() + "TimeActive" , Time.fixedTime - initialTime);
		int activeNum = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
			if (go.activeInHierarchy){
				activeNum++;
			}
		}
		if (activeNum <= 1) {
			gameOver = true;
			toEndMenuTimer = waitForEndMenu;
		}
	}
}
