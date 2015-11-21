using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float deathTimer;
	float origDeathTimer;
    public bool waitForCenter = false;
    bool reachedCenter = false;

	// Use this for initialization
	void Start () {
		origDeathTimer = deathTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (!waitForCenter)
        {
            deathTimer -= Time.deltaTime;
            if (gameObject.GetComponent<ParticleSystem>() != null)
            {
                if (deathTimer <= (origDeathTimer - 0.4f))
                {
                    gameObject.GetComponent<ParticleSystem>().enableEmission = false;
                }
                if (deathTimer <= 0.0f)
                {
                    Destroy(this.gameObject);
                }
            }
            else if (deathTimer <= 0.0f)
            {
                Destroy(this.gameObject);
            }
        } else
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 particlePos = gameObject.transform.position;

            float separationDistanceX = (cameraPos.x - particlePos.x);
            float separationDistanceZ = (cameraPos.z - particlePos.z);

            if (!reachedCenter && (separationDistanceX < 0.5 && separationDistanceZ < 0.5))
            {
                deathTimer = 0.5f;
                reachedCenter = true;
            }
            else if (reachedCenter && deathTimer <= 0.0f)
            {
                Destroy(this.gameObject);
            }
            else
                deathTimer -= Time.deltaTime;
            
        }
	}
}
