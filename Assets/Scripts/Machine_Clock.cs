using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Clock : Machine
{
    public float period = 6f;
    float curtime;
    public GameObject hand;
    int phase = 1;
    public GameObject[] phase0objects;
    public string[] phase0states;
    public GameObject[] phase1objects;
    public string[] phase1states;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        machineName = "Clock";
        description = "Changes the states of other machines on a cycle.\nThis machine only has one state; jamming will do nothing.";
        possibleStates = new string[] { "ticking" };
        lr = GetComponent<LineRenderer>();
        int linepoints = (phase0objects.Length + phase1objects.Length) * 2;
        lr.positionCount = linepoints;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == "ticking")
        {
            curtime += Time.deltaTime;
            if (curtime > period)
            {
                curtime -= period;
            }

            if (phase == 0 && curtime > period / 2)
            {
                PhaseOne();
                phase = 1;
            }
            else if (phase == 1 && curtime < period / 2)
            {
                PhaseZero();
                phase = 0;
            }

            hand.transform.rotation = Quaternion.Euler(0, 0, -curtime/period*360);
        }
    }

    private void OnMouseOver()
    {
        lr.enabled = true;
        for (int i = 0; i < phase0objects.Length; i++)
        {
            lr.SetPosition(i * 2, transform.position);
            lr.SetPosition(i * 2 + 1, phase0objects[i].transform.position);
        }
        for (int i = 0; i < phase1objects.Length; i++)
        {
            lr.SetPosition(phase0objects.Length * 2 + i * 2, transform.position);
            lr.SetPosition(phase0objects.Length * 2 + i * 2 + 1, phase1objects[i].transform.position);
        }
    }

    private void OnMouseExit()
    {
        lr.enabled = false;
    }

    void PhaseZero()
    {
        for (int i = 0; i < phase0objects.Length; i++)
        {
            phase0objects[i].GetComponent<Machine>().SwitchState(phase0states[i]);
        }
    }

    void PhaseOne()
    {
        for (int i = 0; i < phase1objects.Length; i++)
        {
            phase1objects[i].GetComponent<Machine>().SwitchState(phase1states[i]);
        }
    }
}
