using UnityEngine;
using System.Collections;

public class IceyPlayer : PowerUp {

	PlayerControls pc;
	
	public IceyPlayer (int duration) : base (duration) {
	}
	
	public void Start () {
		powerUpID = 3;
		duration = 10;
		timeLeft = this.duration;
		pc = gameObject.GetComponent<PlayerControls> ();
		ModifyObject ();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
	}
	
	public override void ModifyObject () {
		//pc.turnAssist = -(Mathf.Abs (pc.turnAssist));
		pc.OnIce ();
	}
	
	public override void DemodifyObject () {
		//pc.turnAssist = Mathf.Abs (pc.turnAssist);
		pc.OffIce ();
	}
}
