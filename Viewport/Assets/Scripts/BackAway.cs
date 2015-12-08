using UnityEngine;
using System.Collections;

[RequireComponent ( typeof(FollowObject) )]
public class BackAway : MonoBehaviour {

    private FollowObject followObjScript;
    private float globalYMin = 0.0f;

    public float backThreshold = 5.0f; // The minimum distance allowed between follow object and camera
    
	// Use this for initialization
	void Start () {
        // This script requires a lot of interaction with the follow object script.
        followObjScript = gameObject.GetComponent<FollowObject>();
        if(followObjScript == null)
        {
            Debug.LogError("Error: You must include a Follow Object Script to use Back Away");
        } else {
            // Don't move below the starting position value
            globalYMin = this.transform.position.y;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        GameObject followObj = followObjScript.GetTarget();

        if(followObj != null)
        {
            Vector3 myPos = gameObject.transform.position;
            float diff = myPos.y - followObj.transform.position.y;

            // Don't go below min y.
            if (followObjScript.followY && myPos.y <= globalYMin)
            {
                followObjScript.followY = false;
                gameObject.transform.position = new Vector3(myPos.x, globalYMin, myPos.z);
            }

            // If object is too close to the camera, begin to back away from it.
            if (!followObjScript.followY && diff <= backThreshold)
            {
                followObjScript.offsets.y = backThreshold;
                followObjScript.followY = true;
            }
        }
    }
}
