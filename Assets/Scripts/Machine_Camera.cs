using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Camera : Machine
{
    public GameObject[] enterObjects;
    public string[] enterStates;
    public GameObject[] exitObjects;
    public string[] exitStates;
    PolygonCollider2D vision;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        machineName = "Camera";
        description = "Changes the states of other machines when something enters or exits its vision.\nThis machine only has one state; jamming will do nothing.";
        possibleStates = new string[] { "scanning" };

        vision = GetComponent<PolygonCollider2D>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = vision.points[1];
        vertices[2] = vision.points[2];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        Color[] visioncolors = new Color[3];
        visioncolors[0] = new Color(0, .75f, 0, .75f);
        visioncolors[1] = new Color(0, .75f, 0, .25f);
        visioncolors[2] = new Color(0, .75f, 0, .25f);
        mesh.colors = visioncolors;
        GetComponent<MeshFilter>().mesh = mesh;

        lr = GetComponent<LineRenderer>();
        int linepoints = (enterObjects.Length + exitObjects.Length) * 2;
        lr.positionCount = linepoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < enterObjects.Length; i++)
        {
            enterObjects[i].GetComponent<Machine>().SwitchState(enterStates[i]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < exitObjects.Length; i++)
        {
            exitObjects[i].GetComponent<Machine>().SwitchState(exitStates[i]);
        }
    }

    private void OnMouseOver()
    {
        lr.enabled = true;
        for (int i = 0; i < enterObjects.Length; i++)
        {
            lr.SetPosition(i * 2, transform.position);
            lr.SetPosition(i * 2 + 1, enterObjects[i].transform.position);
        }
        for (int i = 0; i < exitObjects.Length; i++)
        {
            lr.SetPosition(enterObjects.Length * 2 + i * 2, transform.position);
            lr.SetPosition(enterObjects.Length * 2 + i * 2 + 1, exitObjects[i].transform.position);
        }
    }

    private void OnMouseExit()
    {
        lr.enabled = false;
    }
}
