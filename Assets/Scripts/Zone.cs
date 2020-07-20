using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zone : MonoBehaviour
{
    public bool wantPlayer;
    public bool wantTurret;
    public bool wantDrone;
    public string nextscene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (wantPlayer)
            {
                Advance();
            }
        }
        else if (collision.gameObject.layer == 10)
        {
            Machine_Turret turret = collision.GetComponent<Machine_Turret>();
            Machine_Drone drone = collision.GetComponent<Machine_Drone>();
            if (turret != null && wantTurret)
            {
                Advance();
            }
            else if (drone != null && wantDrone)
            {
                Advance();
            }
        }
    }

    public void Advance()
    {
        Debug.Log("Zone reached or target delivered, level complete!");
        SceneManager.LoadScene(nextscene);
    }
}
