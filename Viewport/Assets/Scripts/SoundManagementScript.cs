using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManagementScript : MonoBehaviour {

    public int songNum = 1; // 1 or 2

	public bool soundEnabled = true;

    public AudioClip mainSong1Clip;
    public AudioClip mainSong2Clip;
    public List<AudioClip> overlay1Clips;
    public List<AudioClip> overlay2Clips;

	private AudioSource mainSongSource;
    private List<AudioSource> overlaySources;
    private int currentOverlay;

    public float avoidOverlayTime = 0; // Don't play overlays until this many seconds have passed in game
    public float overlayVolume = 0.3f;
    private float overlayStartTimer;

    // public float pitchVelocityMultiplier = 0.0008f;
	// private float basePitch = 1.0f;
	// private FollowObject cameraFollowScript;
    

	// Use this for initialization
	void Start () {
        if ((songNum == 1 && mainSong1Clip != null) || (songNum == 2 && mainSong2Clip != null))
        {
            mainSongSource = gameObject.AddComponent<AudioSource>();
            mainSongSource.clip = ((songNum == 1) ? mainSong1Clip : mainSong2Clip);
            mainSongSource.playOnAwake = true;
            mainSongSource.loop = true;
        }
        overlaySources = new List<AudioSource>();
        foreach(AudioClip overlay in ((songNum == 1) ? overlay1Clips : overlay2Clips))
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = overlay;
            source.playOnAwake = true;
            source.loop = true;
            source.volume = 0;
            overlaySources.Add(source);
        }
        currentOverlay = -1;

        // Needed to play music once the camera picks up an object
        // cameraFollowScript = Camera.main.GetComponent<FollowObject> ();

        StartBkgMusic();
	}
	
	// Called once every frame
	public void Update() {
		if (!soundEnabled)
			return;

        if(overlayStartTimer <= avoidOverlayTime)
        {
            overlayStartTimer += Time.deltaTime;
        }

        /*
		if (cameraFollowScript.GetTarget () != null)
        {
			Modulate tempo with follow object velocity.
			Rigidbody targetObjectRb = cameraFollowScript.GetTarget().GetComponent<Rigidbody>();
			if(targetObjectRb) {
				getCurrentBkgMusic().pitch = basePitch + targetObjectRb.velocity.magnitude * pitchVelocityMultiplier;
			}
            
		}
        */
	}
    
	// Starts the background music at the given index
	public void StartBkgMusic() {
		if (!soundEnabled)
			return;
        
        if (mainSongSource != null)
        {
            mainSongSource.Play();
            foreach(AudioSource source in overlaySources) {
                source.volume = 0;
                source.Play();
            }
        }
        overlayStartTimer = 0.0f;
    }

    /// <summary>
    /// Called whenever the camera changes hands.
    /// </summary>
    public void HandleCameraSwitch()
    {
        PlayNextOverlay();
    }

    /// <summary>
    /// Plays the next overlay in the list.
    /// Loops around to beginning, if reached the end of the list.
    /// </summary>
    public void PlayNextOverlay() {
        if(overlayStartTimer < avoidOverlayTime)
        {
            return; // Don't play overlays yet.
        }

        if(!soundEnabled || overlaySources.Count <= 0)
        {
            return; // No Overlays to Play
        }

        currentOverlay = (currentOverlay + 1) % overlaySources.Count;
        Debug.Log("Playing Overlay: " + currentOverlay);
        for(int i=0; i<overlaySources.Count; i++)
        {
            overlaySources[i].volume = ((i == currentOverlay) ? overlayVolume : 0);
        }
    }

}
