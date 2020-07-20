using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Conveyor : Machine
{
    float dragspeed = 3f;
    float dragvel;

    // Start is called before the first frame update
    void Start()
    {
        machineName = "Conveyor Belt";
        description = "Transports objects on top of it.";
        possibleStates = new string[] { "still", "right", "left" };
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "still")
        {
            dragvel = 0;
        }
        else if (state == "right")
        {
            dragvel = dragspeed;
        }
        else if (state == "left")
        {
            dragvel = -dragspeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.Translate(new Vector3(dragvel * Time.deltaTime, 0));
    }
}
