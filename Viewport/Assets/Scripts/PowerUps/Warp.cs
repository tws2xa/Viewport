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
    }

    public override void DemodifyObject()
    {
        transform.localScale -= deltaScale;
    }
}
