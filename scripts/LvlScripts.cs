using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlScripts : MonoBehaviour
{
    private GameObject main;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetLevel(int checkpoint)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
