using UnityEngine;
using System.Collections;
using System;

public class ObjectSpawnPowerUp : PowerUp {

    private float delay;
    ParticleSystem flashiness; // Deal with this later
    Vector3 playerPos;

    public ObjectSpawnPowerUp(int duration) : base (duration) {
    }

	// Use this for initialization
	public void Start () {
        delay = 5.0f;
        powerUpID = 4;
        duration = 10;
        timeLeft = this.duration;
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
        if(delay <= 1.0f && delay >= 0.5f) {
            playerPos = gameObject.transform.position;
        }
        if(delay <= 0.05f && delay > 0.0f) {
            GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obstacle.transform.position = playerPos + transform.up;
            obstacle.AddComponent<Rigidbody>();
        }
    }
}
