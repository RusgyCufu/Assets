using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHub : MonoBehaviour
{
    GameObject main;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        if (PlayerPrefs.GetInt("CanDoubleJump") == 1)
        {
            main.GetComponent<CharacterController2D>().maxJumpCount = 2;
        }
    }

    void GiveSkin(int n)
    {
        PlayerPrefs.SetInt("unlock_skin" + n.ToString(), 1);
        main.GetComponent<main_script>().ChangeSkin(n);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
