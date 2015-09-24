using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {

	// ALL POWERUPS INHERIT FROM THIS CLASS

	protected int powerUpID;
	public int duration; // In seconds
	protected float timeLeft;

	abstract public void Start ();
	
	// Update is called once per frame
	abstract public void Update ();

	abstract public void ModifyObject ();

	abstract public void Timer ();
}
