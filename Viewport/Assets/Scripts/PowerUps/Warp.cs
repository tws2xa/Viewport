using UnityEngine;
using System.Collections;

public class Warp : PowerUp {

    Rigidbody orb;
    Vector3 deltaScale;
    Vector3 newScale;

	float velDelta;
	float angDrDelta;
	float masDelta;
	float forcDelta;

	CapsuleCollider capsuleColl;

    public Warp(int duration) : base(duration)
    {
    }

    public void Start()
    {
		powerUpID = 2;
        duration = 10;
        timeLeft = this.duration;
        orb = gameObject.GetComponent<Rigidbody>();
        deltaScale = new Vector3(1, 0, 0);
		velDelta = 14 - orb.maxAngularVelocity;
		masDelta = 0.25F;
		angDrDelta = 0 - orb.angularDrag;
        ModifyObject();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        Timer();
    }

    public override void ModifyObject()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale + deltaScale, 1);
		Destroy (gameObject.GetComponent<SphereCollider> ());
		capsuleColl = gameObject.AddComponent<CapsuleCollider> ();
		capsuleColl.direction = 0;
		capsuleColl.radius = 0.5f;
		capsuleColl.height = 1.0f;
		orb.maxAngularVelocity += velDelta;
		orb.angularDrag += angDrDelta;
		orb.mass -= masDelta;
    }

    public override void DemodifyObject()
    {
		transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale - deltaScale, 1);
		Destroy (gameObject.GetComponent<CapsuleCollider>());
		gameObject.AddComponent<SphereCollider>();
		orb.maxAngularVelocity -= velDelta;
		orb.angularDrag -= angDrDelta;
		orb.mass += masDelta;
    }
}
