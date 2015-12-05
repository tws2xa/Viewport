using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManagementScript : MonoBehaviour {

    /// <summary>
    /// Different methods for handling overlays
    /// </summary>
    public enum OverlayMethod {
        None, // Do not use overlays
        Camera_Swap_Constant, // Constantly play overlay, update on camera swap
        Camera_Swap_FadeOut, // Play on camera switch then fade out
    };

    /**
     * General Settings
     **/
    public int songNum = 1; // 1 or 2
	public bool soundEnabled = true;
    public OverlayMethod overlayMethod = OverlayMethod.Camera_Swap_FadeOut;
    public float overlayVolume = 0.7f;
    public float overlayFadeTime = 2.0f; // Seconds for sound to last when using Fade Out Overlay Method
    private float currentOverlayTimeValue = -1;

    /**
     * Song and Overlay AudioClip Files
     **/
    public AudioClip mainSong1Clip;
    public AudioClip mainSong2Clip;
    public List<AudioClip> overlay1Clips;
    public List<AudioClip> overlay2Clips;

    /**
     * Song and Overlay Audio Sources
     **/
	private AudioSource mainSongSource;
    private List<AudioSource> overlaySources;
    private int currentOverlay;
    


    /**
     * Change Pitch Info
     **/
    private bool changePitchOnSpeed = false; // Not recommended with overlays
    private float pitchVelocityMultiplier = 0.0008f;
	private float basePitch = 1.0f;
	private FollowObject cameraFollowScript;


    /// <summary>
    /// Use this for initialization
    /// </summary>
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
        cameraFollowScript = Camera.main.GetComponent<FollowObject> ();

        StartBkgMusic();
	}

    /// <summary>
    /// Called once every frame
    /// </summary>
    public void Update() {
		if (!soundEnabled)
			return;

        // Update Fade Timer
        //
        if(currentOverlayTimeValue >= 0)
        {
            currentOverlayTimeValue = currentOverlayTimeValue - Time.deltaTime;
            if(currentOverlayTimeValue < 0)
            {
                currentOverlayTimeValue = -1;
                overlaySources[currentOverlay].volume = 0;
            }

            if(overlayMethod == OverlayMethod.Camera_Swap_FadeOut && overlayFadeTime != 0 && currentOverlayTimeValue >= 0)
            {
                float interpolant = ((overlayFadeTime - currentOverlayTimeValue) / overlayFadeTime); // Fraction of fade time we have progressed
                interpolant = Math.Min(1, Math.Max(0, interpolant)); // Limit between 0 and 1.
                overlaySources[currentOverlay].volume = Mathf.Lerp(overlayVolume, 0, interpolant);
            }
        }

        // Modulate tempo with follow object velocity.
        // Not recommended with overlays.
        //
        if (changePitchOnSpeed && cameraFollowScript.GetTarget () != null)
        {
			Rigidbody targetObjectRb = cameraFollowScript.GetTarget().GetComponent<Rigidbody>();
			if(targetObjectRb) {
				mainSongSource.pitch = basePitch + targetObjectRb.velocity.magnitude * pitchVelocityMultiplier;
                foreach(AudioSource overlay in overlaySources)
                {
                    overlay.pitch = basePitch + targetObjectRb.velocity.magnitude * pitchVelocityMultiplier;
                }
			}
            
		}
        
	}

    /// <summary>
    /// Starts the background music
    /// </summary>
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

        currentOverlayTimeValue = -1;
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
        if(!soundEnabled || overlayMethod == OverlayMethod.None || overlaySources.Count <= 0)
        {
            return; // Should not play an overlay
        }

        // Increment Overlay (Wrap to list size)
        //
        currentOverlay = (currentOverlay + 1) % overlaySources.Count;
        Debug.Log("Playing Overlay: " + currentOverlay);
        for(int i=0; i<overlaySources.Count; i++)
        {
            overlaySources[i].volume = ((i == currentOverlay) ? overlayVolume : 0);
        }

        // If in fade mode, start the fade timer.
        //
        if(overlayMethod == OverlayMethod.Camera_Swap_FadeOut)
        {
            currentOverlayTimeValue = overlayFadeTime;
        }
    }

    /// <summary>
    /// Update the overlay method being used for sound
    /// </summary>
    public void ChangeOverlayMethod(OverlayMethod newMethod)
    {
        if(newMethod == OverlayMethod.None)
        {
            foreach(AudioSource overlay in overlaySources)
            {
                overlay.volume = 0;
            }
        }

        this.overlayMethod = newMethod;
    }

}
