using UnityEngine;
using System.Collections;

public class SpeedUp : PowerUp
{

    // This powerUp should double the maximum angular velocity and decrease the angular drag to 0;

    // Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.

    Rigidbody orb;

    float origVeloc;
    float origAngDr;

    public SpeedUp(int duration) : base(duration)
    {
    }

    public void Start()
    {
        duration = 10;
        timeLeft = this.duration;
        orb = gameObject.GetComponent<Rigidbody>();
        origVeloc = orb.maxAngularVelocity;
        origAngDr = orb.angularDrag;
        ModifyObject();
    }

    // Update is called once per frame
    public override void Update()
    {
        Timer();
    }

    public override void ModifyObject()
    {
        orb.maxAngularVelocity = 14;
        orb.angularDrag = 0;
    }

    public override void DemodifyObject()
    {
        orb.maxAngularVelocity = origVeloc;
        orb.angularDrag = origAngDr;
    }
}
