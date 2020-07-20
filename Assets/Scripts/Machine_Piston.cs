using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Piston : Machine
{
    public Rigidbody2D arm;
    float switchtime = .25f;
    float armpos;

    // Start is called before the first frame update
    void Start()
    {
        machineName = "Piston";
        description = "Pushes objects or blocks pathways.";
        possibleStates = new string[] { "extended", "retracted" };
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "extended")
        {
            if (armpos <= -2)
            {
                armpos = -2;
            }
            else
            {
                armpos -= (2 / switchtime) * Time.deltaTime;
            }
            arm.MovePosition(transform.TransformPoint(new Vector3(0, armpos)));
        }
        else if (state == "retracted")
        {
            if (armpos >= 0)
            {
                armpos = 0;
            }
            else
            {
                armpos += (2 / switchtime) * Time.deltaTime;
            }
            arm.MovePosition(transform.TransformPoint(new Vector3(0, armpos)));
        }
    }
}
