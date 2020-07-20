using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Misc : MonoBehaviour
{
    public Camera cam;
    Vector2 mousePos;
    public Vector2 aim;
    float jammerRange = 20f;
    public LayerMask jammerHit;
    LineRenderer jamline;
    public UI_StateMachineBroke ui;
    public GameObject grave;

    // Start is called before the first frame update
    void Start()
    {
        jamline = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        aim = mousePos - new Vector2(transform.position.x, transform.position.y);
        aim = aim.normalized;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, aim, jammerRange, jammerHit);
            jamline.SetPosition(0, transform.position);
            if (hit.transform == null)
            {
                jamline.SetPosition(1, transform.position + new Vector3(aim.x*jammerRange, aim.y*jammerRange));
            }
            else
            {
                jamline.SetPosition(1, hit.point);
                if (hit.transform.gameObject.layer == 10)
                {
                    Machine mach = hit.transform.GetComponent<Machine>();
                    if (mach == null)
                    {
                        mach = hit.transform.GetComponentInParent<Machine>();
                    }
                    mach.Jam();
                }
            }
            jamline.enabled = true;
            StartCoroutine("EraseLine", .05f);
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 15, Color.cyan);
        //Debug.Log(Input.mousePosition);
        RaycastHit2D mouseover = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
        if (mouseover.collider != null)
        {
            if (mouseover.collider.gameObject.layer == 10)
            {
                Machine minfo = mouseover.transform.GetComponent<Machine>();
                if (minfo == null)
                {
                    minfo = mouseover.transform.GetComponentInParent<Machine>();
                }
                //UI Stuff
                ui.curMachine = minfo;
            }
        }
        else
        {
            ui.curMachine = null;
        }

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey("escape"))
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
    }

    public void Kill()
    {
        Debug.Log("Oof");
        GameObject spawnedgrave = GameObject.Instantiate(grave, transform.position, transform.rotation);
        spawnedgrave.transform.parent = null;
        transform.GetChild(0).parent = null;
        Destroy(gameObject);
    }

    IEnumerator EraseLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        jamline.enabled = false;
    }
}
