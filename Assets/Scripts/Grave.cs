using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Restart", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
