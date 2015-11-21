using UnityEngine;
using System.Collections;
using System;

public class ObjectSpawnPowerUp : PowerUp {

    private float delay;
    ParticleSystem flashiness; // Deal with this later
    Vector3 playerPos;
    ParticleSystem particlez;
    ParticleSystem prefabPart;

	GameObject spawnBox;

    public ObjectSpawnPowerUp(int duration) : base (duration) {
    }

	// Use this for initialization
	public new void Start () {
        delay = 5.0f;
        powerUpID = 4;
        duration = 10;
        timeLeft = this.duration;
		spawnBox = (GameObject)(Resources.Load ("Prefabs/BoxA", typeof(GameObject)));
        particlez = gameObject.GetComponent<ParticleSystem>();
		GameObject playerPrefab = (GameObject)(Resources.Load("Prefabs/PlayerPrefab", typeof(GameObject)));
		prefabPart = playerPrefab.GetComponent<ParticleSystem> ();
        ModifyObject();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void FixedUpdate() {
        objTimer();
        Timer();
    }

    public override void ModifyObject() {
    }

    public override void DemodifyObject() {
    }

    public void objTimer() {
        delay -= Time.deltaTime;
        if(delay <= 5.0f && delay > 3.0f) {
            particlez.emissionRate = 50.0f;
            particlez.startSpeed = -5.0f;
            particlez.startLifetime = 0.35f;
        }
        if(delay <= 3.0f && delay > 1.0f) {
            particlez.emissionRate = 500.0f;
            particlez.startSpeed = -5.0f;
            particlez.startLifetime = 0.35f;
        }
        if(delay <= 1.0f && delay >= 0.5f) {
            playerPos = gameObject.transform.position;
        }
        if(delay <= 0.05f && delay > 0.0f) {
            particlez.emissionRate = 2000.0f;
            particlez.startSpeed = -10.0f;
            particlez.startLifetime = 0.5f;
			GameObject obstacle = GameObject.Instantiate(spawnBox);
            obstacle.transform.position = playerPos + transform.up;
        }
        if(delay <= 0.0f) {
			particlez.startSpeed = prefabPart.startSpeed;
			particlez.startLifetime = prefabPart.startLifetime;
			particlez.emissionRate = prefabPart.emissionRate;
            particlez.enableEmission = false;
        }
    }
}
