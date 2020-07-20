using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Drone : Machine
{
    float flyspeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        machineName = "Drone";
        description = "Used to transport objects vertically.";
        possibleStates = new string[] { "hovering", "ascending", "descending" };
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "hovering")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (state == "ascending")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyspeed);
        }
        else if (state == "descending")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -flyspeed);
        }
    }
}
