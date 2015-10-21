using UnityEngine;
using System.Collections;

public class Warp : PowerUp {

    Rigidbody orb;
    Vector3 deltaScale;
    Vector3 newScale;

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
		gameObject.AddComponent<CapsuleCollider> ();
    }

    public override void DemodifyObject()
    {
		transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale - deltaScale, 1);
		Destroy (gameObject.GetComponent<CapsuleCollider>());
		gameObject.AddComponent<SphereCollider>();
    }
}
