using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Turret : Machine
{
    float gravNormal = 24f;
    public Vector2 temp;

    public int direction;
    float bulletOffset = 1.1f;
    float spool = 0f;
    float spoolrate = .6f;
    float range = 15f;
    public LayerMask bulletHit;
    bool canshoot = true;
    float firedelay = .25f;
    LineRenderer bulletLine;

    // Start is called before the first frame update
    void Start()
    {
        machineName = "Turret";
        description = "Shoots you when you stand in front of it. Do not stand in front of it.";
        possibleStates =  new string[] {"idle","shooting"};
        bulletLine = GetComponent<LineRenderer>();
        if (direction < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == "idle")
        {
            if (spool < 0)
            {
                spool = 0;
            }
            else
            {
                spool -= spoolrate * Time.deltaTime;
            }
            var actual = Physics2D.Raycast(transform.position + new Vector3(bulletOffset * Mathf.Sign(direction), 0, 0), new Vector2(direction, 0), Mathf.Infinity, bulletHit);
            if (actual.transform != null && actual.transform.gameObject.layer == 9)
            {
                SwitchState("shooting");
            }
        }
        else if (state == "shooting")
        {
            var actual = Physics2D.Raycast(transform.position + new Vector3(bulletOffset * Mathf.Sign(direction), 0, 0), new Vector2(direction, 0), Mathf.Infinity, bulletHit);
            if (actual.transform == null || actual.transform.gameObject.layer != 9)
            {
                SwitchState("idle");
            }

            if (spool > 1)
            {
                spool = 1;
            }
            else
            {
                spool += spoolrate * Time.deltaTime;
            }

            if (spool == 1 && canshoot)
            {
                canshoot = false;
                StartCoroutine("Reload", .25f);
                bulletLine.SetPosition(0, transform.position + new Vector3(bulletOffset * Mathf.Sign(direction), 0, 0));
                if (actual.transform == null)
                {
                    bulletLine.SetPosition(1, transform.position + new Vector3((bulletOffset+range) * Mathf.Sign(direction), 0, 0));
                }
                else
                {
                    bulletLine.SetPosition(1, actual.point);
                    if (actual.transform.gameObject.layer == 9)
                    {
                        actual.transform.GetComponent<Player_Misc>().Kill();
                    }
                    else if (actual.transform.gameObject.layer == 12)
                    {
                        actual.transform.GetComponent<Target>().Shot();
                    }
                }
                bulletLine.enabled = true;
                StartCoroutine("EraseLine", .05f);
            }
        }

        temp = transform.GetComponent<Rigidbody2D>().velocity;
        temp.y -= gravNormal * Time.deltaTime;
        transform.GetComponent<Rigidbody2D>().velocity = temp;
        //transform.Translate(temp * Time.deltaTime);
    }

    IEnumerator Reload(float delay)
    {
        yield return new WaitForSeconds(delay);
        canshoot = true;
    }

    IEnumerator EraseLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletLine.enabled = false;
    }
}
