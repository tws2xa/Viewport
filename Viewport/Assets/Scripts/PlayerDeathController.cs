using UnityEngine;
using System.Collections;

public class PlayerDeathController : MonoBehaviour {

    private int attempts = 0;
    private float secondsPerAttempt = 0;
    private float timer;
    private bool prevState = true;

    public bool getPrevState() {
        return prevState;
    }

    public void setPrevState(bool prevState) {
        this.prevState = prevState;
    }

    public float getTimer() {
        return timer;
    }

    public void setTimer(float timer) {
        this.timer = timer;
    }
            
}
