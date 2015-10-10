using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManagementScript : MonoBehaviour {

	public bool enabled = false;
    public bool cycleTracks = true;

	int currMusicIndex = 0;
	public AudioClip[] bkgMusicClips;
	private List<AudioSource> bkgMusicSources;

	public float pitchVelocityMultiplier = 0.0008f;
	private float basePitch = 1.0f;
	private FollowObject cameraFollowScript;

	private bool ascend = true;


	// Use this for initialization
	void Start () {
		bkgMusicSources = new List<AudioSource> ();
		foreach (AudioClip clip in bkgMusicClips) {
			AudioSource src = gameObject.AddComponent<AudioSource>();
			src.clip = clip;
			src.playOnAwake = false;
			src.loop = true;
			src.Stop();
			bkgMusicSources.Add (src);
		}

		cameraFollowScript = Camera.main.GetComponent<FollowObject> ();
	}
	
	// Called once every frame
	public void Update() {
		if (!enabled)
			return;

		if (cameraFollowScript.GetTarget () != null) {
			if (!getCurrentBkgMusic().isPlaying) {
				startBkgMusic(currMusicIndex);
			}

			// Modulate tempo with follow object velocity.
			Rigidbody targetObjectRb = cameraFollowScript.GetTarget().GetComponent<Rigidbody>();
			if(targetObjectRb) {
				getCurrentBkgMusic().pitch = basePitch + targetObjectRb.velocity.magnitude * pitchVelocityMultiplier;
			}
		}
	}

	// Moves to the next background audio track
	public void updateBkgMusic() {
		if (!enabled)
			return;

		float prevTime = bkgMusicSources [currMusicIndex].time;
		bkgMusicSources [currMusicIndex].Stop ();

        if (cycleTracks)
        {
            currMusicIndex += (ascend ? 1 : -1);
            if (currMusicIndex == 0 || currMusicIndex == bkgMusicSources.Count - 1)
            {
                ascend = !ascend;
            }
        }
        else
        {
            if (currMusicIndex < bkgMusicSources.Count)
            {
                currMusicIndex++;
            }
        }


        bkgMusicSources[currMusicIndex].time = prevTime;
		bkgMusicSources [currMusicIndex].Play ();
	}

	// Starts the background music at the given index
	public void startBkgMusic(int index) {
		if (!enabled)
			return;

		for (int i=0; i < bkgMusicSources.Count; i++) {
			bkgMusicSources[i].Stop ();
		}
		AudioSource toPlay = bkgMusicSources [index % bkgMusicSources.Count];
		toPlay.time = 0;
		toPlay.Play ();
	}

	// Gets the currently playing background music
	public AudioSource getCurrentBkgMusic() {
		return bkgMusicSources[currMusicIndex];
	}
}
