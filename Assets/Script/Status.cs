using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    protected int statusID;
    protected float statusDuration;
    protected float nextEffectTick;
    // Start is called before the first frame update
    void Start() 
    {
        statusID = 0;
        statusDuration = 0;
        nextEffectTick = 0;
    }

    public bool CanSetStatus() {
        if(statusID != 0) {
            return false;
        }
        return true;
    }

    public void SetStatus(int buffID, float buffDuration) {
        this.statusID = buffID;
        this.statusDuration = Time.time + buffDuration;
        this.nextEffectTick = Time.time + 2.0f;
    }
    // Returns an integer representing if an effect is triggered
    // -1: effect ends 0: do nothing 1: damage 2: invun
    public int StatusEffect() {
        switch(statusID)
        {
            case 0:
                //Do Nothing
                return 0;
            // Poison Damage
            case 1:
                if(Time.time >= nextEffectTick && Time.time < statusDuration) {
                    nextEffectTick = Time.time + 2.0f;
                    return 1;
                } else if (Time.time >= statusDuration) {
                    return -1;
                }
                break;
            // Invun
            case 2:
                if(Time.time >= statusDuration) {
                    return -2;
                } else {
                    return 2;
                }
        }
        return 0;   
    }


}
