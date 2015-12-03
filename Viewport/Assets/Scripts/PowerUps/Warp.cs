using UnityEngine;
using System.Collections;

public class Warp : PowerUp {

    //Should stretch player width to 1 unit more than original

    Rigidbody orb;
    Vector3 deltaScale;
    Vector3 newScale;
	Vector3 origScale;

	float velDelta;
	float angDrDelta;
	float masDelta;
	float forcDelta;
	float timePassed;
	float interpolant;
	float changeTime;

	CapsuleCollider capsuleColl;

    public Warp(int duration) : base(duration)
    {
    }

	public new void Start()
    {
		powerUpID = 2;
        duration = 10;
		changeTime = 0.3f;
        timeLeft = this.duration;
        orb = gameObject.GetComponent<Rigidbody>();
        deltaScale = new Vector3(1, 0, 0);
		velDelta = 14 - orb.maxAngularVelocity;
		masDelta = 0.25F;
		angDrDelta = 0 - orb.angularDrag;
		origScale = transform.localScale;
        ModifyObject();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        Timer();
		scaleTimer();
    }

    public override void ModifyObject()
    {
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
		Destroy (gameObject.GetComponent<CapsuleCollider>());
		gameObject.AddComponent<SphereCollider>();
		orb.maxAngularVelocity -= velDelta;
		orb.angularDrag -= angDrDelta;
		orb.mass += masDelta;
    }
	public void scaleTimer(){
		timePassed += Time.deltaTime;
		if (timePassed <= changeTime && timePassed >= 0.0F) {
			interpolant = timePassed/changeTime;
			if (interpolant >= 1.0F){
				interpolant = 1.0F;
			}
			transform.localScale = Vector3.Lerp (origScale, origScale + deltaScale, interpolant);
		} else if (timePassed >= duration - changeTime && timePassed <= duration) {
			interpolant = (timePassed - (duration - changeTime))/changeTime;
			if (interpolant >= 1.0F){
				interpolant = 1.0F;
			}
			transform.localScale = Vector3.Lerp(origScale + deltaScale, origScale, interpolant);
		}
	}
}
