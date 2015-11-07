using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManagementScript : MonoBehaviour {

	public bool soundEnabled = true;

    public AudioClip introClip;
    public AudioClip loopClip;

	private AudioSource introClipSrc;
    private AudioSource loopClipSrc;

    private bool introPlayed;
    private bool introPlaying;

    public float pitchVelocityMultiplier = 0.0008f;
	private float basePitch = 1.0f;
	private FollowObject cameraFollowScript;

	private bool ascend = true;


	// Use this for initialization
	void Start () {
        if (introClip != null)
        {
            introClipSrc = gameObject.AddComponent<AudioSource>();
            introClipSrc.clip = introClip;
            introClipSrc.playOnAwake = false;
            introClipSrc.loop = false;
        }
        if(loopClip != null)
        {
            loopClipSrc = gameObject.AddComponent<AudioSource>();
            loopClipSrc.clip = loopClip;
            loopClipSrc.playOnAwake = false;
            loopClipSrc.loop = true;
        }

        introPlaying = false;
        introPlayed = false;

        // Needed to play music once the camera picks up an object
        cameraFollowScript = Camera.main.GetComponent<FollowObject> ();
	}
	
	// Called once every frame
	public void Update() {
		if (!soundEnabled)
			return;

		if (cameraFollowScript.GetTarget () != null)
        {
			if (getCurrentBkgMusic() == null || !getCurrentBkgMusic().isPlaying) {
                StartBkgMusic();
			}

			// Modulate tempo with follow object velocity.
			Rigidbody targetObjectRb = cameraFollowScript.GetTarget().GetComponent<Rigidbody>();
			if(targetObjectRb) {
				getCurrentBkgMusic().pitch = basePitch + targetObjectRb.velocity.magnitude * pitchVelocityMultiplier;
			}
		}
	}
    
	// Starts the background music at the given index
	public void StartBkgMusic() {
		if (!soundEnabled)
			return;

        if (introClipSrc != null)
        {
            Debug.Log("Intro Clip!");
            introClipSrc.Play();
            Invoke("SwapToLoop", introClipSrc.clip.length - 0.3f);
            introPlaying = true;
        } else
        {
            Debug.Log("Straigh to Loop!");
            SwapToLoop();
        }
    }

    // Switches from the intro to the main loop
    private void SwapToLoop()
    {
        if (introClipSrc != null)
        {
            introClipSrc.Stop();
        }

        if(loopClipSrc != null)
        {
            loopClipSrc.Play();
            introPlaying = false;
            introPlayed = true;
        }
        Debug.Log("Swapped to loop!");
    }

	// Gets the currently playing background music
	public AudioSource getCurrentBkgMusic() {
        if(introPlayed)
        {
            return loopClipSrc;
        } else if (introPlaying)
        {
            return introClipSrc;
        } else
        {
            return null;
        }
	}
}
