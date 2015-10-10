using UnityEngine;
using System.Collections;

public class SpeedUp : PowerUp
{

    // This powerUp should double the maximum angular velocity and decrease the angular drag to 0;

    // Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.

    Rigidbody orb;


	// Changed variables to use change in variables instead of storing original variables.
	// Should fix any fringe cases where two powerups that modify one property are retrieved in sequence.
	float velDelta;
	float angDrDelta;

    public SpeedUp(int duration) : base(duration)
    {
    }

    public void Start()
    {
        duration = 10;
        timeLeft = this.duration;
        orb = gameObject.GetComponent<Rigidbody>();
		velDelta = 14 - orb.maxAngularVelocity;
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
        orb.maxAngularVelocity += velDelta;
        orb.angularDrag += angDrDelta;
    }

    public override void DemodifyObject()
    {
        orb.maxAngularVelocity -= velDelta;
        orb.angularDrag -= angDrDelta;
    }
}
