using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Target : MonoBehaviour
{
    public string nextscene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        Debug.Log("Target shot, level complete!");
        SceneManager.LoadScene(nextscene);
    }
}
