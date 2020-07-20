using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    public string machineName;
    public string description;
    public string[] possibleStates;
    public string state = "funny";
    public bool jammed = false;


    public virtual void SwitchState(string newstate)
    {
        if (!jammed)
        {
            state = newstate;
        }
    }

    public virtual void Jam()
    {
        jammed = true;
    }
}
